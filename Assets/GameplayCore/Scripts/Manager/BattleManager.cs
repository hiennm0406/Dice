using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : LocalSingleton<BattleManager>
{
    public Dictionary<int, Tile> ListTile = new Dictionary<int, Tile>();
    public List<UnitController> listUnit = new List<UnitController>();
    public int lv;
    public int way;
    public int wayFactor;
    public LevelCampain level;

    public bool IsPlay;
    public GAMESTAGE Stage;
    public List<DiceOnBoardController> Dice = new List<DiceOnBoardController>();
    public Transform diceStart;


    public List<DiceController> ListDice = new List<DiceController>();
    public int done = 0;


    #region PrivateProperty
    private int diceCount;
    private Camera mainCamera;
    private bool drag = false;
    private DiceOnBoardController diceDrag;
    private Tile nowTile;
    private GodInfo playerGod;
    public GodInfo PlayerGod => playerGod;
    #endregion

    private void Start()
    {
        mainCamera = Camera.main;

        // lấy ra player God
        playerGod = new GodInfo(PlayerData.Instance.GodId, 1); // default 1
        for (int i = 0; i < 5; i++)
        {
            ListDice[i].DiceId = playerGod.godData.dice[i];
        }
    }
    [Button]
    public void InitGame()
    {
        Stage = GAMESTAGE.PREGAME;
        Debug.Log("INIT GAME");

        IsPlay = true;
        StartCoroutine(GamePlay());
    }

    [Button]
    public void StartGame(int lv = -1)
    {
        Debug.Log("START GAME");
        if (lv == -1)
        {
            lv = 1;
        }

        level = LevelCampainData.instance.GetCampain(lv);
        listUnit.Clear();
        way = -1;
        wayFactor = -1;
        Stage = GAMESTAGE.STARTGAME;
    }

    public void StartTurn()
    {
        foreach (var item in Dice)
        {
            Destroy(item.gameObject);
        }
        way++;
        wayFactor++;
        Debug.Log("START TURN - MOVE");

        // move old unit

        foreach (var item in listUnit)
        {
            if (item.HPNow > 0)
            {
                item.MoveUnit();
            }
        }

        // spawn new unit
        bool firstCheck = true;
        if (level.LevelInfo[way] != "")
        {
            EnemyWay enemyWay = JsonConvert.DeserializeObject<EnemyWay>(level.LevelInfo[way]);

            //get list ID enemy;
            foreach (var unitId in enemyWay.unitsInWay)
            {
                // get enemy
                UnitEnemy _e = UnitData.instance.GetUnitEnemy(unitId);
                if (_e != null)
                {
                    GameObject _go = Instantiate(_e.Prefab);
                    UnitController _unit = _go.GetComponent<UnitController>();
                    // random pos
                    List<int> pos = new List<int>();
                    for (int i = 0; i < 6; i++)
                    {
                        if (ListTile[Helper.GetIVector(i, Helper.col - 1)].unitController == null)
                        {
                            pos.Add(i);
                        }
                    }
                    if (firstCheck)
                    {
                        firstCheck = false;
                        if (pos.Count < enemyWay.unitsInWay.Count)
                        {
                            Stage = GAMESTAGE.UNITMOVE;
                            way--;
                            return;
                        }
                    }
                    int x = pos[Random.Range(0, pos.Count)];
                    _unit.StartMoveUnit(Helper.GetIVector(x, Helper.col - 1));

                    // init unit stat
                    _unit.UnitStat.CopyStat(_e.BaseStat);
                    _unit.UnitStat.Power = Mathf.CeilToInt(_e.BaseStat.Power * (1 + level.PowerFactor) * (1 + enemyWay.PowerFactor) * (1 + wayFactor * level.PowerFactorInWay));
                    _unit.UnitStat.HP = Mathf.CeilToInt(_e.BaseStat.HP * (1 + level.HpFactor) * (1 + enemyWay.HpFactor) * (1 + wayFactor * level.HpFactorInWay));
                    _unit.HPNow = _unit.UnitStat.HP;
                    listUnit.Add(_unit);
                }
            }
        }
        Stage = GAMESTAGE.UNITMOVE;
    }


    public void RollDice()
    {
        Debug.Log("ROLL DICE");

        if (Stage != GAMESTAGE.ROLLDICE)
        {
            return;
        }
        Stage = GAMESTAGE.WAITDICE;

        // random 3 trong 5 dice
        Dice.Clear();
        for (int i = 0; i < 3; i++)
        {
            int x = Random.Range(0, 5);
            GameObject _go = Instantiate(ListDice[x].gameObject);

            Dice.Add(_go.GetComponent<DiceOnBoardController>());
        }

        diceCount = 0;
        List<int> _list = new List<int>();
        foreach (KeyValuePair<int, Tile> item in ListTile)
        {
            if (item.Value.unitController == null)
            {
                item.Value.free = true;
            }
        }
        Messenger.Broadcast(GameConstant.Event.RESET_COLOR);
        for (int i = 0; i < Dice.Count; i++)
        {
            Dice[i].OnBoard = true;
            //get free slot
            foreach (KeyValuePair<int, Tile> item in ListTile)
            {
                if (item.Value.unitController == null && item.Value.free)
                {
                    _list.Add(item.Key);
                }
            }

            int _vec = _list[Random.Range(0, _list.Count)];
            ListTile[_vec].free = false;
            // random 1-6
            diceCount++;
            Dice[i].pos = _vec;
            Dice[i].number = Random.Range(0, 6);
            StartCoroutine(MoveParabol(Dice[i], diceStart.position, ListTile[_vec].transform.position, Random.Range(1f, 2f), 5f));
        }
    }

    [Button]
    public void TriggerDice()
    {
        if (IsPlay && Stage == GAMESTAGE.WAITPLAYER)
        {
            Stage = GAMESTAGE.BEFORERDICE;
        }
    }


    public void Endturn()
    {

    }


    public IEnumerator GamePlay()
    {
        while (IsPlay)
        {
            switch (Stage)
            {
                case GAMESTAGE.PREGAME:
                    StartGame();
                    break;
                case GAMESTAGE.STARTGAME:
                    StartTurn();
                    break;
                case GAMESTAGE.UNITMOVE:
                    foreach (var item in listUnit)
                    {
                        if (item.isMoving && item.HPNow > 0)
                        {
                            break;
                        }
                    }
                    Stage = GAMESTAGE.ROLLDICE;
                    break;
                case GAMESTAGE.ROLLDICE:
                    RollDice();
                    break;
                case GAMESTAGE.WAITDICE:
                    break;
                case GAMESTAGE.WAITPLAYER:

                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, int.MaxValue, 1 << GameConstant.GameLayer.DICE);

                        // Kiểm tra xem ray đã va chạm với một collider không
                        if (hit.collider != null)
                        {
                            diceDrag = hit.collider.GetComponent<DiceOnBoardController>();
                            // Xử lý các thao tác sau khi raycast trúng collider
                            drag = true;
                            diceDrag.boxCollider2D.enabled = false;
                        }

                        hit = Physics2D.Raycast(rayOrigin, Vector2.zero, int.MaxValue, 1 << GameConstant.GameLayer.FLOOR);

                        // Kiểm tra xem ray đã va chạm với một collider không
                        if (hit.collider != null)
                        {
                            nowTile = hit.collider.GetComponent<Tile>();
                        }
                    }


                    if (drag)
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            drag = false;
                            diceDrag.boxCollider2D.enabled = true;
                            diceDrag = null;
                            break;
                        }

                        if (Input.GetMouseButton(0))
                        {
                            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, int.MaxValue, 1 << GameConstant.GameLayer.FLOOR);

                            // Kiểm tra xem ray đã va chạm với một collider không
                            if (hit.collider != null)
                            {
                                Tile _tile = hit.collider.GetComponent<Tile>();
                                if (_tile != nowTile && _tile.free)
                                {
                                    nowTile.free = true;
                                    nowTile = _tile;
                                    nowTile.free = false;
                                    diceDrag.gameObject.transform.position = nowTile.transform.position;
                                    diceDrag.pos = Helper.GetIVector(nowTile.Row, nowTile.Col);
                                    Messenger.Broadcast(GameConstant.Event.RESET_COLOR);
                                    Messenger.Broadcast(GameConstant.Event.SET_COLOR);
                                }
                            }
                        }
                    }

                    break;
                case GAMESTAGE.BEFORERDICE:
                    // do smt.
                    Stage = GAMESTAGE.TRIGGERDICE;
                    break;
                case GAMESTAGE.TRIGGERDICE:
                    // do smt
                    done = Dice.Count;
                    foreach (var item in Dice)
                    {
                        StartCoroutine(item.dice.TriggerDice(item.number));
                    }

                    while (done > 0)
                    {
                        yield return null;
                    }
                    Stage = GAMESTAGE.ENDTURN;
                    break;
                case GAMESTAGE.ENDTURN:
                    // do smt
                    done = listUnit.Count;
                    foreach (var item in listUnit)
                    {
                        StartCoroutine(item.EndTurn());
                    }
                    while (done > 0)
                    {
                        yield return null;
                    }
                    StartTurn();
                    break;
            }
            yield return null;
        }
    }

    #region RollDice

    public IEnumerator MoveParabol(DiceOnBoardController movingGo, Vector3 start, Vector3 to, float arcHeight, float speed)
    {
        float distance = Vector3.Distance(start, to);

        float _stepScale = speed / distance;

        float _progress = 0;
        movingGo.transform.position = start;
        Animator anim = movingGo.GetComponent<Animator>();

        anim.enabled = true;

        anim.SetFloat("Speed", 10);
        while (movingGo.transform.position.DistanceSqrt(to) > 0.01f)
        {
            _progress = Mathf.Min(_progress + Time.deltaTime * _stepScale, 1.0f);

            // Turn this 0-1 value into a parabola that goes from 0 to 1, then back to 0.
            float parabola = 1.0f - 4.0f * (_progress - 0.5f) * (_progress - 0.5f);

            // Travel in a straight line from our start position to the target.        
            Vector3 nextPos = Vector3.Lerp(start, to, _progress);

            // Then add a vertical arc in excess of this.
            nextPos.y += parabola * arcHeight;

            // Continue as before.

            movingGo.transform.position = nextPos;
            yield return null;
        }
        float t = 10;
        while (t > 3)
        {
            t -= Time.deltaTime * Random.Range(2f, 3f) * 2;
            anim.SetFloat("Speed", t);
            yield return null;
        }
        anim.enabled = false;
        movingGo.spriteRenderer.sprite = DiceData.instance.GetDice(0).SpriteList[movingGo.number];
        diceCount--;
        if (diceCount == 0)
        {
            if (IsPlay)
            {
                UIManager.Instance.battleUI.ShowButtonTrigger();
                Stage = GAMESTAGE.WAITPLAYER;
            }
        }
        movingGo.ChangePos();
    }
    #endregion
}

public enum GAMESTAGE
{
    PREGAME,
    STARTGAME,
    UNITMOVE,
    ROLLDICE,
    WAITDICE,
    WAITPLAYER,
    BEFORERDICE,
    TRIGGERDICE,
    ENDTURN
}