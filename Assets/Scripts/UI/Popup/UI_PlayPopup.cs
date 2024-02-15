using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayPopup : UI_Popup
{
	enum Texts
	{
		NameText,
		MoneyBarText,
		DiamondBarText,
		TrainingButtonText,
		HeroButtonText,
		TowerButtonText,
		SummonButtonText,
		ShopButtonText,
	}

	enum Buttons
	{
		TrainingButton,
		HeroButton,
		TowerButton,
		SummonButton,
		ShopButton,
		MenuButton,
		MaxFishButton,
		CurFishButton,
		GenerateButton,
		SortButton,
		AutoGenerateButton,
	}

	enum Images
	{
		DiamondBarFill,
		MoneyBarFill,
		NoticeImage,
		TrainingBox,
		HeroBox,
		TowerBox,
		SummonBox,
		ShopBox,
		MenuBox,
		
	}

	enum GameObjects
	{
		TrainingTab,
		HeroTab,
		TowerTab,
		SummonTab,
		ShopTab,
		MenuTab,
		
	}

	public enum PlayTab
	{
		None,
		Training,
		Hero,
		Tower,
		Summon,
		Shop
	}

	// 업그레이드 능력치
	enum TrainingItems
	{
		
	}
	PlayTab _tab = PlayTab.None;

	GameManagerEx _game;
	public override bool Init()
	{
		if (base.Init() == false)
			return false;
		
		_game = Managers.Game;
		
		BindText(typeof(Texts));
		BindButton(typeof(Buttons));
		BindObject(typeof(GameObjects));
		BindImage(typeof(Images));

		
		return true;
	}
}
