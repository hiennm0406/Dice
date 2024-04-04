using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigData", menuName = "Data/ConfigData", order = 0)]
public class ConfigData : SingletonScriptableObject<ConfigData>
{
    public List<int> ExpLevelUp = new List<int>();
}
