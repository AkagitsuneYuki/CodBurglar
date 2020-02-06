using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private int weaponEquipped = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(weapons.Length == 0)
        {
            print("There are no weapons!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
