using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplode : MonoBehaviour
{

    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = player.transform.rotation;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
        GetComponent<Rigidbody>().AddForce(transform.forward * 750);
        //GetComponent<Rigidbody>().rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


}
