#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;

// 
// This is the main RPG editor that which exposes everything included in this sample project.
// 
// This editor window lets users edit and create characters and items. To achieve this, we inherit from OdinMenuEditorWindow 
// which quickly lets us add menu items for various objects. Each of these objects are then customized with Odin attributes to make
// the editor user friendly. 
// 
// In order to let the user create items and characters, we don't actually make use of the [CreateAssetMenu] attribute 
// for any of our scriptable objects, instead we've made a custom ScriptableObjectCreator, which we make use of in the 
// in the custom toolbar drawn in OnBeginDrawEditors method below.
// 
// Go on an adventure in various classes to see how things are achived.
// 

public class RPGEditorWindow : OdinMenuEditorWindow
{
    [MenuItem("(=^･ω･^=)/DataEdit")]
    private static void Open()
    {
        var window = GetWindow<RPGEditorWindow>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);
        tree.DefaultMenuStyle.IconSize = 28.00f;
        tree.Config.DrawSearchToolbar = true;

        // Adds the character overview table.
        tree.AddAssetAtPath("GodData", "Assets/GameplayCore/Resources/Data/GodData.asset", typeof(GodData));
        tree.AddAssetAtPath("UnitData", "Assets/GameplayCore/Resources/Data/UnitData.asset", typeof(UnitData));
        tree.AddAssetAtPath("LevelCampainData", "Assets/GameplayCore/Resources/Data/LevelCampainData.asset", typeof(LevelCampainData));
        return tree;
    }
}
#endif
