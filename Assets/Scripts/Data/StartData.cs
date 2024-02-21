using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class StartData
{
	[XmlAttribute]
	public int ServerNum;
	[XmlAttribute]
	public string Name;
	[XmlAttribute]
	public int ID;
	[XmlAttribute]
	public int BaseAttackPower;									// 기본 공격력
	[XmlAttribute]
	public int PvpAttackPower;									// Pvp 공격력
	[XmlAttribute]
	public int FishAttackPower;									// 생선 공격력
	[XmlAttribute]
	public int CannonAttackPower;								// 대포 공격력
	[XmlAttribute]
	public float AttackAmplification;							// 공격력 증폭
	[XmlAttribute]
	public float SkillDamageAmplification;						// 스킬 피해 증폭
	[XmlAttribute]
	public int NormalDamageAmplification;						// 일반 피해 증폭
	[XmlAttribute]
	public int FishDamageAmplification;							// 생선 피해 증폭
	[XmlAttribute]
	public int TotalDamageIncrease;								// 모든 피해량 증가
	[XmlAttribute]
	public int TouchLightningPower;								// 터치번개 피해
	[XmlAttribute]
	public int AttackProportionalTouchLightningAdditionalDamage;// 공격력 비례 터치번개 추가 피해

	[XmlAttribute]
	public int HP;
	[XmlAttribute]
	public int MaxHP;											// 최대 체력	
	[XmlAttribute]
	public int HPRegen;											// HP 회복
	[XmlAttribute]
	public int MaxHPAmplification;								// 최대 체력 증폭


	[XmlAttribute]
	public int CriticalRate;									// 치명타 확률
	[XmlAttribute]
	public int CriticalDamageAmplification;						// 치명타 피해 증폭
	
	[XmlAttribute]
	public int AttackSpeed;										// 공격 속도

	[XmlAttribute]
	public int Money;
	[XmlAttribute]
	public int Diamond;
	[XmlAttribute]
	public int Stage;
}
