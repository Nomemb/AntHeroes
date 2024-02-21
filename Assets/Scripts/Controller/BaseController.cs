using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class BaseController : UI_Base
{
	protected SkeletonGraphic _anim = null;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		_anim = GetComponent<SkeletonGraphic>();
		return true;
	}

	protected virtual void UpdateAnimation() { }

	/// <summary>
	/// flag에 따라 좌우 회전시킴
	/// </summary>
	/// <param name="flag"></param>
	public virtual void LookLeft(bool flag)
	{
		Vector3 scale = transform.localScale;

		if (flag)
			transform.localScale = new Vector3(Math.Abs(scale.x), scale.y, scale.z);
		else
			transform.localScale = new Vector3(-Math.Abs(scale.x), scale.y, scale.z);
	}

	public void SetSkeletonAsset(string path)
	{
		Init();
		_anim.skeletonDataAsset = Managers.Resource.Load<SkeletonDataAsset>(path);
		_anim.Initialize(true);
	}

	public void PlayAnimation(string name, bool loop = true)
	{
		Init();
		_anim.startingAnimation = name;
		_anim.startingLoop = loop;
	}

	public void ChangeSkin(string name)
	{
		Init();
		_anim.initialSkinName = name;
		_anim.Initialize(true);
	}

	public void Refresh()
	{
		Init();
		_anim.Initialize(true);
	}
}
