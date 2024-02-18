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
		
	}

	enum GameObjects
	{
		Player,
		Enemy,
		Block,
		BlockStart,
		BlockDest,
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

	int _enemyHP = 0;

	PlayerData _playerData;
	PlayerData _enemyData;

	enum BattleStatus
	{
		PlayerAttack,
		Boss,
		Victory,
		Defeat
	}

	BattleStatus _status = BattleStatus.PlayerAttack;

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

		_block = GetObject((int)GameObjects.Block);
		_block.SetActive(false);
		
		if(Managers.Data.Players.TryGetValue(0, out _playerData) == false)
			Debug.Log("PlayerData not Found");

		_player.GetOrAddComponent<PlayerController>();
		
		return true;
	}
}
