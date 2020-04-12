using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeCat : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private GameObject target;

    [SerializeField] private int health;

    private RaycastHit rayHit;
    public LayerMask layerMask;
    [SerializeField] private float rayLength;

    [SerializeField] private GameObject deathObject;

    [SerializeField] private bool attacking = false;
    [SerializeField] private bool walking = false;
    private bool inAttack;  //this is only needed to prevent the dude from repeatedly switching sprites


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        //renderer = GetComponent<Renderer>();
        //savedMaterial = renderer.material;
    }

    void Update()
    {
        FollowPlayer();
    }

    public void TakeDamage(int dmg)
    {
        // CALL FLASH COROUTINE
        //StartCoroutine(Flash());

        health -= dmg;
        if (health <= 0)
        {
            GameObject fuck = Instantiate(deathObject, transform.position, transform.rotation);
            fuck.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            Destroy(gameObject);
        }
    }

    private void OnAttack()
    {
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, 1f, layerMask))
        {
            if (rayHit.transform.gameObject.tag == "Player")
            {
                Vector2 curPos = new Vector2(transform.position.x, transform.position.z);
                Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);
                float dis = Vector2.Distance(curPos, playerPos);
                //print("The player is " + dis + " units away from the attack");

                int dmgToPlayer = Mathf.FloorToInt((5f * dis)) + Random.Range(-2, 2);

                rayHit.transform.gameObject.GetComponent<PlayerData>().DecreaseHP(dmgToPlayer);


            }
        }
    }

    private void FollowPlayer()
    {
        if (target != null)
        {
            if (!inAttack)
            {
                transform.LookAt(target.transform);
                transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
                if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
                {
                    Debug.DrawRay(transform.position, transform.forward, Color.red);
                    if (rayHit.transform.gameObject.tag == "Player")
                    {
                        Vector2 curPos = new Vector2(transform.position.x, transform.position.z);
                        Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);

                        float dis = Vector2.Distance(curPos, playerPos);
                        // this is probably a shitty way to do this but I'm lazy so  ¯\_(ツ)_/¯
                        if (dis <= rayLength)
                        {
                            if (dis > 1f)
                            {
                                navMeshAgent.SetDestination(target.transform.position);
                                walking = true;
                                attacking = false;
                            }
                            else
                            {
                                navMeshAgent.SetDestination(transform.position);
                                attacking = true;
                                walking = false;
                                inAttack = true;
                                StartCoroutine(Attack());
                            }
                        }
                        else
                        {
                            navMeshAgent.SetDestination(transform.position);
                            walking = false;
                            attacking = false;
                        }
                    }
                }
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                walking = false;
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sword")
        {
            TakeDamage(3);
        }
    }


    public bool IsAttacking()
    {
        return attacking;
    }

    public bool IsWalking()
    {
        return walking;
    }

    //the coroutine for when this guy is attacking the player
    IEnumerator Attack()
    {
        OnAttack();
        // add the attacking part here

        yield return new WaitForSeconds(1);
        attacking = false;
        yield return new WaitForSeconds(0.5f);
        inAttack = false;
    }

}
