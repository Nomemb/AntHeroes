using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CollectData
{
	[XmlAttribute]
	public int Level;
	[XmlAttribute]
	public int CollectDamage;
	[XmlAttribute]
	public int Reward;
	[XmlAttribute]
	public string Hex;
}

[Serializable, XmlRoot("ArrayOfCollectData")]
public class CollectDataLoader : ILoader<int, CollectData>
{
	[XmlElement("CollectData")]
	public List<CollectData> _CollectData = new List<CollectData>();

	public Dictionary<int, CollectData> MakeDic()
	{
		Dictionary<int, CollectData> dic = new Dictionary<int, CollectData>();

		foreach (CollectData data in _CollectData)
			dic.Add(data.Level, data);

		return dic;
	}
}

