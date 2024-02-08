using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopSystem shopSystem;
    [SerializeField] ShopItemUI shopItemUIPrefab;
    [SerializeField] RectTransform shopList;
    [SerializeField] CreditComponent creditComp;

    List<ShopItemUI> shopItems = new List<ShopItemUI>();

    private void Start()
    {
        InitShopItems();
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
