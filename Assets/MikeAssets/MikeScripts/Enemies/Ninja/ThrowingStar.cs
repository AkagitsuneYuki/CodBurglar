using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingStar : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject player;

    [SerializeField] private SpriteRenderer sprite;
    private int flipCounter = 0;

    void Start()
    {
        //this was stupid
        //rb.velocity = new Vector3(0, 0, speed);
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);
    }

    void Update()
    {
        transform.position += transform.forward * speed;
    }

    private void FixedUpdate()
    {
        flipCounter++;
        if(flipCounter >= 3)    //this just makes the star look like it's spinning
        {
            flipCounter = 0;
            sprite.flipX = !sprite.flipX;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player){
            player.GetComponent<PlayerData>().DecreaseHP(Mathf.FloorToInt(Random.Range(1, 6)));
        }
        Destroy(gameObject);
    }
}
