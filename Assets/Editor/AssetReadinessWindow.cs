#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace NeoTokyo.HackSlash.Editor
{
    public sealed class AssetReadinessWindow : EditorWindow
    {
        [MenuItem("Neo-Tokyo/Asset Readiness")]
        public static void Open() => GetWindow<AssetReadinessWindow>("Asset Readiness");

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Production asset intake", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Import real character GLBs, animation clips, enemy rigs, VFX prefabs and audio into Assets/Art/Incoming. This project intentionally has no blockout art in runtime scenes.", MessageType.Info);
            if (GUILayout.Button("Open Incoming Art Folder")) EditorUtility.RevealInFinder(Application.dataPath + "/Art/Incoming");
            if (GUILayout.Button("Open Addressables Groups")) EditorApplication.ExecuteMenuItem("Window/Asset Management/Addressables/Groups");
        }
    }
}
#endif
