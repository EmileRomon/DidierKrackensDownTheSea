using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropReceptor: MonoBehaviour, IDropHandler
{
    [SerializeField] DraggableBoatList _boatList;

    private Zone _zone;

    private void Start()
    {
        _zone = GetComponent<Zone>();
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
            _boatList.AddBoat(db.Boat);
        }
    }
}
