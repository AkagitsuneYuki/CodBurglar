using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{

    [SerializeField] private GameObject weapons;

    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        // delete this object if I fucked up and didn't assign the variables in the inspector
        if(weapons == null)
        {
            Debug.Log("Mike fucked up and forgot to assign the weapons object on the ammo display, go bitch to him about it");
            Destroy(gameObject);
        }
        if(text == null)
        {
            Debug.Log("Mike fucked up and forgot to assign the text on the ammo display, go bitch to him about it");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        GameObject curWeapon = weapons.GetComponent<WeaponManager>().GetWeapon();
        WeaponData curWeaponData = curWeapon.GetComponent<WeaponData>();

        if(curWeapon.name == "EmptyHand")
        {
            text.SetText("");
        }
        else if(curWeapon.name == "pistol")
        {
            text.SetText(curWeaponData.GetLoadedAmmo() + "/" + curWeaponData.GetMaxLoadedAmmo());
        }
        else if(curWeapon.name == "grenade")
        {
            text.SetText(curWeaponData.GetLoadedAmmo() + "/" + curWeaponData.GetMaxLoadedAmmo());
        }
        else if(curWeapon.name == "Sword")
        {
            text.SetText("");
        }
        else
        {
            text.SetText("Mike fucked up somewhere");
        }
    }
}
