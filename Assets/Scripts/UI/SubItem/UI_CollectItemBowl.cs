using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CollectItemBowl : UI_Base
{
	[SerializeField]
	private List<GameObject> _collectItems = new List<GameObject>();
	
	int _capacity = 40;
	CollectData _maxLevelData;
	CollectData _makeLevelData;
	
	public CollectData MaxLevelData
	{
		get { return _maxLevelData; }
	}
	public CollectData MakeLevelData
	{
		get { return _makeLevelData; }
	}
	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		if (Managers.Game.CollectItems.Count == 0)
		{
			for (int i = 0; i < _capacity; ++i)
			{
				GameObject go = Managers.Resource.Instantiate("UI/SubItem/UI_CollectItem", transform);
				go.GetComponent<UI_CollectItem>().SetInfo(0);
				go.name = $"UI_CollectItem_{i}";
				Managers.Game.CollectItems.Add(0);
				_collectItems.Add(go);
			}
		}
		else
		{
			for (int i = 0; i < Managers.Game.CollectItems.Count; i++)
			{
				GameObject go = Managers.Resource.Instantiate("UI/SubItem/UI_CollectItem", transform);
				go.GetComponent<UI_CollectItem>().SetInfo(Managers.Game.CollectItems[i]);
				go.name = $"UI_CollectItem_{i}";
				_collectItems.Add(go);
			}
		}

		Managers.Data.Collections.TryGetValue(Managers.Game.MaxCollectionLevel, out _maxLevelData);
		Managers.Data.Collections.TryGetValue(Managers.Game.MakeCollectionLevel, out _makeLevelData);
		
		Managers.UI.FindPopup<UI_PlayPopup>().RefreshMaxLevel();
		Managers.UI.FindPopup<UI_PlayPopup>().RefreshMakeLevel();

		return true;
	}

	public bool MergeCollectItem(int index, int level)
	{
		UI_CollectItem indexItem = _collectItems[index].GetComponent<UI_CollectItem>();
		
		int mergeIndex = _collectItems.Count - 1;
		for (int i = 0; i < _collectItems.Count; i++)
		{
			if(i == index)
				continue;
			
			UI_CollectItem mergeItem = _collectItems[i].GetComponent<UI_CollectItem>();
			if (mergeItem.CollectData != null)
			{
				if (mergeItem.GetCollectLevel() == level)
				{
					mergeIndex = i;
					// 무조건 뒤에 있는 인덱스를 없애기 위해
					if (index > mergeIndex)
					{
						(indexItem, mergeItem) = (mergeItem, indexItem);
						(index, mergeIndex) = (mergeIndex, index);
					}
					int nextLevel = level + 1;
					
					// 최대 레벨 갱신 시 정보 업데이트
					if (nextLevel > Managers.Game.MaxCollectionLevel)
					{
						Managers.Game.MaxCollectionLevel = nextLevel;
						if (Managers.Data.Collections.TryGetValue(nextLevel, out _maxLevelData) == true)
						{
							Managers.Game.CollectAttackPower = _maxLevelData.CollectDamage;
							Managers.UI.FindPopup<UI_PlayPopup>().RefreshMaxLevel();
						};
					}
					indexItem.SetInfo(nextLevel);
					mergeItem.SetInfo(0);

					Managers.Game.CollectItems[index] = nextLevel;
					Managers.Game.CollectItems[mergeIndex] = 0;
					
					return true;
				}
				
			}
		}
		
		return false;
	}

	public bool GenerateCollectItem()
	{
		for (int i = 0; i < _capacity; i++)
		{
			if (_collectItems[i].GetComponent<UI_CollectItem>().CollectData == null)
			{
				_collectItems[i].GetComponent<UI_CollectItem>().SetInfo(Managers.Game.MakeCollectionLevel);
				Managers.Game.CollectItems[i] = Managers.Game.MakeCollectionLevel;
				return true;
			}
		}
		return false;
	}

	public void SortCollectItems()
	{
		for (int i = 0; i < _capacity; ++i)
		{
			Destroy(_collectItems[i].gameObject);
		}
		_collectItems.Clear();
		
		Managers.Game.CollectItems.Sort((a,b) => b.CompareTo(a));
		
		for (int i = 0; i < Managers.Game.CollectItems.Count; i++)
		{
			GameObject go = Managers.Resource.Instantiate("UI/SubItem/UI_CollectItem", transform);
			go.GetComponent<UI_CollectItem>().SetInfo(Managers.Game.CollectItems[i]);
			go.name = $"UI_CollectItem_{i}";
			_collectItems.Add(go);
		}
	}
}
