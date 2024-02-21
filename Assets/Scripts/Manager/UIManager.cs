using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager
{
	int _order = -20;
	
	Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
	
	public UI_Scene SceneUI { get; private set; }

	public GameObject Root 
	{
		get 
		{
			GameObject root = GameObject.Find("@UI_Root");
			if (root == null)
				root = new GameObject { name = "@UI_Root" };

			return root;
		}	
	}

	public void SetCanvas(GameObject go, bool sort = true)
	{
		Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.overrideSorting = true;

		if (sort)
		{
			canvas.sortingOrder = _order;
			_order++;
		} 
		else
		{
			canvas.sortingOrder = 0;
		}
	}

	public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UI_Popup
	{
		if (string.IsNullOrEmpty(name))
			name = typeof(T).Name;
		
		
		// 미리 만들어놓은 프리팹을 가져와서 달아줌
		GameObject prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/Popup/{name}");

		GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
		T popup = Utils.GetOrAddComponent<T>(go);
		_popupStack.Push(popup);
		
		if (parent != null)
			go.transform.SetParent(parent);
		else if (SceneUI != null)
			go.transform.SetParent(SceneUI.transform);
		else
			go.transform.SetParent(Root.transform);

		go.transform.localScale = Vector3.one;
		go.transform.localPosition = prefab.transform.position;
		
		return popup;
	}
	
	public T FindPopup<T>() where T : UI_Popup
	{
		return _popupStack.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
		// return _popupStack.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
	}

	public void ClosePopupUI(UI_Popup popup)
	{
		if (_popupStack.Count == 0)
			return;

		if (_popupStack.Peek() != popup)
		{
			Debug.Log("Close Popup Failed!");
			return;
		}
		
		ClosePopupUI();
	}

	/// <summary>
	/// UI 스택에서 UI를 닫고, 레퍼런스 null 처리를 함
	/// </summary>
	public void ClosePopupUI()
	{
		if (_popupStack.Count == 0)
			return;

		UI_Popup popup = _popupStack.Pop();
		Managers.Resource.Destroy(popup.gameObject);
		popup = null;
		_order--;
	}
}