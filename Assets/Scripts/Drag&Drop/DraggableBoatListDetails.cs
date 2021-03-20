using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableBoatListDetails : DraggableBoatList
{
	public override void UpdateView()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}
	protected new void Start() { }

}
