using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public interface ILoader<Key, Item>
{
	Dictionary<Key, Item> MakeDic();
}
public class DataManager
{
	public StartData Start { get; private set; }
	
	public Dictionary<int, StatData> Stats { get; private set; }
	public Dictionary<int, PlayerData> Players { get; private set; }

	public Dictionary<int, TextData> Texts { get; private set; }

	public void Init()
	{
		Start = LoadSingleXml<StartData>("StartData");
		
		Stats = LoadXml<StatDataLoader, int, StatData>("StatData").MakeDic();
		Players = LoadXml<PlayerDataLoader, int, PlayerData>("PlayerData").MakeDic();
		Texts = LoadXml<TextDataLoader, int, TextData>("TextData").MakeDic();
	}

	/// <summary>
	/// XML을 읽고 지정 타입 객체로 변환
	/// </summary>
	/// <param name="name">파일 이름</param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	private T LoadSingleXml<T>(string name)
	{
		XmlSerializer xs = new XmlSerializer(typeof(T));
		TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
		using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
			return (T)xs.Deserialize(stream);
	}

	private Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
	{
		XmlSerializer xs = new XmlSerializer(typeof(Loader));
		TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
		using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
			return (Loader)xs.Deserialize(stream);
	}
}
