using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    [SerializeField] private GameObject grenade;

    [SerializeField] private WeaponData data;

    [SerializeField] private WeaponManager manager;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<WeaponData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && data.GetLoadedAmmo() > 0)
        {
            ThrowGrenade();
        }
        if(data.GetLoadedAmmo() == 0)
        {
            manager.ChangeWeapon(1);
        }
    }

    private void ThrowGrenade()
    {
        data.DecreaseAmmo();

        GameObject test = Instantiate(grenade,
            GameObject.FindGameObjectWithTag("Player").transform.position + 
            GameObject.FindGameObjectWithTag("Player").transform.forward * 2 + GameObject.FindGameObjectWithTag("Player").transform.up * 0.5f,
            GameObject.FindGameObjectWithTag("Player").transform.rotation);
    }

}
