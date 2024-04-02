using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public Stat stat;
    public int HPNow;

    public List<Status> statuses = new List<Status>();
    public Dictionary<Element, float> Resis = new Dictionary<Element, float>();
    public List<Element> Immune = new List<Element>();

    public virtual void TakeDamage(ref int _dmg, Element element, List<DmgTag> tags)
    {
        // check all tag logic here


        // immune ?
        Debug.Log("TAKE DMG ==> " + _dmg);

        if (Immune.Contains(element))
        {
            _dmg = -1;
            return;
        }

        // piearcing?

        if (tags.Contains(DmgTag.PIERCE))
        {
            HPNow -= _dmg;
            return;
        }

        // resis ?
        float resis = 0;
        if (Resis.ContainsKey(element))
        {
            resis += Resis[element];
        }
        if (Resis.ContainsKey(Element.ALL))
        {
            resis += Resis[Element.ALL];
        }
        float dmg = _dmg * (1f - resis);
        _dmg = Mathf.CeilToInt(dmg);
        HPNow -= _dmg;
        Debug.Log("TAKE DMG FINAL ==> " + _dmg);
    }
}
