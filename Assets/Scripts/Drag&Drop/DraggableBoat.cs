using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableBoat : BoatListItem, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private UnityEngine.UI.Image _image; 
   
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private DraggableBoatList _boatList;

    private Transform _saveParent;

    public Boat Boat => _boat;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = FindObjectOfType<Canvas>(); //TODO: beurk
        _boatList = GetComponentInParent<DraggableBoatList>();
    }

    override public void SetBoat(Boat boat)
    {
        base.SetBoat(boat);
        BoatDescriptor bd = (BoatDescriptor)boat.Descriptor;

        _itemNameText.text = bd.ItemName;
    }

    private void OnDestroy()
    {
        _boatList.RemoveBoat(_boat);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _saveParent = transform.parent;
        _canvasGroup.blocksRaycasts = false;

        transform.SetParent(_canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_saveParent);
        _canvasGroup.blocksRaycasts = true;
    }
}
