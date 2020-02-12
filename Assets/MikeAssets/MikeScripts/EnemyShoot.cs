using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float rayLength;

    private RaycastHit rayHit;
    public LayerMask layerMask;

    [SerializeField] GameObject player;
    [SerializeField] GameObject sprite;

    [SerializeField] private GameObject impactParticles;
    private GameObject particleRef;
    [SerializeField] private AudioClip shootSound;

    private bool canHitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootyAI());
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCanHitPlayer();


    }

    private void CheckIfCanHitPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= rayLength)
        {
            transform.LookAt(player.transform);
            if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
            {
                if (rayHit.transform.gameObject.tag == "Player")
                {
                    canHitPlayer = true;
                }
                else
                {
                    canHitPlayer = false;
                }
            }
            else
            {
                canHitPlayer = false;
            }
        }
        else
        {
            canHitPlayer = false;
        }
    }

    private void OnShoot()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound);

        if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
        {
            if (rayHit.transform.gameObject.tag == "Player")
            {
                int i = Mathf.FloorToInt(Random.Range(1f, 101f));
                if(i % 3 != 0)
                {
                    rayHit.transform.gameObject.GetComponent<PlayerData>().DecreaseHP(damage + (1 % 5));
                }
            }
        }

        //print(gameObject.name + " shot their load");
    }

    IEnumerator ShootyAI()
    {
        while (true)
        {
            if (canHitPlayer)
            {
                //print("can hit");
                OnShoot();
            }
            yield return new WaitForSeconds(Random.Range(1,3));
        }
    }

}
