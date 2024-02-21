using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using static Define;

public class UI_TrainingItem : UI_Base
{
	enum Texts
	{
		TitleText,
		ChangeText,
		DiffText,
		UpgradeText,
		MoneyText
	}
	
	enum Buttons
	{
		UpgradeButton
	}

	UpgradeStatType _upgradeStatType;
	StatData _statData;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;
		
		BindText(typeof(Texts));
		BindButton(typeof(Buttons));

		GetText((int)Texts.UpgradeText).text = Managers.GetText(Define.Upgrade);

		GetButton((int)Buttons.UpgradeButton).gameObject.BindEvent(OnPressUpgradeButton, UIEvent.Pressed);
		GetButton((int)Buttons.UpgradeButton).gameObject.BindEvent(OnPointerUp, UIEvent.PointerUp);

		RefreshUI();
		return true;
	}

	public void SetInfo(UpgradeStatType type)
	{
		_upgradeStatType = type;

		int id = GetStatUpgradeId(_upgradeStatType);
		if (Managers.Data.Stats.TryGetValue((int)id, out _statData) == false)
		{
			Debug.Log($"UI_AbilityItem SetInfo Failed : {_upgradeStatType}");
			return;
		}

		RefreshUI();
	}
	public void RefreshUI()
	{
		if (_init == false)
			return;

		int value = Utils.GetStatValue(_upgradeStatType);

		GetText((int)Texts.TitleText).text = Managers.GetText(_statData.nameID);
		GetText((int)Texts.ChangeText).text = $"{value} → {GetIncreasedValue()}";
		GetText((int)Texts.MoneyText).text = Utils.GetMoneyString(_statData.price);

		if (CanUpgrade())
			GetButton((int)Buttons.UpgradeButton).interactable = true;
		else
			GetButton((int)Buttons.UpgradeButton).interactable = false;
		
		GetText((int)Texts.DiffText).gameObject.SetActive(false);
	}
	
	/// <summary>
	/// StatData에 저장된 해당 스탯타입의 번호를 반환
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	int GetStatUpgradeId(UpgradeStatType type)
	{
		switch (type)
		{
			case UpgradeStatType.BaseAttackPower:
				return 1;
			case UpgradeStatType.TouchLightningPower:
				return 2;
			case UpgradeStatType.AttackProportionalTouchLightningAdditionalDamage:
				return 3;
			case UpgradeStatType.MaxHP:
				return 4;
			case UpgradeStatType.HPRegen:
				return 5;
			case UpgradeStatType.AttackSpeed:
				return 6;
			case UpgradeStatType.CriticalRate:
				return 7;
			case UpgradeStatType.CriticalDamageAmplification:
				return 8;
			
		}

		return 0;
	}
	Coroutine _coolTime;
	private void OnPressUpgradeButton()
	{
		if (_coolTime == null)
		{
			if (CanUpgrade())
			{
				Managers.Game.Money -= _statData.price;
				int value = _statData.increaseStat;

				switch (_upgradeStatType)
				{
					case UpgradeStatType.BaseAttackPower:
						Managers.Game.BaseAttackPower += value;
						break;
					
					case UpgradeStatType.TouchLightningPower:
						Managers.Game.TouchLightningPower += value;
						break;
					
					case UpgradeStatType.AttackProportionalTouchLightningAdditionalDamage:
						Managers.Game.AttackProportionalTouchLightningAdditionalDamage += value;
						break;
					
					case UpgradeStatType.MaxHP:
						Managers.Game.MaxHP += value;
						break;
					
					case UpgradeStatType.HPRegen:
						Managers.Game.HPRegen += value;
						break;
					
					case UpgradeStatType.AttackSpeed:
						Managers.Game.AttackSpeed += value;
						break;
					
					case UpgradeStatType.CriticalRate:
						Managers.Game.CriticalRate += value;
						break;
					
					case UpgradeStatType.CriticalDamageAmplification:
						Managers.Game.CriticalDamageAmplification += value;
						break;
				}
				Managers.Sound.Play(Sound.Effect, "Sound_UpgradeDone");

				RefreshUI();

				Managers.UI.FindPopup<UI_PlayPopup>()?.RefreshStat();
				Managers.UI.FindPopup<UI_PlayPopup>()?.RefreshMoney();

			}

			_coolTime = StartCoroutine(CoStartUpgradeCoolTime(0.1f));
		}
	}

	private int GetIncreasedValue()
	{
		int value = Utils.GetStatValue(_upgradeStatType);

		return value + _statData.increaseStat;
	}
	private void OnPointerUp()
	{
		if (_coolTime != null)
		{
			StopCoroutine(_coolTime);
			_coolTime = null;
		}
	}

	private bool CanUpgrade()
	{
		return Managers.Game.Money >= _statData.price;
	}

	IEnumerator CoStartUpgradeCoolTime(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		_coolTime = null;
	}
}
