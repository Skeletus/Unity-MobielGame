using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopSystem shopSystem;
    [SerializeField] ShopItemUI shopItemUIPrefab;
    [SerializeField] RectTransform shopList;
    [SerializeField] CreditComponent creditComp;
    [SerializeField] UIManager uiManager;

    [SerializeField] TextMeshProUGUI creditText;

    [SerializeField] Button BackBtn;
    [SerializeField] Button BuyBtn;

    List<ShopItemUI> shopItems = new List<ShopItemUI>();

    private void Start()
    {
        InitShopItems();
        BackBtn.onClick.AddListener(uiManager.SwithToGameplayUI);
        creditComp.onCreditChanged += UpdateCredit;
        UpdateCredit(creditComp.Credit);
    }

    private void UpdateCredit(int newCredit)
    {
        creditText.SetText(newCredit.ToString());
    }

    private void InitShopItems()
    {
        ShopItem[] shopItems = shopSystem.GetShopItems();
        foreach (ShopItem item in shopItems)
        {
            AddShopItem(item);
        }
    }

    private void AddShopItem(ShopItem item)
    {
        ShopItemUI newItemUI = Instantiate(shopItemUIPrefab, shopList);
        newItemUI.Init(item, creditComp.Credit);
        shopItems.Add(newItemUI);
    }
}
