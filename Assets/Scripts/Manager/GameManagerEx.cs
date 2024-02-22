using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using System.IO;

[Serializable]
public class PlayerState
{
	public AnimState state = AnimState.None;
}
public class GameData
{
	public int ServerNum;
	public string Name;

	#region 공격
	public int BaseAttackPower;									// 기본 공격력
	public int PvpAttackPower;									// Pvp 공격력
	public int CollectAttackPower;								// 수집품 공격력
	public int CannonAttackPower;								// 대포 공격력
	public float AttackAmplification;							// 공격력 증폭
	public float SkillDamageAmplification;						// 스킬 피해 증폭
	public int NormalDamageAmplification;						// 일반 피해 증폭
	public int FishDamageAmplification;							// 생선 피해 증폭
	public int TotalDamageIncrease;								// 모든 피해량 증가
	public int TouchLightningPower;								// 터치번개 피해
	public int AttackProportionalTouchLightningAdditionalDamage;// 공격력 비례 터치번개 추가 피해
	  #endregion

	#region 방어_회복
	public int HP;
	public int MaxHP;											// 최대 체력	
	public int HPRegen;											// HP 회복
	public int MaxHPAmplification;								// 최대 체력 증폭
  #endregion

	#region 명중_치명타
	public int CriticalRate;									// 치명타 확률
	public int CriticalDamageAmplification;						// 치명타 피해 증폭
  #endregion

	#region 속도_생산_기타
	public int AttackSpeed;										// 공격 속도
	public int MakeCollectionLevel;								// 기본 생산 레벨
	public int MaxCollectionLevel;								// 최대 생산 레벨

	#region 재화
	public int Money;
	public int Diamond;
	#endregion

	public int Stage;

	#region 수집품
	public List<int> CollectItems;
  #endregion
  #endregion
}


public class GameManagerEx
{
	GameData _gameData = new GameData();
	
	public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

	#region 공격
	public int ServerNum 
	{
		get { return _gameData.ServerNum; }
		set { _gameData.ServerNum = value; }
	}

	public string Name
	{
		get { return _gameData.Name; }
		set { _gameData.Name = value; }
	}
	
	public int BaseAttackPower
	{
		get { return _gameData.BaseAttackPower; }
		set { _gameData.BaseAttackPower = value; }
	}
	
	public int PvpAttackPower
	{
		get { return _gameData.PvpAttackPower; }
		set { _gameData.PvpAttackPower = value; }
	}
	
	public int CollectAttackPower
	{
		get { return _gameData.CollectAttackPower; }
		set { _gameData.CollectAttackPower = value; }
	}
	
	public int CannonAttackPower
	{
		get { return _gameData.CannonAttackPower; }
		set { _gameData.CannonAttackPower = value; }
	}
	
	public float AttackAmplification
	{
		get { return _gameData.AttackAmplification; }
		set { _gameData.AttackAmplification = value; }
	}
	
	public float SkillDamageAmplification
	{
		get { return _gameData.SkillDamageAmplification; }
		set { _gameData.SkillDamageAmplification = value; }
	}
	
	public int NormalDamageAmplification
	{
		get { return _gameData.NormalDamageAmplification; }
		set { _gameData.NormalDamageAmplification = value; }
	}

	public int FishDamageAmplification
	{
		get { return _gameData.FishDamageAmplification; }
		set { _gameData.FishDamageAmplification = value; }
	}

	public int TotalDamageIncrease
	{
		get { return _gameData.TotalDamageIncrease; }
		set { _gameData.TotalDamageIncrease = value; }
	}
	
	public int TouchLightningPower
	{
		get { return _gameData.TouchLightningPower; }
		set { _gameData.TouchLightningPower = value; }
	}
	
	public int AttackProportionalTouchLightningAdditionalDamage
	{
		get { return _gameData.AttackProportionalTouchLightningAdditionalDamage; }
		set { _gameData.AttackProportionalTouchLightningAdditionalDamage = value; }
	}
	
	
	
  #endregion

	#region 방어_회복
	public int HP
	{
		get { return _gameData.HP; }
		set { _gameData.HP = value; }
	}
	public int MaxHP
	{
		get { return _gameData.MaxHP; }
		set { _gameData.MaxHP = value; }
	}
	public int HPRegen
	{
		get { return _gameData.HPRegen; }
		set { _gameData.HPRegen = value; }
	}
	public int MaxHPAmplification
	{
		get { return _gameData.MaxHPAmplification; }
		set { _gameData.MaxHPAmplification = value; }
	}
	
