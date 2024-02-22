using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }
    public enum Scene
    {
        Unknown,
        Dev,
        Game,
    }
    
    public enum Sound
    {
        Bgm,
        Effect, 
        Max,
    }

    public enum UpgradeStatType
    {
        BaseAttackPower,
        TouchLightningPower,
        AttackProportionalTouchLightningAdditionalDamage,
        MaxHP,
        HPRegen,
        AttackSpeed,
        CriticalRate,
        CriticalDamageAmplification
    }
    
    public enum AnimState
    {
        None,
        Idle,
        Sweat,
        Walking,
        Attack,
    }

    public const int BasePowerText = 10001;
    public const int PvpPowerText = 10002;
    public const int CollectPowerText = 10003;
    public const int CannonPowerText = 10004;
    public const int AttackAmplificationText = 10005;
    public const int SkillDamageAmplificationText = 10006;
    public const int NormalDamageAmplificationText = 10007;
    public const int FishDamageAmplificationText = 10008;
    public const int TotalDamageIncreaseText = 10009;
    public const int TouchLightningPowerText = 10010;
    public const int AttackProportionalTouchLightningAdditionalDamageText = 10011;
    public const int MaxHPText = 10012;
    public const int HPRegenText = 10013;
    public const int MaxHPAmplificationText = 10014;
    public const int CriticalRateText = 10015;
    public const int CriticalDamageAmplificationText = 10016;
    public const int AttackSpeedText = 10017;
    
    public const int Upgrade = 20003;
}
