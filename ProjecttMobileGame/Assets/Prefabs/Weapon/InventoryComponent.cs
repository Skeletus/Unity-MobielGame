using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initialWeaponPrefabs;
    [SerializeField] Transform defaultWeaponSlot;
    [SerializeField] Transform[] weaponSlots;

    List<Weapon> weapons;
    int currentWeaponIndex = -1;

    public void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        weapons = new List<Weapon>();
        foreach (Weapon weapon in initialWeaponPrefabs)
        {
            Transform weaponSlot = defaultWeaponSlot;

            foreach(Transform slot in weaponSlots)
            {
                if (slot.gameObject.tag == weapon.GetAttachedSlotTag())
                {
                    weaponSlot = slot;
                }
            }

            Weapon newWeapon = Instantiate(weapon, weaponSlot);
            newWeapon.Init(gameObject);
            weapons.Add(newWeapon);
        }

        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextWeaponIndex = currentWeaponIndex + 1;
        if(nextWeaponIndex >= weapons.Count )
        {
            nextWeaponIndex = 0;
        }

        EquipWeapon(nextWeaponIndex);
    }

    private void EquipWeapon(int nextWeaponIndex)
    {
        if (nextWeaponIndex < 0 || nextWeaponIndex >= weapons.Count)
        {
            return;
        }
        if(currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count) 
        {
            weapons[currentWeaponIndex].UnEquip();
        }

        weapons[nextWeaponIndex].Equip();
        currentWeaponIndex = nextWeaponIndex;
    }
}
