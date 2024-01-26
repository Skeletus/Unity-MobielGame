using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string attachedSlotTag;

    public string GetAttachedSlotTag()
    {
        return attachedSlotTag;
    }
    public GameObject owner
    {
        get;
        private set;
    }

    public void Init(GameObject _owner)
    {
        owner = _owner;
        UnEquip();
    }

    public void Equip()
    {
        gameObject.SetActive(true);
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
