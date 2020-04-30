using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    [SerializeField] private GameObject objToDestroy;   //  use this for the wall that the switch
    [SerializeField] private GameObject lever;          //  this is the object that rotates the lever
    [SerializeField] private GameObject KeyNeeded;

    private bool flipped;   //  is this switch flipped?

    void Start()
    {
        flipped = false;
    }

    public void PullTheLever()
    {
        if (!flipped && (KeyNeeded == null || !KeyNeeded.activeInHierarchy))
        {
            lever.transform.localRotation = Quaternion.Euler(-37, 0, 0);
            Destroy(objToDestroy);
            flipped = true;
        }
    }

}
