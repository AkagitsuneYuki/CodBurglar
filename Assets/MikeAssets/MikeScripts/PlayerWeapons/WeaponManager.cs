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
        //print(Input.GetAxis("Mouse ScrollWheel"));

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
