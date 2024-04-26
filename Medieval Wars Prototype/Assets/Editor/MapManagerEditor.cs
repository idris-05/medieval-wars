using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Cast the target object to the MapManager type
        MapManager mapManager = (MapManager)target;

        // Add a button to the Inspector
        if (GUILayout.Button("Save Map Data"))
        {
            // Call the method you want to invoke
            MapManager.Instance.SaveMapData();
        }
    }
}
