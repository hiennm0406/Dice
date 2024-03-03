using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
public class MenuFindRef
{ // Adding a new context menu item
    [MenuItem("GameObject/Find Reference", true)]
    private static bool ValidateLogSelectedTransformName()
    {
        // disable menu item if no transform is selected.
        return Selection.activeTransform != null;
    }

    // Put menu item at top near other "Create" options
    [MenuItem("GameObject/Find Reference", false, 0)] //10
    private static void FindRef(MenuCommand menuCommand)
    {
        // Use selected item as our context (otherwise does nothing because of above)
        //GameObject selected = menuCommand.context as GameObject;
        GameObject selected = Selection.activeObject as GameObject;
        List<GameObject> list = new List<GameObject>();
        // Create a empty game object with same name
        // adjust hierarchy accordingly
        GameObject[] gos = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gos)
        {
            bool have = false;

            Component[] comps = go.GetComponents<MonoBehaviour>();
            foreach (Component c in comps)
            {
                FieldInfo[] fields = c.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (FieldInfo field in fields)
                {

                    bool isSerialized = field.IsDefined(typeof(SerializeField), true);

                    object value = field.GetValue(c);

                    if (value is GameObject || value is Component)
                    {
                        if (value as GameObject != null)
                        {
                            GameObject componentGameObject = value as GameObject;
                            if (componentGameObject == selected)
                            {
                                if (!list.Contains(go))
                                {
                                    list.Add(go);
                                    have = true;
                                    break;
                                }
                            }
                        }
                        else if (value as Component != null)
                        {
                            GameObject componentGameObject = (value as Component).gameObject;
                            if (componentGameObject == selected)
                            {
                                if (!list.Contains(go))
                                {
                                    list.Add(go);
                                    have = true;
                                    break;
                                }
                            }
                        }

                    }
                }
                if (have)
                {
                    break;
                }
            }
        }
        GameObject[] gameObjectsToSelect = list.ToArray();
        Selection.objects = new GameObject[0];
        Selection.objects = gameObjectsToSelect;
    }
}