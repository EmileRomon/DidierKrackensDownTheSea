using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IncomeGenerator : ScriptableObject
{
	[SerializeField] protected float _incomeFactor;
	[SerializeField] protected float _maintenancePrice;
	[SerializeField] protected float _purchasePrice;
	[SerializeField] protected float _sellPrice;

	public float IncomeFactor => _incomeFactor;
	public float MaintenancePrice => _maintenancePrice;
	public float PurchasePrice => _purchasePrice;
	public float SellPrice => _sellPrice;
}
