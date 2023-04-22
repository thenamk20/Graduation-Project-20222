using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "HcGameSetting", menuName = "HyperCat/Game Setting")]
public class HCGameSetting : ScriptableObject
{
    [BoxGroup("Identifier")]
    public string gameName = "Prototype";

    [BoxGroup("Identifier")]
    public string packageName = "com.hypercat.prototype";

    [HorizontalGroup("A", LabelWidth = 100), PropertySpace(10, 10)]
    public string gameVersion = "1.0";

    [HorizontalGroup("A"), PropertySpace(10, 10)]
    public int bundleVersion = 1;

    [HorizontalGroup("A"), PropertySpace(10, 10)]
    public int buildVersion = 0;

    [PropertySpace(10, 20), Header("Appstore ID (iOS Only)")]
    public string appstoreId;


    [Space, Header("Screen Orientation")]
    public ScreenOrientation aoaOrientation = ScreenOrientation.Portrait;

#if UNITY_EDITOR
    public UIOrientation deviceOrientation = UIOrientation.Portrait;
    
    [Button]
    public static void SaveGameSetting()
    {
        HCTools.SaveGameSetting();
    }
#endif
}