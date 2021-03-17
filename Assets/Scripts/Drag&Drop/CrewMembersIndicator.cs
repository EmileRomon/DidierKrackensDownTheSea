using UnityEngine;
using TMPro;

public class CrewMembersIndicator : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _countText;

    public void UpdateNumber(int count)
	{
		_countText.text = count.ToString();
	}
}
