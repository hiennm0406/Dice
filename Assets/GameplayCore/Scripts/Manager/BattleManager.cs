using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : LocalSingleton<BattleManager>
{
    public Dictionary<Vector2Int, Tile> ListTile = new Dictionary<Vector2Int, Tile>();

    public int lv;
    public int way;
    public LevelCampain level;

    public bool IsPlay;
    public GAMESTAGE Stage;
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

        way = -1;
        Stage = GAMESTAGE.STARTGAME;
    }

    public void StartTurn()
    {
        way++;
        Debug.Log("START TURN - MOVE");
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
                    int x = pos[Random.Range(0, pos.Count)];
                    _unit.MoveUnit(new Vector2Int(x, 8));

                    // init unit stat
                    _unit.UnitStat.CopyStat(_e.BaseStat);
                    _unit.UnitStat.Power = Mathf.CeilToInt(_e.BaseStat.Power * level.PowerFactor * enemyWay.PowerFactor);
                    _unit.UnitStat.HP = Mathf.CeilToInt(_e.BaseStat.HP * level.HpFactor * enemyWay.HpFactor);
                }
            }
        }
    }

    public void RollDice()
    {

    }

    public void TriggerTurn()
    {

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
                    return;
                case GAMESTAGE.ROLLDICE:
                    return;
                case GAMESTAGE.WAITDICE:
                    return;
                case GAMESTAGE.TRIGGERDICE:
                    return;
                case GAMESTAGE.ENDTURN:
                    return;
            }
        }
    }
}

public enum GAMESTAGE
{
    PREGAME,
    STARTGAME,
    UNITMOVE,
    ROLLDICE,
    WAITDICE,
    TRIGGERDICE,
    ENDTURN
}