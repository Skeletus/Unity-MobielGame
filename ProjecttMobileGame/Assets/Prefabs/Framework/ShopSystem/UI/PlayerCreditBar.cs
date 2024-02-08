using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreditBar : MonoBehaviour
{
    [SerializeField] Button ShopBtn;
    [SerializeField] UIManager uiManager;
    [SerializeField] CreditComponent creditComp;
    [SerializeField] TextMeshProUGUI creditText;
    private void Start()
    {
        ShopBtn.onClick.AddListener(PullOutShop);
        creditComp.onCreditChanged += UpdateCredit;
        UpdateCredit(creditComp.Credit);
    }

    private void UpdateCredit(int newCredit)
    {
        creditText.SetText(newCredit.ToString());
    }

    private void PullOutShop()
    {
        uiManager.SwithToShop();
    }
}
