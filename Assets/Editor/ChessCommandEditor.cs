using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChessCommand))]
public class MyPlayerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ChessCommand myScript = (ChessCommand) target;

        // if (GUILayout.Button("Create Map"))
        // {
        //     myScript.BuildChessTable();
        // }
    }

}