using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] private Vector3 spawnPos;


    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "KillBox")
        {
            transform.position = spawnPos;
        }
        if (other.tag == "Checkpoint")
        {
            spawnPos = other.gameObject.transform.position;
        }
    }

}
