using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropReceptorPlayerBoat : DragDropReceptor
{
	private DraggablePlayerBoatContainer _dpbc;

	private void Awake()
	{
		_dpbc = GetComponent<DraggablePlayerBoatContainer>();
	}

	public override void OnDrop(PointerEventData eventData)
	{
		//base.OnDrop(eventData);
		DraggablePlayerBoat playerBoat = eventData.pointerDrag.GetComponent<DraggablePlayerBoat>();
		if (playerBoat != null)
		{
			playerBoat.Player.CurrentZone = null;
			_dpbc.UpdateView();
			Destroy(eventData.pointerDrag);
		}
	}
}
