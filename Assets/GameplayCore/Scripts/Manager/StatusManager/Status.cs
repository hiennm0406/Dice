using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status : ScriptableObject
{
    public string status;
    public string statusName;

    public float Value;
    public string Description;

    #region VirtualMethod

    public virtual void OnTrigger(UnitBase target)
    {

    }

    public virtual void OnRemove(UnitBase target)
    {

    }
    #endregion
}

