using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ManagementItem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _itemNameText;
    [SerializeField] protected TextMeshProUGUI _itemSellPriceText;
    [SerializeField] protected Button _detailsButton;
    [SerializeField] protected Button _sellButton;
    [SerializeField] protected Image _preview;

    protected CrewItem _item;

    public Button DetailsButton => _detailsButton;
    public Button SellButton => _sellButton;

    public CrewItem Item => _item;

    public virtual void InitItem(CrewItem item)
    {
        _item = item;
        _itemNameText.text = item.Descriptor.ItemName;
        _itemSellPriceText.text = item.Descriptor.ResalePrice.ToString("0");
        _preview.sprite = item.Descriptor.ItemSprite;
    }

    public virtual void UpdateItem() { }
}
