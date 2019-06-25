using UnityEngine;
using DebugPanels;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DebugPanel
{
    #region Variables

#if UNITY_EDITOR
    private static DebugPanelSave debugPanelSave;
#endif

    #endregion

    #region Instanciate Methods

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnBeforeSceneLoad()
    {
        if (!debugPanelSave)
            debugPanelSave = GetDebugPanelSave();
    }

    public static DebugPanelSave GetDebugPanelSave(bool _create = false)
    {
        DebugPanelSave b_debugPanelSave = null;

        // Load save
        string[] debugPanelSaveFind = AssetDatabase.FindAssets("t:DebugPanelSave");
        if (debugPanelSaveFind.Length > 0)
            b_debugPanelSave = AssetDatabase.LoadAssetAtPath<DebugPanelSave>(AssetDatabase.GUIDToAssetPath(debugPanelSaveFind[0]));

        // If no save, create one
        if (_create && !b_debugPanelSave)
        {
            // Search path
            string debugPanelSavePath = "";
            string[] debugPanelSavePathFind = AssetDatabase.FindAssets("DebugPanelSave t:Script");
            if (debugPanelSavePathFind.Length > 0)
                debugPanelSavePath = ((AssetDatabase.GUIDToAssetPath(debugPanelSavePathFind[0])).Replace("DebugPanelSave.cs", "DebugPanelSave.asset"));

            // In case don't find create at root
            if (!debugPanelSavePath.Contains("DebugPanelSave.asset"))
                debugPanelSavePath = "Assets/MenuBuilderSave.asset";

            b_debugPanelSave = ScriptableObject.CreateInstance<DebugPanelSave>();
            AssetDatabase.CreateAsset(b_debugPanelSave, debugPanelSavePath);
            AssetDatabase.SaveAssets();
        }

        return b_debugPanelSave;
    }
#endif

    #endregion

    #region Log Methods

    public static void Log(string _key, object _value, string _group = "")
    {
#if UNITY_EDITOR
        if (debugPanelSave)
        {
            LogGroup b_logGroup = debugPanelSave.GetOrCreateLogGroup(_group);
            b_logGroup.Log(_key, _value.ToString());
            debugPanelSave.Changed = true;
        }
#endif
    }

    #endregion
}