  #endregion

	#region 명중_치명타
	public int CriticalRate
	{
		get { return _gameData.CriticalRate; }
		set { _gameData.CriticalRate = value; }
	}
	public int CriticalDamageAmplification
	{
		get { return _gameData.CriticalDamageAmplification; }
		set { _gameData.CriticalDamageAmplification = value; }
	}
	
  #endregion

	#region 속도_생산_기타
	public int AttackSpeed
	{
		get { return _gameData.AttackSpeed; }
		set { _gameData.AttackSpeed = value; }
	}

	public int MakeCollectionLevel
	{
		get { return _gameData.MakeCollectionLevel; }
		set { _gameData.MakeCollectionLevel = value; }
	}

	public int MaxCollectionLevel
	{
		get { return _gameData.MaxCollectionLevel; }
		set { _gameData.MaxCollectionLevel = value; }
	}
  #endregion

	#region 재화
	public int Money
	{
		get { return _gameData.Money; }
		set { _gameData.Money = value; }
	}
	public int Diamond
	{
		get { return _gameData.Diamond; }
		set { _gameData.Diamond = value; }
	}

	public int Stage
	{
		get { return _gameData.Stage; }
		set { _gameData.Stage = value; }
	}
  #endregion

	#region 생산품
	public List<int> CollectItems
	{
		get{ return _gameData.CollectItems; }
		set{ _gameData.CollectItems = value; }
	}
  #endregion
	public void Init()
	{
		StartData data = Managers.Data.Start;
		//실제 서비스용
		if(LoadGame() == false)
		{
			ServerNum = data.ServerNum;
			Name = "Player";
		
			BaseAttackPower = 10;
			PvpAttackPower = 0;
			CollectAttackPower = 10;
			CannonAttackPower = 10;
			AttackAmplification = 0;
			SkillDamageAmplification = 0;
			NormalDamageAmplification = 0;
			FishDamageAmplification = 0;
			TotalDamageIncrease = 0;
			TouchLightningPower = 20;
			AttackProportionalTouchLightningAdditionalDamage = 0;
			MaxHP = 100;
			HPRegen = 1;
			MaxHPAmplification = 0;
			CriticalRate = 0;
			CriticalDamageAmplification = 0;
			AttackSpeed = 1;
			MakeCollectionLevel = 1;
			MaxCollectionLevel = 1;
			Money = 150;
			Diamond = 0;
			Stage = 0;
			CollectItems = new List<int>();
		}
		
		//테스트용
		// ServerNum = data.ServerNum;
		// Name = "Player";
		//
		// BaseAttackPower = 10;
		// PvpAttackPower = 0;
		// CollectAttackPower = 10;
		// CannonAttackPower = 10;
		// AttackAmplification = 0;
		// SkillDamageAmplification = 0;
		// NormalDamageAmplification = 0;
		// FishDamageAmplification = 0;
		// TotalDamageIncrease = 0;
		// TouchLightningPower = 20;
		// AttackProportionalTouchLightningAdditionalDamage = 0;
		// MaxHP = 100;
		// HPRegen = 1;
		// MaxHPAmplification = 0;
		// CriticalRate = 0;
		// CriticalDamageAmplification = 0;
		// AttackSpeed = 1;
		// MakeCollectionLevel = 1;
		// MaxCollectionLevel = 1;
		// Money = 150;
		// Diamond = 0;
		// Stage = 0;
		// CollectItems = new List<int>();
		
		HP = MaxHP;
		SaveGame();

	}
	#region Save_Load
	public string _path = Application.persistentDataPath + "/SaveData.json";
	public void SaveGame()
	{
		string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
		File.WriteAllText(_path, jsonStr);
	}

	public bool LoadGame()
	{
		if (File.Exists(_path) == false)
			return false;

		string fileStr = File.ReadAllText(_path);
		GameData data = JsonUtility.FromJson<GameData>(fileStr);

		if (data != null)
		{
			Managers.Game.SaveData = data;
			if (Managers.Data.Collections.TryGetValue(Managers.Game.MaxCollectionLevel, out CollectData collectData) == true)
				Managers.Game.CollectAttackPower = collectData.CollectDamage;

			CollectItems.Sort((a,b) => b.CompareTo(a));
		}

		return true;
	}
	
	#endregion
}
