using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugEvent))]
public class DebugEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DebugEvent myScript = (DebugEvent)target;
        if(GUILayout.Button("Invoke"))
        {
            myScript.Event.Invoke();
        }
    }
}