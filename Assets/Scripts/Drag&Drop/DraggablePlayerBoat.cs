using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePlayerBoat : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private GameInfo _gameInfo;
	public PlayerController Player => _gameInfo.Player;


	private RectTransform _rectTransform;
	private Transform _saveParent;
	private CanvasGroup _canvasGroup;
	private Canvas _canvas;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
		_canvasGroup = GetComponentInParent<CanvasGroup>();
		_canvas = FindObjectOfType<Canvas>(); //TODO: beurk
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
