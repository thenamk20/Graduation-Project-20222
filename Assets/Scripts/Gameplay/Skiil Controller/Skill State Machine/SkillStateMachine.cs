using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStateMachine : MonoBehaviour
{
    [SerializeField]
    private SkillItemController skill;

    [ShowInInspector]
    private SkillStateBase currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = new SkillReadyState(skill);
    }

    // Update is called once per frame
    void Update()
    {
        var newState = currentState?.OnUpdate();

        if (newState != currentState)
            ChangeState(newState);
    }

    private void ChangeState(SkillStateBase newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}
