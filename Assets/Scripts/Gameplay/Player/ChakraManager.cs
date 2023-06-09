using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    [SerializeField]
    private ProgressBar chakraBar;

    // Update is called once per frame
    void Update()
    {
        RestoreChakra();
    }

    void RestoreChakra()
    {
        if(controller.stats.currentChakra < controller.stats.maxChakra)
        {
            controller.stats.currentChakra += controller.stats.restoreChakraSpeed * Time.deltaTime;
            UpdateChakraBar();
        }
    }

    public void ConsumeChakraForSkill()
    {
        if (CheckChakraRequireForSkill)
        {
            controller.stats.currentChakra -= ChakraRequireForASkill;
            UpdateChakraBar();
        }
    }

    public bool CheckChakraRequireForSkill => (controller.stats.currentChakra >= ChakraRequireForASkill);

    public float ChakraRequireForASkill => controller.stats.CharkaRequirePerSkill;

    public void UpdateChakraBar()
    {
        chakraBar.SetDirectProgressValue(controller.stats.currentChakra / controller.stats.maxChakra);
    }

}
