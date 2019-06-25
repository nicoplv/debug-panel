using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace DebugPanels
{
	public class DebugPanelWindow : EditorWindow
    {
        #region Variables

        private static DebugPanelWindow debugPanelWindow;
        private Vector2 scrollViewPosition = Vector2.zero;
        private bool clearOnStart = false;
        private bool b_isPlaying = false;
        private Dictionary<string, bool> groupDisplays = new Dictionary<string, bool>();

        // Style
        private bool guiStyleDefined = false;
        private GUIStyle groupStyle;

        private DebugPanelSave debugPanelSave = null;

        #endregion

        #region Menu Item Methods

        [MenuItem("Window/Debug Panel", priority = 1100)]
        public static void ShowWindow()
        {
            debugPanelWindow = GetWindow<DebugPanelWindow>("Debug Panel");
            debugPanelWindow.titleContent = new GUIContent("Debug Panel", EditorGUIUtility.IconContent("CustomSorting").image);
            debugPanelWindow.Show();
        }
        
        #endregion
        
        #region Window Methods

        public void OnEnable()
        {
            debugPanelSave = DebugPanel.GetDebugPanelSave(true);
        }

        public void Update()
        {
            // Force repaint if changed
            if (debugPanelSave && debugPanelSave.Changed)
                Repaint();
        }

        public void OnGUI()
        {
            // Repaint so reset changed
            debugPanelSave.Changed = false;

            // Clear on start
            if (clearOnStart && EditorApplication.isPlaying && !b_isPlaying)
                ButtonClear();
            b_isPlaying = EditorApplication.isPlaying;

            // Manage GUIStyle
            if (!guiStyleDefined)
            {
                groupStyle = new GUIStyle(EditorStyles.foldout);
                groupStyle.fontStyle = FontStyle.Bold;
                guiStyleDefined = true;
            }

            GUILayout.BeginVertical();

            // Toolbar
            GUILayout.BeginHorizontal(EditorStyles.toolbar);

            // Clear
            if (GUILayout.Button("Clear", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
                ButtonClear();

            EditorGUILayout.Space();

            // Auto clear on start
            clearOnStart = GUILayout.Toggle(clearOnStart, "Clear on Start", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false));

            GUILayout.FlexibleSpace();

            // Show all
            if (GUILayout.Button("Show All", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
                ButtonShowAll();

            // Hide all
            if (GUILayout.Button("Hide All", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
                ButtonHideAll();

            GUILayout.EndHorizontal();

            // Scroll view
            scrollViewPosition = GUILayout.BeginScrollView(scrollViewPosition);

            if (debugPanelSave)
            {
                foreach (LogGroup iLogGroup in debugPanelSave.LogGroups)
                {
                    if (!groupDisplays.ContainsKey(iLogGroup.Name))
                        groupDisplays[iLogGroup.Name] = true;

                    if (iLogGroup.Name != "")
                        groupDisplays[iLogGroup.Name] = EditorGUILayout.Foldout(groupDisplays[iLogGroup.Name], iLogGroup.Name, true, groupStyle);

                    if (groupDisplays[iLogGroup.Name])
                    {
                        foreach (Log iLog in iLogGroup.Logs)
                            EditorGUILayout.LabelField(iLog.Key, iLog.Value);
                    }
                }
            }

            GUILayout.EndScrollView();

            GUILayout.EndVertical();
        }

        #endregion

        #region Button Methods

        private void ButtonClear()
        {
            debugPanelSave.Clear();
        }

        private void ButtonShowAll()
        {
            for (int i = 0; i < groupDisplays.Count; i++)
                groupDisplays[groupDisplays.ElementAt(i).Key] = true;
        }

        private void ButtonHideAll()
        {
            for (int i = 0; i < groupDisplays.Count; i++)
                groupDisplays[groupDisplays.ElementAt(i).Key] = false;
        }

        #endregion
    }
}