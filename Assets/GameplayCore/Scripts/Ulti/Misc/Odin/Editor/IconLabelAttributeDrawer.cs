
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[DrawerPriority(0.2, 0, 0)]
public class IconLabelAttributeDrawer : OdinAttributeDrawer<IconLabelAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        string path = $"Assets/Asset/Icon/icon_{this.Attribute.AssetPath}.png";
        Texture2D icon = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
        GUILayout.Label(icon, GUILayout.MaxWidth(24.0f), GUILayout.MaxHeight(24.0f), GUILayout.ExpandWidth(false));
        this.CallNextDrawer(label);
    }
}