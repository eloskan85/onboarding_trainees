using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestItem))]
public class QuestItem_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var item = (QuestItem)target;

        if (GUILayout.Button("Set Start Position & Rotation"))
        {
            Undo.RecordObject(target, "Setting start position and rotation.");
            item.StartRotation = item.transform.rotation;
            item.StartPosition = item.transform.position;
        }
    }
}
