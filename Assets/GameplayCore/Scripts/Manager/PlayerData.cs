using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private static PlayerData _Instance;
    public static PlayerData Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameManager.Instance.PlayerData;
            }
            return _Instance;
        }
    }

    public int GodId;

}
