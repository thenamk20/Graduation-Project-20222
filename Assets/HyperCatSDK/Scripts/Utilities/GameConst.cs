﻿#if !PROTOTYPE

#endif

using Spine.Unity.Examples;
using UnityEngine;

public class GameConst
{
    public const string PublicKeyAppsflyer =
        "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA5ey5IArolWyBplig8yepzwRCsmd2b43h9+WAcALzLeSfpNTGTFMWI0v/uKsjs7bhf6+zpXhdI1jfwqfbqOFT2e647kjYHEzCWMtRk8dPeJwE3cpyMQDc8dMl1XsGqw5b/sZcimTgTfPwg9gfQhj9G9ZEeEB1w6tOhK+acnMXwnDRQeesZ3CPZHuMC04UBL4FezmCtC9Vju8lOFmZiGtpWduM7r7nCsntOnaoWH6/EyThJtz9rHcDAi2vY9Wd1XVNERKtDtNBdNFfjCvmd1jThCXsKaN6zVVUAX8biX8y4HBnC4Daq1culFjtMacu237YBTfGQqkz9yOPQyaaNV2T9wIDAQAB";

    public const string AppflyerAppKey = "Mza5CYwx7pzKhdhcFcTHdm";

    public const string IronSourceAppKey = "";

    public const string NetworkNotAvailble = "Network connection failed. Please try again later.";

    public const string FeedbackThanks = "Thank you for your feedback!";

    public const string HallSceneName = "Hall";
    public const string BattleSceneName = "Battle";

    public const string SkillJoystickHoz = "Horizontal_Skill_";

    public const string SkillJoystickVer = "Vertical_Skill_";

    public const string PlayerTag = "Player";

    public const string DamageableObject = "DamObject";

    // resources path, file name

    public const string PhotonPrefabs = "PhotonPrefabs";

    public const string PlayerManager = "PlayerManager";

    public const string PlayerController = "PlayerController";

    public const string MissileName = "Missile";

    public static readonly int MovingSpeedHash = Animator.StringToHash("moveSpeed");

    public static readonly int NormalAttackHash = Animator.StringToHash("normalAttack");

    public static readonly int Skill1Hash = Animator.StringToHash("skill1");

    public static readonly int UltimateHash = Animator.StringToHash("ultimate");

    public static readonly int DieHash = Animator.StringToHash("die");

    public static readonly int MovingBoolHash = Animator.StringToHash("isMoving");
}