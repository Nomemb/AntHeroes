using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static Define;

public class StatData
{
	[XmlAttribute]
	public int ID;					// 스탯 번호
	[XmlAttribute]
	public UpgradeStatType type;	// 이름
	[XmlAttribute]
	public int nameID;				// 이름 아이디
	[XmlAttribute]
	public int price;				// 가격
	[XmlAttribute]
	public int increaseStat;		// 능력치 상승치
	[XmlAttribute]
	public float increasePriceRate; // 업그레이드 비용 상승률
}

[Serializable, XmlRoot("ArrayOfStatData")]
public class StatDataLoader : ILoader<int, StatData>
{
	[XmlElement("StatData")]
	public List<StatData> StatDatas = new List<StatData>();

	public Dictionary<int, StatData> MakeDic()
	{
		Dictionary<int, StatData> dic = new Dictionary<int, StatData>();

		foreach (StatData data in StatDatas)
			dic.Add(data.ID, data);

		return dic;
	}
}
