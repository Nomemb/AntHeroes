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

    public const int Upgrade = 20003;
}
