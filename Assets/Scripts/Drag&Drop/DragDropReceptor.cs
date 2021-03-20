using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropReceptor: MonoBehaviour, IDropHandler
{
    protected Zone _zone;
    protected DraggableBoatList _list;

	protected static Zone OpenZone;

	protected void Start()
    {
        _zone = GetComponent<Zone>();
        _list = GetComponentInChildren<DraggableBoatList>();
		OpenZone = null;
    }

	public Zone DragDropZone
	{
		get => _zone;
		set
		{
			_zone = value;
			Debug.Log("new value is " + value, this);
		}
	}

    public virtual void OnDrop(PointerEventData eventData)
    {
        DraggableBoat db = eventData.pointerDrag.GetComponent<DraggableBoat>();

        if (((OpenZone == null && db.Boat.CheckAvailable()) || _zone == null) && db != null)
        {
            Destroy(eventData.pointerDrag);

            if (db.Boat.CurrentZone == null)
                db.RemoveFromList();

            db.Boat.AffectNewZone(_zone);

			if (_list != null)
			{
				_list.AddBoat(db.Boat);
			}
		}

    }

	public static void SetOpenZone(Zone zone)
	{
		OpenZone = zone;
	}
}
