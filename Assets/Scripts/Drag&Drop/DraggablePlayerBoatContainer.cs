using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggablePlayerBoatContainer : MonoBehaviour
{
	[SerializeField] private DraggablePlayerBoat _playerBoatPrefab;
	[SerializeField] private PlayerController _playerController;
	public bool _assigned = false;

	private void Awake()
	{
		UpdateView();
	}

	[ContextMenu("Update")]
	public virtual void UpdateView()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		DraggablePlayerBoat go = Instantiate(_playerBoatPrefab, transform);
	}
}
