using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaintainable
{
	/// <summary>
	/// Calcule le prix à payer pour entretenir l'entité.
	/// </summary>
	/// <returns>Le prix d'entretien de l'entité</returns>
	public float Maintain();
	public float Sell();
}
