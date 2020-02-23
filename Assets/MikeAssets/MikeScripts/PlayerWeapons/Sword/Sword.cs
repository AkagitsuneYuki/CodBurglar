using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] private Animation anime;
    [SerializeField] private CapsuleCollider hitbox;

    private bool swing = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!anime.isPlaying)
        {
            swing = false;
            if (Input.GetButtonDown("Fire1"))
            {
                swing = true;
                anime.Play();
            }
        }
        hitbox.enabled = swing;

    }


}

