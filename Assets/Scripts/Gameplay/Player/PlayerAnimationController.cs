using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    void OnEnable()
    {
        
    }

    public void PlayIdle()
    {
        // playerAnimator.SetFloat(GameConst.MovingSpeedHash, 0);
        playerAnimator.SetBool(GameConst.MovingBoolHash, false);
    }

    public void PlayMoving()
    {
        //playerAnimator.SetFloat(GameConst.MovingSpeedHash, 1);
        playerAnimator.SetBool(GameConst.MovingBoolHash, true);
    }

    public void PlayNormalAttack()
    {
        playerAnimator.SetTrigger(GameConst.NormalAttackHash);
    }

    public void PlaySkill1()
    {
        playerAnimator.SetTrigger(GameConst.Skill1Hash);
    }

    public void PlayUltimate()
    {
        playerAnimator.SetTrigger(GameConst.UltimateHash);
    }

    public void PlayDie()
    {
        playerAnimator.SetTrigger(GameConst.DieHash);
    }
}
