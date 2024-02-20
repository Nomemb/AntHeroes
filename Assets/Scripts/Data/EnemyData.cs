using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyData
{
	[XmlAttribute]
	public int ID;
	[XmlAttribute]
	public int nameID;
	[XmlAttribute]
	public string illustPath;
	[XmlAttribute]
	public string battleIconPath;
	[XmlAttribute]
	public string spine;
	[XmlAttribute]
	public string aniIdle;
	[XmlAttribute]
	public string aniIdleSkin;
	[XmlAttribute]
	public string aniWorking;
	[XmlAttribute]
	public string aniWorkingSkin;
	[XmlAttribute]
	public string aniAttack;
	[XmlAttribute]
	public string aniAttackSkin;
	[XmlAttribute]
	public string aniWalk;
	[XmlAttribute]
	public string aniWalkSkin;
	[XmlAttribute]
	public string aniSweat;
	[XmlAttribute]
	public string aniSweatSkin;
	[XmlAttribute]
	public int maxhp;
	[XmlAttribute]
	public int atk;
	[XmlAttribute]
	public string promotion;
}

[Serializable, XmlRoot("ArrayOfEnemyData")]
public class EnemyDataLoader : ILoader<int, EnemyData>
{
	[XmlElement("EnemyData")]
	public List<EnemyData> _EnemyDatas = new List<EnemyData>();

	public Dictionary<int, EnemyData> MakeDic()
	{
		Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();

		foreach (EnemyData data in _EnemyDatas)
			dic.Add(data.ID, data);

		return dic;
	}
}
