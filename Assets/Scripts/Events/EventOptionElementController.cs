using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventOptionElementController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _optionDescriptionText;
    [SerializeField] private Button _optionButton;

    public string OptionDescription
    {
        get { return _optionDescriptionText.text; }
        set { _optionDescriptionText.text = value; }
    }
    public Button OptionButton => _optionButton;
}
