using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
public class RichTextAttributeDrawer : OdinAttributeDrawer<RichTextAttribute, string>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var previous = EditorStyles.textArea.richText;
        EditorStyles.textArea.richText = true;
        CallNextDrawer(label);
        EditorStyles.textArea.richText = previous;
    }
}