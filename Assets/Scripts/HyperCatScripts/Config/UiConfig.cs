#region

using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using UnityEngine;

#endregion

[CreateAssetMenu(fileName = "UiConfig", menuName = "Configs/UI")]
public class UiConfig : ScriptableObject
{
    [Header("All panels")]
    public List<UIPanel> panelInstances;

    [Header("Prefabs"), Space]
    public GameObject prefabScreen;

    public GameObject prefabPopup;
    public GameObject prefabButton;

#if UNITY_EDITOR
    [FolderPath, Header("Path to panels")]
    public string path;

    [Button(ButtonSizes.Large, ButtonStyle.Box), PropertySpace(2, 10)]
    public void GetPanelInstances()
    {
        panelInstances = new List<UIPanel>();
        var fileEntries = Directory.GetFiles(path);

        foreach (var t in fileEntries)
        {
            if (t.EndsWith(".prefab"))
            {
                var ui = AssetDatabase.LoadAssetAtPath<UIPanel>(t.Replace("\\", "/"));
                if (ui != null)
                    panelInstances.Add(ui);
            }
        }
    }

    [InfoBox("Create new panel")]
    [Button(ButtonSizes.Large, ButtonStyle.Box), PropertySpace(0, 20), PropertyOrder(-1), GUIColor(0.4f, 0.8f, 1)]
    public void CreatePanel()
    {
        HCPanelCreator.CreateNewScreen();
    }
#endif
}