using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class UI_BattlePopup : UI_Popup
{
	enum Texts
	{
		StageDifficultyText,
		StageNumText,
		PlayerHPText,
		EnemyHPText,
	}

	enum GameObjects
	{
		Player,
		Enemy,
		// Enemies,
		Block,
		BlockStart,
		BlockDest,
		BlockEffect,
		ProgressBar,
	}

	enum Images
	{
		PlayerHPBarFill,
		EnemyHPBarFill,
		
	}
	
	Coroutine _waitCoroutine;
	[SerializeField] float _blockSpeed = 700.0f;

	GameObject _block;
	GameObject _player;
	GameObject _enemy;
	// GameObject _enemies;
	
	int _waveCount = 0;
	int _enemyHP = 0;

	PlayerData _playerData;
	PlayerData _enemyData;
	List<GameObject> _enemyList;

	enum BattleStatus
	{
		BattleStart,
		BattleProceed,
		BattleFinish,
		Boss,
		Victory,
		Defeat
	}

	BattleStatus _status = BattleStatus.BattleStart;

	public override bool Init()
	{
		Managers.Sound.Clear();
		Managers.Sound.Play(Sound.Bgm, "Sound_Battle", volume: 0.2f);

		if (base.Init() == false)
			return false;
		
		BindText(typeof(Texts));
		BindObject(typeof(GameObjects));
		BindImage(typeof(Images));

		_player = GetObject((int)GameObjects.Player);
		// _enemies = GetObject((int)GameObjects.Enemies);
		_enemy = GetObject((int)GameObjects.Enemy);
		

		_block = GetObject((int)GameObjects.Block);
		_block.SetActive(false);
		
		if(Managers.Data.Players.TryGetValue(1, out _playerData) == false)
			Debug.Log("PlayerData not Found");

		if(Managers.Data.Players.TryGetValue(2, out _enemyData) == false)
			Debug.Log("EnemyData not Found");
		
		_player.GetOrAddComponent<PlayerController>().SetSkeletonAsset(_playerData.spine);
		_player.GetOrAddComponent<PlayerController>().State = AnimState.Idle;
		
		// _enemy.GetOrAddComponent<EnemyController>()
		
		_enemyHP = _enemyData.maxhp;
		
		_block.SetActive(false);
		GetObject((int)GameObjects.BlockEffect).SetActive(false);
		

		Managers.Game.HP = Managers.Game.MaxHP;

		return true;
	}

	private void Update()
	{
		if (_waitCoroutine != null)
			return;

		switch (_status)
		{
			case BattleStatus.BattleStart:
				BattleInit();
				break;
				
			case BattleStatus.BattleProceed:
				UpdateBattleProceed();
				break;
				
		}
	}

	private void BattleInit()
	{
		// 적군 데이터를 스테이지 데이터에서 불러오고 목적지로 이동시킴, 공격모드로 전환
		
		_block.SetActive(true);
		_block.transform.position = GetObject((int)GameObjects.BlockStart).transform.position;

		

		_status = BattleStatus.BattleProceed;
	}

	const float EPSILON = 30.0f;
	private void UpdateBattleProceed()
	{
		Vector3 dir = (GetObject((int)GameObjects.BlockDest).transform.position - _block.transform.position);
		
		GetText((int)Texts.PlayerHPText).gameObject.SetActive(false);
		GetText((int)Texts.EnemyHPText).gameObject.SetActive(false);

		// 피격 시
		if (dir.magnitude < EPSILON)
		{
			_block.SetActive(false);

			Managers.Sound.Play(Sound.Effect, ("Sound_EnemyAttacked"));
			
			GetObject((int)GameObjects.BlockEffect).SetActive(true);
			GetObject((int)GameObjects.BlockEffect).GetOrAddComponent<BaseController>().Refresh();
			

			int damage = GetBaseAttackDamage();
			_enemyHP -= damage;
			GetText((int)Texts.EnemyHPText).text = $"-{damage}";
			GetText((int)Texts.EnemyHPText).gameObject.SetActive(true);
			GetImage((int)Images.EnemyHPBarFill).fillAmount = (float)_enemyHP / _enemyData.maxhp;

			if (_enemyHP <= 0)
			{
				_status = BattleStatus.Victory;
				_waitCoroutine = StartCoroutine(CoWait(2.0f));
				GetObject((int)GameObjects.Enemy).SetActive(false);
				return;
			}
			
			
			_waitCoroutine = StartCoroutine(CoWait(2.0f));
			_status = BattleStatus.BattleStart;
			return;
		}
		

		_block.transform.position += dir.normalized * Math.Min(dir.magnitude, _blockSpeed * Time.deltaTime);
	}

	private int GetBaseAttackDamage()
	{
		return (int)(Managers.Game.BaseAttackPower * (1 + Managers.Game.AttackAmplification) + Managers.Game.FishAttackPower * (1 + Managers.Game.FishDamageAmplification));
	}


	IEnumerator CoWait(float seconds)
	{
		// 코루틴 실행중이면 중지
		if(_waitCoroutine != null)
			StopCoroutine(_waitCoroutine);

		yield return new WaitForSeconds(seconds);
		_waitCoroutine = null;
	}
}
