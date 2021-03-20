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

		Debug.Log(_zone == null);

		if (db != null && db.Boat.CheckAvailable())
		{
			Destroy(eventData.pointerDrag);

			db.Boat.AffectNewZone(_zone);

			if (_list != null)
			{
				_list.AddBoat(db.Boat);
			}
		}
	}
}
