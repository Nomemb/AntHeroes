using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class StageData
{
	[XmlAttribute]
	public int ID;
	[XmlAttribute]
	public int nameID;
	[XmlAttribute]
	public float enhancementStatus;
	[XmlAttribute]
	public float enhancementReward;
	[XmlArray]
	public List<StageEnemyData> enemyData = new List<StageEnemyData>();
}

public class StageEnemyData
{
	[XmlAttribute]
	public int enemyID;
}

[Serializable, XmlRoot("ArrayOfStageData")]
public class StageDataLoader : ILoader<int, StageData>
{
	[XmlElement("StageData")]
	public List<StageData> _StageDatas = new List<StageData>();

	public Dictionary<int, StageData> MakeDic()
	{
		Dictionary<int, StageData> dic = new Dictionary<int, StageData>();

		foreach (StageData data in _StageDatas)
			dic.Add(data.ID, data);

		return dic;
	}
}
