using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplode : MonoBehaviour
{

    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().rotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("MainCamera");
        transform.rotation = player.transform.rotation;
        GetComponent<Rigidbody>().AddForceAtPosition(player.transform.forward * 1000, player.transform.position + player.transform.forward * 3);
        GetComponent<Rigidbody>().AddForce(Vector3.up*300);

        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject, 2);
    }


}
