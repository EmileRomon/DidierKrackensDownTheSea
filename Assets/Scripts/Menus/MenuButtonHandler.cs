using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{
	[SerializeField] private List<Button> _buttonsToDisable;

    public void OnDisplay()
	{
		foreach(Button btn in _buttonsToDisable)
		{
			btn.interactable = false;
		}
		gameObject.SetActive(true);
	}

	public void OnHide()
	{
		foreach (Button btn in _buttonsToDisable)
		{
			btn.interactable = true;
		}
		gameObject.SetActive(false);
	}
}
