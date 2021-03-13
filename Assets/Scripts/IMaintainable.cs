using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaintainable
{
	/// <summary>
	/// Calcule le prix � payer pour entretenir l'entit�.
	/// </summary>
	/// <returns>Le prix d'entretien de l'entit�</returns>
	public float Maintain();
	public float Sell();
}
