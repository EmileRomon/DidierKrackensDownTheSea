using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IncomeGenerator : ScriptableObject
{
	[SerializeField] protected string _itemName;
	[SerializeField] protected float _incomeFactor;
	[SerializeField] protected float _maintenancePrice;
	[SerializeField] protected float _purchasePrice;
	[SerializeField] protected float _resalePrice;
	[SerializeField] protected Sprite _sprite;


	public string ItemName => _itemName;
	public float IncomeFactor => _incomeFactor;
	public float MaintenancePrice => _maintenancePrice;
	public float PurchasePrice => _purchasePrice;
	public float ResalePrice => _resalePrice;
	public Sprite ItemSprite => _sprite;
}
