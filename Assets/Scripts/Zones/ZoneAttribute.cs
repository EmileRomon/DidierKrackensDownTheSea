using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZoneAttribute", menuName = "Environment/ZoneAttribute")]
public class ZoneAttribute : ScriptableObject
{
	[SerializeField] private string _attributeName;
	public string AttributeName => _attributeName;

	//TO-DO: risk increase, rentability increase, etc.
}
