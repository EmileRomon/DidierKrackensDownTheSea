using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableBoat : BoatListItem, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TextMeshProUGUI _itemNameText;
	[SerializeField] private TextMeshProUGUI _crewCountText;
    [SerializeField] private UnityEngine.UI.Image _image; 
   
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private DraggableBoatList _boatList;

    private Transform _saveParent;

    public Boat Boat => _boat;

	[SerializeField] private PlayerHandler _playerHandler;

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
		_crewCountText.text = _boat.Crew.Count.ToString();
		_image.sprite = boat.Descriptor.ItemSprite;
    }

    public void RemoveFromList()
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

	public void AssignMember()
	{
		BoatDescriptor bd = (BoatDescriptor)_boat.Descriptor;
		if (_boat.CurrentZone == null || _boat.Crew.Count < bd.MaxCrew)
		{
			_playerHandler.Player.AssignMemberToBoat(_boat);
			_crewCountText.text = _boat.Crew.Count.ToString();
		}
	}

	public void RemoveMember()
	{
		BoatDescriptor bd = (BoatDescriptor)_boat.Descriptor;
		if (_boat.CurrentZone == null || _boat.Crew.Count > bd.MinCrew)
		{
			_playerHandler.Player.RemoveMemberFromBoat(_boat);
			_crewCountText.text = _boat.Crew.Count.ToString();
		}
	}
}
