using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float rayLength;

    [SerializeField] private int ammo;
    [SerializeField] private int maxAmmo;

    private RaycastHit rayHit;
    public LayerMask layerMask;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject sprite;
    [SerializeField] private Sprite shootSprite;
    private Sprite defaultSprite;

    [SerializeField] private GameObject impactParticles;
    private GameObject particleRef;
    [SerializeField] private AudioClip shootSound;

    private bool canHitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = sprite.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(ShootyAI());
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCanHitPlayer();
    }

    public bool GetCanHitplayer()
    {
        return canHitPlayer;
    }

    private void CheckIfCanHitPlayer()
    {
        transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.transform.position) <= rayLength)
        {
            
            if(ammo > 0)
            {
                if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
                {
                    if (rayHit.transform.gameObject.tag == "Player")
                    {
                        canHitPlayer = true;
                        //Debug.Log(gameObject.name + "can hit the player");
                    }
                    else
                    {
                        canHitPlayer = false;
                        //Debug.Log(gameObject.name + "can't hit the player because " + rayHit.transform.gameObject.name + " is in the way");
                    }
                }
                else
                {
                    canHitPlayer = false;
                    //Debug.Log(gameObject.name + "can't hit the player because the raycast hit nothing");
                }
            }
            else
            {
                canHitPlayer = false;
                //Debug.Log(gameObject.name + "can't hit the player because they have no ammo");
            }
        }
        else
        {
            canHitPlayer = false;
            //Debug.Log(gameObject.name + "can't hit the player because they're " + Vector3.Distance(transform.position, player.transform.position) + " units away");
        }
        transform.rotation = transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
    }

    private void OnShoot()
    {
        sprite.GetComponent<SpriteRenderer>().sprite = shootSprite;
        ammo--;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().pitch = Random.Range(0.5f, 3);
        GetComponent<AudioSource>().PlayOneShot(shootSound);

        if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
        {
            if (rayHit.transform.gameObject.tag == "Player")
            {
                int i = Mathf.FloorToInt(Random.Range(1f, 101f));
                if(i % 3 != 0)
                {
                    rayHit.transform.gameObject.GetComponent<PlayerData>().DecreaseHP(damage + (i % 5));
                }
            }
        }
    }

    IEnumerator ShootyAI()
    {
        while (true)
        {
            if (canHitPlayer)
            {
                //print("can hit");
                
                OnShoot();
                yield return new WaitForSeconds(0.5f);
                sprite.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
            else
            {
                sprite.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }

            if (ammo == 0)
            {
                yield return new WaitForSeconds(Random.Range(3, 5));
                ammo = maxAmmo;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range((1 / 20), 1f));
            }
        }
    }

}
