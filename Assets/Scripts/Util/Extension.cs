using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extension
{
	public static T GetOrAddComponent<T>(GameObject go) where T : Component
	{
		return Utils.GetOrAddComponent<T>(go);
	}

	public static void BindEvent(this GameObject go, Action action, Define.UIEvent actionType = Define.UIEvent.Click)
	{
		UI_Base.BindEvent(go, action, actionType);
	}

	/// <summary>
	/// 수직 스크롤 위치 초기화
	/// </summary>
	/// <param name="scrollRect"></param>
	public static void ResetVertical(this ScrollRect scrollRect)
	{
		scrollRect.verticalNormalizedPosition = 1;
	}
}
