using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_PlayPopup : UI_Popup
{
	enum Texts
	{
		NameText,
		MoneyBarText,
		DiamondBarText,
		MaxCollectLevelText,
		MakeCollectLevelText,
		TrainingButtonText,
		HeroButtonText,
		TowerButtonText,
		SummonButtonText,
		ShopButtonText,
	}

	enum Buttons
	{
		PlayerInfoButton,
		TrainingButton,
		HeroButton,
		TowerButton,
		SummonButton,
		ShopButton,
		MenuButton,
		MaxCollectLevelButton,
		MakeCollectLevelButton,
		GenerateButton,
		SortButton,
		AutoGenerateButton,
	}

	enum Images
	{
		DiamondBarFill,
		MoneyBarFill,
		NoticeImage,
		MaxCollectLevelImage,
		MakeCollectLevelImage,
		
		TrainingBox,
		HeroBox,
		TowerBox,
		SummonBox,
		ShopBox,
	}

	enum GameObjects
	{
		CollectItemBowl,
		TrainingTab,
		HeroTab,
		TowerTab,
		SummonTab,
		ShopTab,

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

	public enum Button
	{
		MaxCollectLevelButton,
		MakeCollectLevelButton,
	}

	// 업그레이드 능력치
	enum TrainingItems
	{
		UI_TrainingItem_BaseAttackPower,
		UI_TrainingItem_TouchLightningPower,
		UI_TrainingItem_AttackProportionalTouchLightningAdditionalDamage,
		UI_TrainingItem_MaxHP,
		UI_TrainingItem_HPRegen,
		UI_TrainingItem_AttackSpeed,
		UI_TrainingItem_CriticalRate,
		UI_TrainingItem_CriticalDamageIncrease,
	}
	PlayTab _tab = PlayTab.None;
	UI_CollectItemBowl _collectItemBowl;

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
		Bind<UI_TrainingItem>(typeof(TrainingItems));

		GetText((int)Texts.NameText).text = _game.Name;
		GetButton((int)Buttons.TrainingButton).gameObject.BindEvent(()=> ShowTab(PlayTab.Training));
		GetButton((int)Buttons.HeroButton).gameObject.BindEvent(()=> ShowTab(PlayTab.Hero));
		GetButton((int)Buttons.TowerButton).gameObject.BindEvent(()=> ShowTab(PlayTab.Tower));
		GetButton((int)Buttons.SummonButton).gameObject.BindEvent(()=> ShowTab(PlayTab.Summon));
		GetButton((int)Buttons.ShopButton).gameObject.BindEvent(()=> ShowTab(PlayTab.Shop));
		
		GetButton((int)Buttons.PlayerInfoButton).gameObject.BindEvent(OnClickPlayerInfoButton);
		GetButton((int)Buttons.GenerateButton).gameObject.BindEvent(OnClickGenerateButton);
		GetButton((int)Buttons.SortButton).gameObject.BindEvent(OnClickSortButton);
		
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_BaseAttackPower).SetInfo(UpgradeStatType.BaseAttackPower);
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_TouchLightningPower).SetInfo(UpgradeStatType.TouchLightningPower);
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_AttackProportionalTouchLightningAdditionalDamage).SetInfo(UpgradeStatType.AttackProportionalTouchLightningAdditionalDamage);
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_MaxHP).SetInfo(UpgradeStatType.MaxHP);
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_HPRegen).SetInfo(UpgradeStatType.HPRegen);
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_AttackSpeed).SetInfo(UpgradeStatType.AttackSpeed);
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_CriticalRate).SetInfo(UpgradeStatType.CriticalRate);
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_CriticalDamageIncrease).SetInfo(UpgradeStatType.CriticalDamageAmplification);

		_collectItemBowl = GetObject((int)GameObjects.CollectItemBowl).GetComponent<UI_CollectItemBowl>();
		RefreshUI();
		StartCoroutine(SaveGame(3.0f));
		Managers.Sound.Play(Sound.Bgm, "Sound_Battle", volume: 0.2f);
		ShowTab(PlayTab.None);


		Managers.UI.ShowPopupUI<UI_BattlePopup>();
		return true;
	}

	/// <summary>
	/// 플레이어가 각 탭 눌렀을 때 최상단에 해당 탭을 띄움
	/// </summary>
	/// <param name="tab"></param>
	public void ShowTab(PlayTab tab)
	{
		if (_tab == tab)
			_tab = PlayTab.None;
		else
			_tab = tab;
		
		// 모든 탭 비활성화
		GetObject((int)GameObjects.TrainingTab).gameObject.SetActive(false);
		GetObject((int)GameObjects.HeroTab).gameObject.SetActive(false);
		GetObject((int)GameObjects.TowerTab).gameObject.SetActive(false);
		GetObject((int)GameObjects.SummonTab).gameObject.SetActive(false);
		GetObject((int)GameObjects.ShopTab).gameObject.SetActive(false);
		
		// 버튼 스프라이트 불러옴
		GetButton((int)Buttons.TrainingButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_05");
		GetButton((int)Buttons.HeroButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_06");
		GetButton((int)Buttons.TowerButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_07");
		GetButton((int)Buttons.SummonButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_06");
		GetButton((int)Buttons.ShopButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_08");
		
		// 박스 이미지 불러옴
		GetImage((int)Images.TrainingBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_04");
		GetImage((int)Images.HeroBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_04");
		GetImage((int)Images.TowerBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_04");
		GetImage((int)Images.SummonBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_04");
		GetImage((int)Images.ShopBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_04");

		switch (_tab)
		{
			case PlayTab.None:
				Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
				break;
			
			case PlayTab.Training:
				Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
				GetObject((int)GameObjects.TrainingTab).gameObject.SetActive(true);
				GetObject((int)GameObjects.TrainingTab).GetComponent<ScrollRect>().ResetVertical();
				GetButton((int)Buttons.TrainingButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_18");
				GetImage((int)Images.TrainingBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_12");
				break;
			
			case PlayTab.Hero:
				Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
				GetObject((int)GameObjects.HeroTab).gameObject.SetActive(true);
				GetButton((int)Buttons.HeroButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_19");
				GetImage((int)Images.HeroBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_12");
				break;
			
			case PlayTab.Tower:
				Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
				GetObject((int)GameObjects.TowerTab).gameObject.SetActive(true);
				GetButton((int)Buttons.TowerButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_20");
				GetImage((int)Images.TowerBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_12");
				break;
			
			case PlayTab.Summon:
				Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
				GetObject((int)GameObjects.SummonTab).gameObject.SetActive(true);
				GetButton((int)Buttons.SummonButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_19");
				GetImage((int)Images.SummonBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_12");
				break;
			
			case PlayTab.Shop:
				Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
				GetObject((int)GameObjects.ShopTab).gameObject.SetActive(true);
				GetButton((int)Buttons.ShopButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_21");
				GetImage((int)Images.ShopBox).sprite = Managers.Resource.Load<Sprite>("Sprites/Main/Common/btn_12");
				break;
			
		}

	}

	public void RefreshUI()
	{
		if (_init == false)
			return;
		
		ShowTab(_tab);
		RefreshStat();
		RefreshMoney();
		RefreshDiamond();
	}

	public void RefreshStat()
	{
		RefreshUpgradeButton();
	}
	
	public void RefreshMaxLevel()
	{
		GetImage((int)Images.MaxCollectLevelImage).color = Utils.HexColor(_collectItemBowl.MaxLevelData.Hex);
		GetText((int)Texts.MaxCollectLevelText).text = $"최대 레벨 : {_collectItemBowl.MaxLevelData.Level}";
	}
	public void RefreshMakeLevel()
	{
		GetImage((int)Images.MakeCollectLevelImage).color = Utils.HexColor(_collectItemBowl.MakeLevelData.Hex);
		GetText((int)Texts.MakeCollectLevelText).text = $"생산 레벨 : {_collectItemBowl.MakeLevelData.Level}";
	}
	

	public void RefreshMoney(bool playSoundAndEffect = false)
	{
		if (GetText((int)Texts.MoneyBarText).text != Utils.GetMoneyString(_game.Money))
		{
			if (playSoundAndEffect)
			{
				Managers.Sound.Play(Sound.Effect, "Sound_Coin");
			}
			GetText((int)Texts.MoneyBarText).text = Utils.GetMoneyString(_game.Money);
		}
	}
	
	public void RefreshDiamond(bool playSoundAndEffect = false)
	{
		if (GetText((int)Texts.DiamondBarText).text != Utils.GetMoneyString(_game.Diamond))
		{
			if (playSoundAndEffect)
			{
				Managers.Sound.Play(Sound.Effect, "Sound_Coin");
			}
			GetText((int)Texts.DiamondBarText).text = Utils.GetMoneyString(_game.Diamond);
		}
	}

	/// <summary>
	/// 업그레이드 버튼들 정보 갱신
	/// </summary>
	private void RefreshUpgradeButton()
	{
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_BaseAttackPower).RefreshUI();
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_TouchLightningPower).RefreshUI();
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_AttackProportionalTouchLightningAdditionalDamage).RefreshUI();
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_MaxHP).RefreshUI();
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_HPRegen).RefreshUI();
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_AttackSpeed).RefreshUI();
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_CriticalRate).RefreshUI();
		Get<UI_TrainingItem>((int)TrainingItems.UI_TrainingItem_CriticalDamageIncrease).RefreshUI();
	}

	private void OnClickPlayerInfoButton()
	{
		Managers.Sound.Play(Sound.Effect, "Sound_FolderItemClick");
		Managers.UI.ShowPopupUI<UI_PlayerInfoPopup>();
	}

	private void OnClickGenerateButton()
	{
		_collectItemBowl.GenerateCollectItem();
	}

	private void OnClickSortButton()
	{
		_collectItemBowl.SortCollectItems();
	}
	IEnumerator SaveGame(float interval = 1.0f)
	{
		while (true)
		{
			yield return new WaitForSeconds(interval);
			Managers.Game.SaveGame();
		}
	}
}
