using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CrewItem : IMaintainable
{
	public IncomeGenerator Descriptor { get; set; }

	public virtual float Maintain()
	{
		return 0;
	}

	public virtual float Sell()
	{
		return 0;
	}
}
