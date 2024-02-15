using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
	protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

	protected bool _init = false;

	public virtual bool Init()
	{
		if (_init)
			return false;

		return _init = true;
	}

	private void Start()
	{
		Init();
	}

	protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
		// Enum에 선언해놓은 목록들을 가져옴
		string[] names = Enum.GetNames(type);
		// 해당 길이만큼 오브젝트 배열 생성
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		// 오브젝트 딕셔너리에 해당 배열을 타입, 밸류값으로 저장
		_objects.Add(typeof(T), objects);
		
		for (int i = 0; i < names.Length; i++)
		{
			// General 사용해서 해당하는 타입의 자식을 가져옴
			if(typeof(T) == typeof(GameObject))
				objects[i] = Utils.FindChild(gameObject, names[i], true);
			else
				objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
			
			
			if(objects[i] == null)
				Debug.Log($"Failed to bind({names[i]})");
		}
	}
	
	protected void BindObject(Type type) { Bind<GameObject>(type); }
	protected void BindImage(Type type) { Bind<Image>(type); }
	protected void BindText(Type type) { Bind<TextMeshProUGUI>(type); }
	protected void BindButton(Type type) { Bind<Button>(type); }

	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (_objects.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}
	
	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
	protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
	protected Button GetButton(int idx) { return Get<Button>(idx); }
	protected Image GetImage(int idx) { return Get<Image>(idx); }
}
