using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SunBurn", menuName = "Create Data/Status/SunBurn")]
public class Status_SunBurn : Status
{
    public override void OnTrigger(UnitBase target)
    {
        if (!target.Resis.ContainsKey(Element.FIRE))
        {
            target.Resis.Add(Element.FIRE, -Value);
        }
        else
        {
            target.Resis[Element.FIRE] -= Value;
        }
    }

    public override void OnRemove(UnitBase target)
    {
        if (target.Resis.ContainsKey(Element.FIRE))
        {
            target.Resis[Element.FIRE] += Value;
        }
    }

}

