using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] TextMeshProUGUI DescriptionText;

    [SerializeField] Button button;
    [SerializeField] Image GrayOutCover;

    ShopItem item;

    [SerializeField] Color InEfficientCreditColor;
    [SerializeField] Color SurffiicentCreditColor;
    // Start is called before the first frame update

    public delegate void OnItemSelected(ShopItemUI selectedItem);

    public event OnItemSelected onItemSelected;

    private void Start()
    {
        button.onClick.AddListener(ItemSelected);
    }

    private void ItemSelected()
    {
        onItemSelected?.Invoke(this);
    }

    public void Init(ShopItem item, int AvaliableCredits)
    {
        this.item = item;

        Icon.sprite = item.ItemIcon;
        TitleText.text = item.Title;
        PriceText.text = "$" + item.Price.ToString();
        DescriptionText.text = item.Description;

        Refresh(AvaliableCredits);
    }

    internal ShopItem GetItem()
    {
        return item;
    }

    public void Refresh(int avaliableCredits)
    {
        if (avaliableCredits < item.Price)
        {
            GrayOutCover.enabled = true;
            PriceText.color = InEfficientCreditColor;
        }
        else
        {
            GrayOutCover.enabled = false;
            PriceText.color = SurffiicentCreditColor;
        }
    }
}
