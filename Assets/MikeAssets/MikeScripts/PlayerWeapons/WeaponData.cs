using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{

    [SerializeField] private int ammo;          //the total ammo the weapon has
    [SerializeField] private int maxAmmo;       //how much ammo the player can carry

    [SerializeField] private int loadedAmmo;    //how much ammo is loaded into the weapon
    [SerializeField] private int maxLoadedAmmo; //how much ammo can be loaded into the weapon

    [SerializeField] private bool infiniteAmmo; //does the weapon have infinite ammo

    public int GetLoadedAmmo()
    {
        return loadedAmmo;
    }

    public int GetMaxLoadedAmmo()
    {
        return maxLoadedAmmo;
    }

    public int GetTotalAmmo()
    {
        return ammo;
    }

    public void DecreaseAmmo()
    {
        if (loadedAmmo > 0)
        {
            loadedAmmo--;
        }
    }

    public void Reload()
    {
        if (!infiniteAmmo)
        {
            ammo -= maxLoadedAmmo;
            if (ammo < 0)
            {
                loadedAmmo = (maxLoadedAmmo + ammo);
            }
            else
            {
                loadedAmmo = maxLoadedAmmo;
            }
        }
        else
        {
            loadedAmmo = maxLoadedAmmo;
        }
    }

    public void PickupAmmo(int addedAmmo)
    {
        ammo += addedAmmo;
        if(ammo >= maxAmmo)
        {
            ammo = maxAmmo;
        }
    }

}
