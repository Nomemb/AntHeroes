using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using System;
using UnityEngine;
public class Utils
{
    /// <summary>
    /// 게임오브젝트의 컴포넌트를 가져오는 함수 <para>
    /// 해당 컴포넌트가 없다면 부착해서 가져옴
    /// </para>
    /// </summary>
    /// <param name="go">대상 오브젝트</param>
    /// <typeparam name="T">가져올 컴포넌트 타입</typeparam>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            Transform transform = go.transform.Find(name);
            if (transform != null)
                return transform.GetComponent<T>();
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = true)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;

        return null;
    }

    public static int GetStatValue(UpgradeStatType stat)
    {
        switch (stat)
        {
            case UpgradeStatType.BaseAttackPower:
                return Managers.Game.BaseAttackPower;
            case UpgradeStatType.TouchLightningPower:
                return Managers.Game.TouchLightningPower;
            case UpgradeStatType.AttackProportionalTouchLightningAdditionalDamage:
                return Managers.Game.AttackProportionalTouchLightningAdditionalDamage;
            case UpgradeStatType.MaxHP:
                return Managers.Game.MaxHP;
            case UpgradeStatType.HPRegen:
                return Managers.Game.HPRegen;
            case UpgradeStatType.AttackSpeed:
                return Managers.Game.AttackSpeed;
            case UpgradeStatType.CriticalRate:
                return Managers.Game.CriticalRate;
            case UpgradeStatType.CriticalDamageAmplification:
                return Managers.Game.CriticalDamageAmplification;
        }
        return 0;
    }

    public static string GetMoneyString(int value)
    {
        return $"{value}";
    }
}
