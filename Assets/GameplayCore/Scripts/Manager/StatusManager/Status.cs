using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    public float Value;

    public abstract void OnTrigger();

    public abstract void OnRemove();
}

