using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] private Animation anime;
    [SerializeField] private CapsuleCollider hitbox;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 blockPosition;
    private Quaternion blockRotation;

    private bool swing = false;
    private bool block = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;

        //These values were determined from playing in the editor, there should be a better way to do this
        blockPosition = new Vector3(0.158f, -0.262f, 0.652f);
        blockRotation = new Quaternion(-0.04986603f, 0.2032169f, -0.1565667f, 0.9652478f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!anime.isPlaying)
        {
            swing = false;

            if (Input.GetButtonDown("Fire1") && !block)
            {
                swing = true;
                anime.Play();
            }

            if (!swing)
            {
                if (Input.GetButton("Fire2"))
                {
                    if (!block)
                    {
                        block = true;
                        transform.localPosition = blockPosition;
                        transform.localRotation = blockRotation;
                    }
                }
                else
                {
                    block = false;
                    transform.localPosition = initialPosition;
                    transform.localRotation = initialRotation;
                }
            }
        }
        hitbox.enabled = swing;

    }

    public bool IsBlocking()
    {
        return block;
    }

}

