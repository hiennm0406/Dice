using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusManager", menuName = "Data/StatusManager", order = 0)]
public class StatusManager : SingletonScriptableObject<StatusManager>
{
    public List<Status> listStatus = new List<Status>();

    public Status GetStatus(string _status)
    {
        foreach (var item in listStatus)
        {
            if (item.status == _status)
            {
                return item;
            }
        }
        return null;
    }
}

