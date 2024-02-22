using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerInfoPopup : UI_Popup
{
	enum Texts
	{
		BaseAttackPowerText,
		BaseAttackPowerValueText,
		PvpAttackPowerText,
		PvpAttackPowerValueText,
		CollectAttackPowerText,
		CollectAttackPowerValueText,
		CannonAttackPowerText,
		CannonAttackPowerValueText,
		AttackAmplificationText,
		AttackAmplificationValueText,
		SkillDamageAmplificationText,
		SkillDamageAmplificationValueText,
		NormalDamageAmplificationText,
		NormalDamageAmplificationValueText,
		FishDamageAmplificationText,
		FishDamageAmplificationValueText,
		TotalDamageIncreaseText,
		TotalDamageIncreaseValueText,
		TouchLightningPowerText,
		TouchLightningPowerValueText,
		AttackProportionalTouchLightningAdditionalDamageText,
		AttackProportionalTouchLightningAdditionalDamageValueText,
		
		MaxHPText,
		MaxHPValueText,
		HPRegenText,
		HPRegenValueText,
		MaxHPAmplificationText,
		MaxHPAmplificationValueText,
		
		CriticalRateText,
		CriticalRateValueText,
		CriticalDamageAmplificationText,
		CriticalDamageAmplificationValueText,
		
		AttackSpeedText,
		AttackSpeedValueText,
	}

	enum Image
	{
		BoxImage
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;
		
		BindText(typeof(Texts));
		BindImage(typeof(Image));
		
		GetImage((int)Image.BoxImage).gameObject.BindEvent(OnClosePopup);

		RefreshUI();
		return true;
	}

	public void RefreshUI()
	{
		if (_init == false)
			return;

		GetText((int)Texts.BaseAttackPowerText).text = Managers.GetText(Define.BasePowerText);
		GetText((int)Texts.BaseAttackPowerValueText).text = $"{Managers.Game.BaseAttackPower}";
		GetText((int)Texts.PvpAttackPowerText).text = Managers.GetText(Define.PvpPowerText);
		GetText((int)Texts.PvpAttackPowerValueText).text = $"{Managers.Game.PvpAttackPower}";
		GetText((int)Texts.CollectAttackPowerText).text = Managers.GetText(Define.CollectPowerText);
		GetText((int)Texts.CollectAttackPowerValueText).text = $"{Managers.Game.CollectAttackPower}";
		GetText((int)Texts.CannonAttackPowerText).text = Managers.GetText(Define.CannonPowerText);
		GetText((int)Texts.CannonAttackPowerValueText).text = $"{Managers.Game.CannonAttackPower}";
		GetText((int)Texts.AttackAmplificationText).text = Managers.GetText(Define.AttackAmplificationText);
		GetText((int)Texts.AttackAmplificationValueText).text = $"{Managers.Game.AttackAmplification}";
		GetText((int)Texts.SkillDamageAmplificationText).text = Managers.GetText(Define.SkillDamageAmplificationText);
		GetText((int)Texts.SkillDamageAmplificationValueText).text = $"{Managers.Game.SkillDamageAmplification}";
		GetText((int)Texts.NormalDamageAmplificationText).text = Managers.GetText(Define.NormalDamageAmplificationText);
		GetText((int)Texts.NormalDamageAmplificationValueText).text = $"{Managers.Game.NormalDamageAmplification}";
		GetText((int)Texts.FishDamageAmplificationText).text = Managers.GetText(Define.FishDamageAmplificationText);
		GetText((int)Texts.FishDamageAmplificationValueText).text = $"{Managers.Game.FishDamageAmplification}";
		GetText((int)Texts.TotalDamageIncreaseText).text = Managers.GetText(Define.TotalDamageIncreaseText);
		GetText((int)Texts.TotalDamageIncreaseValueText).text = $"{Managers.Game.TotalDamageIncrease}";
		GetText((int)Texts.TouchLightningPowerText).text = Managers.GetText(Define.TouchLightningPowerText);
		GetText((int)Texts.TouchLightningPowerValueText).text = $"{Managers.Game.TouchLightningPower}";
		GetText((int)Texts.AttackProportionalTouchLightningAdditionalDamageText).text = Managers.GetText(Define.AttackProportionalTouchLightningAdditionalDamageText);
		GetText((int)Texts.AttackProportionalTouchLightningAdditionalDamageValueText).text = $"{Managers.Game.AttackProportionalTouchLightningAdditionalDamage}";
		GetText((int)Texts.MaxHPText).text = Managers.GetText(Define.MaxHPText);
		GetText((int)Texts.MaxHPValueText).text = $"{Managers.Game.MaxHP}";
		GetText((int)Texts.HPRegenText).text = Managers.GetText(Define.HPRegenText);
		GetText((int)Texts.HPRegenValueText).text = $"{Managers.Game.HPRegen}";
		GetText((int)Texts.MaxHPAmplificationText).text = Managers.GetText(Define.MaxHPAmplificationText);
		GetText((int)Texts.MaxHPAmplificationValueText).text = $"{Managers.Game.MaxHPAmplification}";
		GetText((int)Texts.CriticalRateText).text = Managers.GetText(Define.CriticalRateText);
		GetText((int)Texts.CriticalRateValueText).text = $"{Managers.Game.CriticalRate}";
		GetText((int)Texts.CriticalDamageAmplificationText).text = Managers.GetText(Define.CriticalDamageAmplificationText);
		GetText((int)Texts.CriticalDamageAmplificationValueText).text = $"{Managers.Game.CriticalDamageAmplification}";
		GetText((int)Texts.AttackSpeedText).text = Managers.GetText(Define.AttackSpeedText);
		GetText((int)Texts.AttackSpeedValueText).text = $"{Managers.Game.AttackSpeed}";
	}

	private void OnClosePopup()
	{
		Debug.Log("OnClosePopup");
		Managers.UI.ClosePopupUI(this);
	}
}
