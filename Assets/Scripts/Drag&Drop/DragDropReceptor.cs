using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropReceptor: MonoBehaviour, IDropHandler
{
    [SerializeField] DraggableBoatList _boatList;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableBoat db = eventData.pointerDrag.GetComponent<DraggableBoat>();

        if (db != null)
        {
            Destroy(eventData.pointerDrag);

            _boatList.AddBoat(db.Boat);
        }
    }
}
