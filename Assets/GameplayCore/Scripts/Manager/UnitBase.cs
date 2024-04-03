using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : SerializedMonoBehaviour
{
    public Stat stat;
    public int HPNow;

    [SerializeField]
    public Dictionary<Element, float> Resis = new Dictionary<Element, float>();
    public List<Element> Immune = new List<Element>();
    public List<TakeStatus> status = new List<TakeStatus>();

    public virtual void TakeDamage(ref int _dmg, Element element, List<DmgTag> tags)
    {
        // check all tag logic here


        // immune ?
        Debug.Log("TAKE DMG ==> " + _dmg + " " + element);

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
