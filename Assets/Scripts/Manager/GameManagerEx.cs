using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
	public int ServerNum;
	public string Name;
	public int BaseAttackPower;  // 기본 공격력
	public int PvpAttackPower;  // Pvp 공격력
	public int FishAttackPower;  // 생선 공격력
	public int CannonAttackPower;  // 대포 공격력
	public float AttackAmplification;  // 공격력 증폭
	public float SkillDamageAmplification;  // 스킬 피해 증폭
	public int NormalDamageAmplification;  // 일반 피해 증폭
	public int FishDamageAmplification;  // 생선 피해 증폭
	public int TotalDamageIncrease;  // 모든 피해량 증가
	public int TouchLightningDamage;  // 터치번개 피해
	public int AttackProportionalTouchLightningAdditionalDamage;  // 공격력 비례 터치번개 추가 피해
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
	
	public int FishAttackPower
	{
		get { return _gameData.FishAttackPower; }
		set { _gameData.FishAttackPower = value; }
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
	
	public int TouchLightningDamage
	{
		get { return _gameData.TouchLightningDamage; }
		set { _gameData.TouchLightningDamage = value; }
	}
	
	public int AttackProportionalTouchLightningAdditionalDamage
	{
		get { return _gameData.AttackProportionalTouchLightningAdditionalDamage; }
		set { _gameData.AttackProportionalTouchLightningAdditionalDamage = value; }
	}
	
  #endregion
}
