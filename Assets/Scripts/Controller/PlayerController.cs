using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using static Define;

public class PlayerController : BaseController
{
	PlayerData _data;
	PlayerState _playerState;

	public AnimState State
	{
		get { return _playerState.state; }
		set
		{
			_playerState.state = value;
			UpdateAnimation();
		}
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		_playerState = new PlayerState();
		// 일단 플레이어만, 차후에 EnemyController로 나눌듯
		Managers.Data.Players.TryGetValue(1, out _data);
		_anim.skeletonDataAsset = Managers.Resource.Load<SkeletonDataAsset>(_data.spine);
		State = AnimState.Attack;
		_anim.timeScale = 0.7f;

		return true;
	}

	protected override void UpdateAnimation()
	{
		Init();

		switch (State)
		{
			case AnimState.Idle:
				PlayAnimation(_data.aniIdle);
				ChangeSkin(_data.aniIdleSkin);
				break;
			
			case AnimState.Walking:
				PlayAnimation(_data.aniWorking);
				ChangeSkin(_data.aniWorkingSkin);
				break;
			
			case AnimState.Attack:
				PlayAnimation(_data.aniAttack);
				ChangeSkin(_data.aniAttackSkin);
				break;
			
			
				
		}
	}
}
