#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShowroomManager))]
public class ShowroomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ShowroomManager showroomManager = (ShowroomManager)target;

        if (GUILayout.Button("Spawn Object"))
        {
            showroomManager.SpawnObject();
        }
    }
}
#endif