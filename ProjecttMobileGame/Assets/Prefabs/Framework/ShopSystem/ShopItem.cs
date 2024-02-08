using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string Title;
    public int Price;
    public Object Item;
    public Sprite ItemIcon;
    [TextArea(10, 10)]
    public string Description;
}
