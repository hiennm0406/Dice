using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class EnumListSelector : MonoBehaviour
{

    public enum enumVal
    {
        AVC,
        PO,
        ASC
    }
    [ValueDropdown("@System.Enum.GetValues(typeof(enumVal))", IsUniqueList = true)]
    public List<enumVal> list;
}
