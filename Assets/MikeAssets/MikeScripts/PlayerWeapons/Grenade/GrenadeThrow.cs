using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    [SerializeField] private GameObject grenade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {

        GameObject test = Instantiate(grenade,
            GameObject.FindGameObjectWithTag("Player").transform.position + 
            GameObject.FindGameObjectWithTag("Player").transform.forward * 2 + GameObject.FindGameObjectWithTag("Player").transform.up * 0.5f,
            GameObject.FindGameObjectWithTag("Player").transform.rotation);
    }

}
