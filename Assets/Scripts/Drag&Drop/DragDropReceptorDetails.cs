using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropReceptorDetails : DragDropReceptor
{
	[SerializeField] private ZoneDetails _zoneDetails;
	public override void OnDrop(PointerEventData eventData)
	{
		//base.OnDrop(eventData);
		DraggableBoat db = eventData.pointerDrag.GetComponent<DraggableBoat>();
		db = eventData.pointerDrag.GetComponent<DraggableBoat>();
		if (_zone != null) _zoneDetails.UpdateDetails(_zone);

		if (db != null && db.Boat.CheckAvailable())
		{
			db.Boat.AffectNewZone(_zone);

			if (_list != null)
			{
				_list.AddBoat(db.Boat);
			}
			Destroy(eventData.pointerDrag);
		}
		else
		{
			DraggablePlayerBoat dpb = eventData.pointerDrag.GetComponent<DraggablePlayerBoat>();
			if (dpb != null)
			{
				dpb.Player.CurrentZone = _zone;
			}
			Destroy(eventData.pointerDrag);
		}
	}
}
