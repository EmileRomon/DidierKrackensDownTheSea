using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHandler", menuName = "Crew/PlayerHandler")]
public class PlayerHandler : ScriptableObject
{
	private PlayerController _playerController = null;
	public PlayerController Player { get; set; }
}
