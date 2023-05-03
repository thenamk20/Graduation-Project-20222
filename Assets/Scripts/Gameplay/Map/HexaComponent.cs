using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class HexaComponent : MonoBehaviour
{
    [Title("Color percents")]
    [SerializeField]
    private List<int> colorPercents;

    [SerializeField]
    private List<GameObject> mainComponents;

    [Title("Road and Corner")]
    [SerializeField]
    private List<GameObject> straightRoads;

    [SerializeField]
    private List<GameObject> corner2s;

    [SerializeField]
    private List<GameObject> corner3Centers;

    [SerializeField]
    private List<GameObject> corner3Sides;

    [SerializeField]
    private List<GameObject> corner3Lefts;

    [SerializeField]
    private List<GameObject> corner3Rights;

    [SerializeField]
    private List<GameObject> corner2Angle;

    [Title("Border")]
    [SerializeField]
    private List<GameObject> borders;

    private GameObject currentHexa;

    private Vector3 currentRot = Vector3.zero;

    private HEXA_TYPE currentType = HEXA_TYPE.MAIN;

#if UNITY_EDITOR

    public void SetDefault()
    {
        RandomizeHexa(mainComponents);
    }

    void RandomizeHexa(List<GameObject> listObject)
    {
        int rd = Random.Range(0, 101);
        GameObject instance;
        for (int i = colorPercents.Count - 1; i >= 0; i--)
        {
            if (i > 0)
            {
                if (rd <= colorPercents[i])
                {
                    instance = (GameObject)PrefabUtility.InstantiatePrefab(listObject[i], gameObject.transform);
                    instance.transform.localPosition = Vector3.zero;
                    currentHexa = instance;
                    currentHexa.transform.rotation = Quaternion.Euler(currentRot);
                    return;
                }
            }
            else
            {
                instance = (GameObject)PrefabUtility.InstantiatePrefab(listObject[0], gameObject.transform);
                instance.transform.localPosition = Vector3.zero;
                currentHexa = instance;
                currentHexa.transform.rotation = Quaternion.Euler(currentRot);
            }
        }
    }

    void SpecificColor(int index)
    {
        switch (currentType)
        {
            case HEXA_TYPE.MAIN:
                Colorize(mainComponents, index);
                break;

            case HEXA_TYPE.STRAIGHT:
                Colorize(straightRoads, index);
                break;

            case HEXA_TYPE.CORNNER2:
                Colorize(corner2s, index);
                break;

            case HEXA_TYPE.CORNER3_CENTER:
                Colorize(corner3Centers, index);
                break;

            case HEXA_TYPE.CORNER3_SIDE:
                Colorize(corner3Sides, index);
                break;

            case HEXA_TYPE.CORNER3_LEFT:
                Colorize(corner3Lefts, index);
                break;

            case HEXA_TYPE.CORNER3_RIGHT:
                Colorize(corner3Rights, index);
                break;

            case HEXA_TYPE.CORNER2_ANGLE:
                Colorize(corner2Angle, index);
                break;
        }
    }

    void Colorize(List<GameObject> listObject, int index)
    {
        if (currentHexa == null) currentHexa = gameObject.transform.GetChild(0).gameObject;
        if (currentHexa != null) DestroyImmediate(currentHexa);
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(listObject[index], gameObject.transform);
        currentHexa = instance;
        currentHexa.transform.rotation = Quaternion.Euler(currentRot);
    }


    [Title("CUSTOMIZE HERE", Bold = true, TitleAlignment = TitleAlignments.Centered)]

    [Title("Type and color")]
    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
    public void ApplyType(HEXA_TYPE type)
    {
        if (currentHexa == null)
        {
            currentHexa = gameObject.transform.GetChild(0).gameObject;
        }
        if (currentHexa != null) DestroyImmediate(currentHexa);

        currentType = type;
        switch (type)
        {
            case HEXA_TYPE.MAIN:
                RandomizeHexa(mainComponents);
                break;
            case HEXA_TYPE.STRAIGHT:
                RandomizeHexa(straightRoads);
                break;
            case HEXA_TYPE.CORNNER2:
                RandomizeHexa(corner2s);
                break;
            case HEXA_TYPE.CORNER3_CENTER:
                RandomizeHexa(corner3Centers);
                break;
            case HEXA_TYPE.CORNER3_SIDE:
                RandomizeHexa(corner3Sides);
                break;
            case HEXA_TYPE.CORNER3_LEFT:
                RandomizeHexa(corner3Lefts);
                break;
            case HEXA_TYPE.CORNER3_RIGHT:
                RandomizeHexa(corner3Rights);
                break;
            case HEXA_TYPE.CORNER2_ANGLE:
                RandomizeHexa(corner2Angle);
                break;

            case HEXA_TYPE.BORDER:
                RandomizeHexa(borders);
                break;
        }
    }

    [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    public void SetColor(HEXA_COLOR hexaColor)
    {
        SpecificColor((int)hexaColor);
    }


    [Title("Rotate")]

    [Button]
    [InfoBox("Rotate 60 deg")]
    public void Rotate()
    {
        if (currentHexa == null) currentHexa = gameObject.transform.GetChild(0).gameObject;
        currentRot = new Vector3(currentRot.x, currentRot.y + 60, currentRot.z);
        currentHexa.transform.rotation = Quaternion.Euler(currentRot);
    }

    [Button]
    [InfoBox("Rotate 120 deg")]
    public void DoubleRotate()
    {
        if (currentHexa == null) currentHexa = gameObject.transform.GetChild(0).gameObject;
        currentRot = new Vector3(currentRot.x, currentRot.y + 120, currentRot.z);
        currentHexa.transform.rotation = Quaternion.Euler(currentRot);
    }

    [Button]
    [InfoBox("Rotate 180 deg")]
    public void Revert()
    {
        if (currentHexa == null) currentHexa = gameObject.transform.GetChild(0).gameObject;
        currentRot = new Vector3(currentRot.x, currentRot.y + 180, currentRot.z);
        currentHexa.transform.rotation = Quaternion.Euler(currentRot);
    }

    [Button]
    public void ToggleMeshRenderer(bool isEnable)
    {
        MeshRenderer meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        meshRenderer.enabled = isEnable;
    }

#endif
}

public enum HEXA_TYPE
{
    MAIN,
    STRAIGHT,
    CORNNER2,
    CORNER3_CENTER,
    CORNER3_SIDE,
    CORNER3_LEFT,
    CORNER3_RIGHT,
    CORNER2_ANGLE,
    BORDER
}

public enum HEXA_COLOR { 
    NORMAL,
    DARK1,
    DARK2
}
