using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this billboards the sprite
        transform.forward = Camera.main.transform.forward;
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
    }
}
