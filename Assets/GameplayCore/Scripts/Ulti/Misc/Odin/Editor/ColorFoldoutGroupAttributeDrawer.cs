
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class ColorFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColorFoldoutGroupAttribute>
{
    private LocalPersistentContext<bool> isExpanded;
    private ValueResolver<string> gName;
    private ValueResolver<string> colorRed;
    private ValueResolver<string> colorGreen;
    private ValueResolver<string> colorBlue;

    protected override void Initialize()
    {
        this.isExpanded = this.GetPersistentValue<bool>("ColorFoldoutGroupAttributeDrawer.isExpanded",
            GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
        this.gName = ValueResolver.GetForString(Property, Attribute.GroupName);

        this.colorRed = ValueResolver.GetForString(Property, Attribute.red);
        this.colorGreen = ValueResolver.GetForString(Property, Attribute.green);
        this.colorBlue = ValueResolver.GetForString(Property, Attribute.blue);
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        GUIHelper.PushColor(new Color(float.Parse(colorRed.GetValue() != null ? colorRed.GetValue() : "0"), float.Parse(colorGreen.GetValue() != null ? colorGreen.GetValue() : "0"), float.Parse(colorBlue.GetValue() != null ? colorBlue.GetValue() : "0")));

        SirenixEditorGUI.BeginBox();
        SirenixEditorGUI.BeginBoxHeader();
        GUIHelper.PopColor();

        this.isExpanded.Value = SirenixEditorGUI.Foldout(this.isExpanded.Value, gName.GetValue());
        SirenixEditorGUI.EndBoxHeader();

        if (SirenixEditorGUI.BeginFadeGroup(this, this.isExpanded.Value))
        {
            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                this.Property.Children[i].Draw();
            }
        }
        SirenixEditorGUI.IndentSpace();
        SirenixEditorGUI.EndFadeGroup();
        SirenixEditorGUI.EndBox();
    }
}