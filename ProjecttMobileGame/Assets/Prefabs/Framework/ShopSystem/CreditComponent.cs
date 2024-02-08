using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPurchaseListener
{
    public void HandlePurchase(Object newPurchase);
}

public class CreditComponent : MonoBehaviour
{
    [SerializeField] int credits;
    [SerializeField] Component[] PurchaseListeners;

    List<IPurchaseListener> purchaseListenerInterfaces = new List<IPurchaseListener>();

    private void Start()
    {
        CollectPurchaseListeners();
    }

    private void CollectPurchaseListeners()
    {
        foreach (Component listener in PurchaseListeners)
        {
            IPurchaseListener listenerInterface = listener as IPurchaseListener;
            if (listenerInterface != null)
            {
                purchaseListenerInterfaces.Add(listenerInterface);
            }
        }
    }

    private void BroadcastPurchase(Object item)
    {
        foreach (IPurchaseListener purchaseListener in purchaseListenerInterfaces)
        {
            purchaseListener.HandlePurchase(item);
        }
    }

    public int Credit
    {
        get { return credits; }
    }

    public delegate void OnCreditChanged(int newCredit);
    public event OnCreditChanged onCreditChanged;

    public bool Purchase(int price, Object item)
    {
        if (credits < price) return false;

        credits -= price;
        onCreditChanged?.Invoke(credits);
        BroadcastPurchase(item);

        return true;
    }
}
