using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropReceptor: MonoBehaviour, IDropHandler
{
    private Zone _zone;
    private DraggableBoatList _list;

    private void Start()
    {
        _zone = GetComponent<Zone>();
        _list = GetComponentInChildren<DraggableBoatList>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableBoat db = eventData.pointerDrag.GetComponent<DraggableBoat>();

        if (db != null)
        {
            Destroy(eventData.pointerDrag);

            if (db.Boat.CurrentZone == null)
                db.RemoveFromList();

            db.Boat.AffectNewZone(_zone);
            
            if(_list!=null)
            {
                _list.AddBoat(db.Boat);
            }
        }

    }
}
