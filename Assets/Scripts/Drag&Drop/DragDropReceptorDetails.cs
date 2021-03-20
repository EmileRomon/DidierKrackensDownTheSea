using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropReceptorDetails : DragDropReceptor
{
	[SerializeField] private ZoneDetails _zoneDetails;
	public override void OnDrop(PointerEventData eventData)
	{
		base.OnDrop(eventData);
		_zoneDetails.UpdateDetails(_zone);
	}
}
