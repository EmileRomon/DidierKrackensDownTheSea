using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "GameInfo")]
public class GameInfo : ScriptableObject
{
	private string _companyName = "";

	public string CompanyName
	{
		get => _companyName;
		set => _companyName = value;
	}

	[ContextMenu("Test")]
	public void LogCompanyName()
	{
		Debug.Log(_companyName);
	}
}
