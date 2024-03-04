using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : LocalSingleton<BattleManager>
{
    public Dictionary<Vector2Int, Tile> ListTile = new Dictionary<Vector2Int, Tile>();
    public List<UnitController> listUnit = new List<UnitController>();
    public int lv;
    public int way;
    public int wayFactor;
    public LevelCampain level;

    public bool IsPlay;
    public GAMESTAGE Stage;
    public DiceOnBoardController[] Dice;
    public Transform diceStart;
    #region PrivateProperty
    int diceCount;
    Camera mainCamera;
    bool drag = false;
    DiceOnBoardController diceDrag;
    Tile nowTile;
    #endregion

    private void Start()
    {
        mainCamera = Camera.main;
    }
    [Button]
    public void InitGame()
    {
        Stage = GAMESTAGE.PREGAME;
        Debug.Log("INIT GAME");

        IsPlay = true;
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
            item.gameObject.SetActive(false);
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
                    for (int i = 1; i <= 6; i++)
                    {
                        if (ListTile[new Vector2Int(i, 8)].unitController == null)
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
                    _unit.StartMoveUnit(new Vector2Int(x, 8));

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
        foreach (var item in Dice)
        {
            item.gameObject.SetActive(true);
        }
        diceCount = 0;
        List<Vector2Int> _list = new List<Vector2Int>();
        foreach (KeyValuePair<Vector2Int, Tile> item in ListTile)
        {
            item.Value.free = true;
        }
        for (int i = 0; i < Dice.Length; i++)
        {
            //get free slot
            foreach (KeyValuePair<Vector2Int, Tile> item in ListTile)
            {
                if (item.Value.unitController == null && item.Value.free)
                {
                    _list.Add(item.Key);
                }
            }

            Vector2Int _vec = _list[Random.Range(0, _list.Count)];
            ListTile[_vec].free = false;
            // random 1-6
            diceCount++;
            Dice[i].pos = _vec;
            StartCoroutine(MoveParabol(Dice[i], diceStart.position, ListTile[_vec].transform.position, Random.Range(1f, 2f), 5f, Random.Range(0, 6)));
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


    public void Update()
    {
        if (IsPlay)
        {
            switch (Stage)
            {
                case GAMESTAGE.PREGAME:
                    StartGame();
                    return;
                case GAMESTAGE.STARTGAME:
                    StartTurn();
                    return;
                case GAMESTAGE.UNITMOVE:
                    foreach (var item in listUnit)
                    {
                        if (item.isMoving && item.HPNow > 0)
                        {
                            return;
                        }
                    }
                    Stage = GAMESTAGE.ROLLDICE;
                    return;
                case GAMESTAGE.ROLLDICE:
                    RollDice();
                    return;
                case GAMESTAGE.WAITDICE:
                    return;
                case GAMESTAGE.WAITPLAYER:

                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, int.MaxValue, 1 << GameConstant.GameLayer.DICE);

                        // Kiểm tra xem ray đã va chạm với một collider không
                        if (hit.collider != null)
                        {
                            Debug.Log("Ray hit: " + hit.collider.name);
                            diceDrag = hit.collider.GetComponent<DiceOnBoardController>();
                            // Xử lý các thao tác sau khi raycast trúng collider
                            drag = true;
                            diceDrag.boxCollider2D.enabled = false;
                        }

                        hit = Physics2D.Raycast(rayOrigin, Vector2.zero, int.MaxValue, 1 << GameConstant.GameLayer.FLOOR);

                        // Kiểm tra xem ray đã va chạm với một collider không
                        if (hit.collider != null)
                        {
                            Debug.Log("Ray hit: " + hit.collider.name);
                            nowTile = hit.collider.GetComponent<Tile>();
                        }
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        drag = false;
                        diceDrag.boxCollider2D.enabled = true;
                    }

                    if (drag)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, int.MaxValue, 1 << GameConstant.GameLayer.FLOOR);

                            // Kiểm tra xem ray đã va chạm với một collider không
                            if (hit.collider != null)
                            {
                                Debug.Log("Ray hit: " + hit.collider.name);
                                Tile _tile = hit.collider.GetComponent<Tile>();
                                if (_tile != nowTile && _tile.free)
                                {
                                    nowTile.free = true;
                                    nowTile = _tile;
                                    nowTile.free = false;
                                    diceDrag.gameObject.transform.position = nowTile.transform.position;
                                    diceDrag.pos = new Vector2Int(nowTile.Row, nowTile.Col);
                                }
                            }
                        }
                    }
                    return;
                case GAMESTAGE.BEFORERDICE:
                    // do smt.
                    Stage = GAMESTAGE.TRIGGERDICE;
                    return;
                case GAMESTAGE.TRIGGERDICE:
                    // do smt
                    Stage = GAMESTAGE.ENDTURN;
                    return;
                case GAMESTAGE.ENDTURN:
                    // do smt
                    StartTurn();
                    return;
            }
        }
    }


    #region RollDice

    public IEnumerator MoveParabol(DiceOnBoardController movingGo, Vector3 start, Vector3 to, float arcHeight, float speed, int value)
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
        movingGo.spriteRenderer.sprite = DiceData.instance.GetDice(0).SpriteList[value];
        diceCount--;
        if (diceCount == 0)
        {
            if (IsPlay)
            {
                Stage = GAMESTAGE.WAITPLAYER;
            }
        }
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