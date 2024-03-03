using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlexibleGridLayout))]
[CanEditMultipleObjects]
public class FlexGridLayoutEditor : Editor
{
    public SerializedProperty ChangePivot;
    public SerializedProperty ChildAlignment;
    public SerializedProperty alignment;

    public SerializedProperty FitType;
    public SerializedProperty GroupType;

    public SerializedProperty columns;
    public SerializedProperty rows;

    public SerializedProperty padding;
    public SerializedProperty spacing;

    public SerializedProperty cellSizeX;
    public SerializedProperty cellSizeY;

    public SerializedProperty FitX;
    public SerializedProperty FitY;

    public SerializedProperty ForceSquare;
    public SerializedProperty NudgeLastItemsOver;

    private void OnEnable()
    {
        ChildAlignment = serializedObject.FindProperty("ChildAlignment");
        ChangePivot = serializedObject.FindProperty("ChangePivot");
        alignment = serializedObject.FindProperty("alignment");
        FitType = serializedObject.FindProperty("fitType");
        GroupType = serializedObject.FindProperty("groupType");

        columns = serializedObject.FindProperty("columns");
        rows = serializedObject.FindProperty("rows");

        padding = serializedObject.FindProperty("Padding");
        spacing = serializedObject.FindProperty("spacing");

        cellSizeX = serializedObject.FindProperty("cellWidth");
        cellSizeY = serializedObject.FindProperty("cellHeight");

        FitX = serializedObject.FindProperty("FitWidth");
        FitY = serializedObject.FindProperty("FitHeight");
        ForceSquare = serializedObject.FindProperty("ForceSquare");
        NudgeLastItemsOver = serializedObject.FindProperty("MiddleLast");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        float originalValue = EditorGUIUtility.labelWidth;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(ChildAlignment);
        EditorGUIUtility.labelWidth = 40;
        EditorGUILayout.PropertyField(ChangePivot, new GUIContent("Pivot"), GUILayout.Width(60));
        EditorGUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = originalValue;

        EditorGUILayout.PropertyField(alignment);

        EditorGUILayout.PropertyField(FitType);
        EditorGUILayout.PropertyField(GroupType);

        bool row = true;
        bool col = true;

        if (GroupType.enumValueIndex == 0)
        {
            row = col = false;
        }
        else if (GroupType.enumValueIndex == 1)
        {
            col = false;
        }
        else if (GroupType.enumValueIndex == 2)
        {
            row = false;
        }
        GUILayout.Space(15);
        if (col)
        {
            EditorGUILayout.PropertyField(columns);
        }
        if (row)
        {
            EditorGUILayout.PropertyField(rows);
        }
        GUILayout.Space(15);
        bool fitx = true;
        bool fity = true;
        bool square = false;
        if (FitType.enumValueIndex == 0) // uniform
        {
            fitx = fity = false;
        }
        else if (FitType.enumValueIndex == 1) // width
        {
            fitx = false;
            square = true;
        }
        else if (FitType.enumValueIndex == 2) // height
        {
            fity = false;
            square = true;
        }
        else if (FitType.enumValueIndex == 3) // flex width
        {
            fitx = false;
            square = false;
        }
        else if (FitType.enumValueIndex == 4) // flex height
        {
            fity = false;
            square = false;
        }
        EditorGUILayout.PropertyField(padding);
        EditorGUILayout.PropertyField(spacing);
        GUILayout.Space(15);
        if (fitx && (!square || !ForceSquare.boolValue))
        {
            EditorGUILayout.PropertyField(cellSizeX);
        }

        if (fity && (!square || !ForceSquare.boolValue))
        {
            EditorGUILayout.PropertyField(cellSizeY);
        }


        if (fitx)
        {
            EditorGUILayout.PropertyField(FitX);
        }

        if (fity)
        {
            EditorGUILayout.PropertyField(FitY);
        }

        if (square)
        {
            EditorGUILayout.PropertyField(ForceSquare);
        }

        EditorGUILayout.PropertyField(NudgeLastItemsOver);

        serializedObject.ApplyModifiedProperties();
    }
}
