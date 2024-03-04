using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCampainData", menuName = "Data/LevelCampainData", order = 1)]
public class LevelCampainData : SingletonScriptableObject<LevelCampainData>
{
    public List<LevelCampain> levelCampains = new List<LevelCampain>();

    public LevelCampain GetCampain(int lv)
    {
        foreach (var item in levelCampains)
        {
            if (item.lv == lv)
            {
                return item;
            }
        }
        return null;
    }


    private void OnValidate()
    {
        foreach (var item in levelCampains)
        {
            item.LevelInfo = new List<string>();
            foreach (var _lv in item.enemyWays)
            {
                if (_lv.HpFactor == 0 && _lv.PowerFactor == 0 && _lv.unitsInWay.Count == 0)
                {
                    item.LevelInfo.Add("");
                }
                else
                {
                    string s = JsonConvert.SerializeObject(_lv);
                    item.LevelInfo.Add(s);
                }
            }
        }
    }
}

[System.Serializable]
public class LevelCampain
{
    public int lv;
    [HorizontalGroup("Power")]
    public float PowerFactor;
    [HorizontalGroup("Power")]
    public float PowerFactorInWay;
    [HorizontalGroup("HP")]
    public float HpFactor;
    [HorizontalGroup("HP")]
    public float HpFactorInWay;
    public List<string> LevelInfo;
    public List<EnemyWay> enemyWays = new List<EnemyWay>();
}
