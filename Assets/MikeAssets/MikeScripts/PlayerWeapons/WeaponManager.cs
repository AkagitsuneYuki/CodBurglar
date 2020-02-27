﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private int weaponEquipped = 0;

    void Start()
    {
        if(weapons.Length == 0)
        {
            print("There are no weapons!");
        }
    }

    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ChangeWeapon((int)(Input.GetAxis("Mouse ScrollWheel") * 10));
        }
    }

    private void ChangeWeapon(int i)
    {
        weaponEquipped += i;

        if (weaponEquipped >= weapons.Length)
        {
            weaponEquipped = 0;
        }
        else if (weaponEquipped < 0)
        {
            weaponEquipped = weapons.Length - 1;
        }

        for(int k = 0; k < weapons.Length; k++)
        {
            if (k == weaponEquipped)
            {
                weapons[k].SetActive(true);
            }
            else
            {
                weapons[k].SetActive(false);
            }
        }
        
        if(weapons[weaponEquipped].GetComponent<WeaponData>().GetTotalAmmo() == 0)
        {
            // this recursively calls the change weapon until we have one that has ammo
            ChangeWeapon(1);
        }

    }

    public GameObject GetWeapon()
    {
        if(weapons != null)
        {
            if (weaponEquipped < weapons.Length)
            {
                return weapons[weaponEquipped];
            }
            else return null;
        }
        else return null;
    }

}
