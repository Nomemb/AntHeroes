using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
		EnemyProjectile,
		ProgressBarSlider,
	}

	enum Images
	{
		PlayerHPBarFill,
		EnemyHPBarFill,
		EnemyCoin,
		
	}
	
	Coroutine _waitCoroutine;
	[SerializeField] float _blockSpeed = 700.0f;

	GameObject _block;
	GameObject _player;
	GameObject _enemy;
	GameObject _enemyProjectile;
	
	PlayerController _playerController;

	
	int _waveCount = 0;
	int _enemyHP = 0;

	int _stageNum = 0;
	
	float _playerAttack;
	
	PlayerData _playerData;
	EnemyData _enemyData;
	StageData _stageData;
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
		_enemy = GetObject((int)GameObjects.Enemy);
		
		_playerController = _player.GetOrAddComponent<PlayerController>();
		_block = GetObject((int)GameObjects.Block);
		_enemyProjectile = GetObject((int)GameObjects.EnemyProjectile);
		
		_block.SetActive(false);
		_enemyProjectile.SetActive(false);
		
		// 전투 관련 오브젝트들 비활성화
		GetImage((int)Images.EnemyCoin).gameObject.SetActive(false);
		GetText((int)Texts.PlayerHPText).gameObject.SetActive(false);
		GetText((int)Texts.EnemyHPText).gameObject.SetActive(false);
		GetObject((int)GameObjects.BlockEffect).SetActive(false);
		
		
		if(Managers.Data.Players.TryGetValue(1, out _playerData) == false)
			Debug.Log("PlayerData not Found");

		_playerController.SetSkeletonAsset(_playerData.spine);
		_playerController.State = AnimState.Idle;
		
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
			
			case BattleStatus.BattleFinish:
				UpdateBattleFinish();
				break;
			
			case BattleStatus.Victory:
				UpdateVictory();
				break;
			
			case BattleStatus.Defeat:
				UpdateDefeat();
				break;
		}
	}

	private void BattleInit()
	{
		_stageNum = Managers.Game.Stage;
		if(Managers.Data.Stages.TryGetValue(_stageNum / 10, out _stageData) == false)
			Debug.Log("StageData not Found");
		
		// 적군 데이터를 스테이지 데이터에서 불러오고 목적지로 이동시킴, 공격모드로 전환
		if (_stageData != null)
		{
			if(Managers.Data.Enemies.TryGetValue(_stageData.enemyData[_waveCount].enemyID, out _enemyData) == false)
				Debug.Log("EnemyData not Found");
		}
		Debug.Log($"{_enemyData.maxhp} , {_enemyData.atk}");
		_enemyData = EnhanceEnemy(_enemyData);
		Debug.Log($"->{_enemyData.maxhp} , {_enemyData.atk}");
		RefreshUI();
		
		_enemy.GetOrAddComponent<PlayerController>().SetSkeletonAsset(_enemyData.spine);
		_enemy.GetOrAddComponent<PlayerController>().State = AnimState.Attack;
		_enemyHP = _enemyData.maxhp;
		GetImage((int)Images.EnemyHPBarFill).fillAmount = (float)_enemyHP / _enemyData.maxhp;
		_enemy.gameObject.SetActive(true);

		//_playerController.State = AnimState.Attack;
		_waitCoroutine = StartCoroutine(CoWait(1.0f));
		
		_block.SetActive(true);
		_block.transform.position = GetObject((int)GameObjects.BlockStart).transform.position;
		_enemyProjectile.SetActive(true);
		_enemyProjectile.transform.position = GetObject((int)GameObjects.BlockDest).transform.position;
		_status = BattleStatus.BattleProceed;
	}

	private void RefreshProjectile(GameObject go, Vector3 origin)
	{
		go.transform.position = origin;
		go.SetActive(true);
	}
	
	const float EPSILON = 30.0f;
	private void UpdateBattleProceed()
	{
		PlayerAttack();
		EnemyAttack();
	}

	private void PlayerAttack()
	{
		Vector3 dir = (GetObject((int)GameObjects.BlockDest).transform.position - _block.transform.position);

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
				GetImage((int)Images.EnemyCoin).gameObject.SetActive(true);
				Managers.Game.Money += _enemyData.goldReward;

				// 아직 스테이지에 몬스터 남아있으면
				if (_waveCount < _stageData.enemyData.Count - 1)
				{
					_status = BattleStatus.BattleStart;
					_waveCount++;
				}
				else
				{
					_status = BattleStatus.Victory;
					_waveCount = 0;
					return;
				}
				
				Managers.UI.FindPopup<UI_PlayPopup>()?.RefreshMoney();
				Managers.UI.FindPopup<UI_PlayPopup>()?.RefreshStat();

				_waitCoroutine = StartCoroutine(CoWait(2.0f));
				GetImage((int)Images.EnemyCoin).gameObject.SetActive(false);
				GetObject((int)GameObjects.Enemy).SetActive(false);
				return;
			}
			_waitCoroutine = StartCoroutine(CoWait(1f));
			//GetText((int)Texts.EnemyHPText).gameObject.SetActive(false);
			RefreshProjectile(_block, GetObject((int)GameObjects.BlockStart).transform.position);
			return;
		}

		_block.transform.position += dir.normalized * Math.Min(dir.magnitude, _blockSpeed * Time.deltaTime);
	}

	private void EnemyAttack()
	{
		if (_status != BattleStatus.BattleProceed)
		{
			_enemyProjectile.SetActive(false);
			return;
		}
		
		Vector3 dir = (GetObject((int)GameObjects.BlockStart).transform.position - _enemyProjectile.transform.position);
		
		
		// 피격 시
		if (dir.magnitude < EPSILON)
		{
			_enemyProjectile.SetActive(false);
			Managers.Sound.Play(Sound.Effect, ("Sound_PlayerAttacked"));

			int damage = _enemyData.atk;
			Managers.Game.HP -= damage;
			
			// 데미지 표시
			GetText((int)Texts.PlayerHPText).text = $"-{damage}";
			GetText((int)Texts.PlayerHPText).gameObject.SetActive(true);
			GetImage((int)Images.PlayerHPBarFill).fillAmount = (float)Managers.Game.HP / Managers.Game.MaxHP;

			if (Managers.Game.HP <= 0)
			{
				_status = BattleStatus.Defeat;

				return;
			}
			_waitCoroutine = StartCoroutine(CoWait(1f));
			//GetText((int)Texts.PlayerHPText).gameObject.SetActive(false);
			RefreshProjectile(_enemyProjectile, GetObject((int)GameObjects.BlockDest).transform.position);
			return;
		}
		
		_enemyProjectile.transform.position += dir.normalized * Math.Min(dir.magnitude, _blockSpeed * Time.deltaTime);
	}
	private void UpdateBattleFinish()
	{
		_block.SetActive(false);
		_enemyProjectile.SetActive(false);
	}

	private int GetBaseAttackDamage()
	{
		return (int)(Managers.Game.BaseAttackPower * (1 + Managers.Game.AttackAmplification) + Managers.Game.CollectAttackPower * (1 + Managers.Game.FishDamageAmplification));
	}

	private void UpdateVictory()
	{
		_enemy.gameObject.SetActive(false);
		Managers.Game.Stage += 1;
		Managers.Game.HP = Managers.Game.MaxHP;
		
		BattleInit();
	}

	private void UpdateDefeat()
	{
		_enemy.gameObject.SetActive(false);
		Managers.Game.Stage -= 1;
		Managers.Game.HP = Managers.Game.MaxHP;
		_waveCount = 0;
		
		BattleInit();
	}


	IEnumerator CoWait(float seconds)
	{
		// 코루틴 실행중이면 중지
		if(_waitCoroutine != null)
			StopCoroutine(_waitCoroutine);

		yield return new WaitForSeconds(seconds);
		_waitCoroutine = null;
	}

	private void RefreshUI()
	{
		RefreshStage();
	}

	private void RefreshStage()
	{
		GetText((int)Texts.StageNumText).text = $"{_stageNum / 10 + 1} - {_stageNum % 10 + 1} {Managers.GetText(_stageData.nameID)}";
		GetObject((int)GameObjects.ProgressBarSlider).GetComponent<Slider>().value = _waveCount / (float)_stageData.enemyData.Count;
		GetImage((int)Images.PlayerHPBarFill).fillAmount = (float)Managers.Game.HP / Managers.Game.MaxHP;
	}

	private EnemyData EnhanceEnemy(EnemyData enemyData)
	{
		EnemyData newData = new EnemyData();
		double magnificationStatus = 1 + _stageData.enhancementStatus * (_stageNum * 0.1 + 1);
		double magnificationReward = 1 + _stageData.enhancementReward * (_stageNum * 0.1 + 1);
		
		newData.maxhp = (int)(enemyData.maxhp * Math.Pow(magnificationStatus, _stageNum%10 + 1));
		newData.atk = (int)(enemyData.atk * Math.Pow(magnificationStatus, _stageNum%10 + 1));
		
		newData.goldReward = (int)(enemyData.goldReward * Math.Pow(magnificationReward, _stageNum%10));
		return newData;
	}
}
