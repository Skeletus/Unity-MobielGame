using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityDock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] RectTransform Root;
    [SerializeField] VerticalLayoutGroup LayoutGrp;
    [SerializeField] AbilityUI AbilityUIPrefab;

    List<AbilityUI> abilityUIs = new List<AbilityUI>();

    PointerEventData touchData;
    AbilityUI hightlightedAbility;

    private void Awake()
    {
        abilityComponent.onNewAbilityAdded += AddAbility;
    }

    private void AddAbility(Ability newAbility)
    {
        AbilityUI newAbilityUI = Instantiate(AbilityUIPrefab, Root);
        newAbilityUI.Init(newAbility);
        abilityUIs.Add(newAbilityUI);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (touchData != null)
        {
            GetUIUnderPointer(touchData, out hightlightedAbility);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (hightlightedAbility)
        {
            hightlightedAbility.ActivateAbility();
        }
        touchData = null;
    }

    bool GetUIUnderPointer(PointerEventData eventData, out AbilityUI abilityUI)
    {
        List<RaycastResult> findAbility = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, findAbility);

        abilityUI = null;
        foreach (RaycastResult result in findAbility)
        {
            abilityUI = result.gameObject.GetComponentInParent<AbilityUI>();
            if (abilityUI != null)
                return true;
        }

        return false;
    }
}
