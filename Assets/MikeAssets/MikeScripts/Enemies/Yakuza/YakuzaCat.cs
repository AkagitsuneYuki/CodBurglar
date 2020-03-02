using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YakuzaCat : MonoBehaviour
{

    [SerializeField] private NavMeshAgent navMeshAgent;
    private GameObject target;

    [SerializeField] private int health;

    private RaycastHit rayHit;
    public LayerMask layerMask;
    [SerializeField] private float rayLength;

    [SerializeField] private bool attacking = false;
    [SerializeField] private bool walking = false;
    private bool inAttack;  //this is only needed to prevent the dude from repeatedly switching sprites


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 location = transform.position + transform.up;
        if (target != null)
        {
            //Debug.Log("Target found!");
            if (!inAttack)
            {
                //Debug.Log("We're not attacking");
                transform.LookAt(target.transform);
                transform.rotation = transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
                if (Physics.Raycast(location, transform.forward, out rayHit, rayLength, layerMask))
                {
                    Debug.DrawRay(transform.position, transform.forward, Color.red);
                    //Debug.Log("Shot the raycast");
                    if (rayHit.transform.gameObject.tag == "Player")
                    {
                        //Debug.Log("Found the player");
                        Vector2 curPos = new Vector2(location.x, location.z);
                        Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);

                        float dis = Vector2.Distance(curPos, playerPos);
                        // this is probably a shitty way to do this but I'm lazy so  ¯\_(ツ)_/¯
                        if (dis <= rayLength)
                        {
                            //Debug.Log("player is within the ray length");
                            if (dis > 1f)
                            {
                                //Debug.Log("The player is far enough to walk to");
                                navMeshAgent.SetDestination(target.transform.position);
                                walking = true;
                                attacking = false;
                            }
                            else
                            {
                                //Debug.Log("The player is very close");
                                navMeshAgent.SetDestination(transform.position);
                                attacking = true;
                                walking = false;
                                inAttack = true;
                                StartCoroutine(Attack());
                            }
                        }
                        else
                        {
                            //Debug.Log("The player is too far from the raycast");
                            navMeshAgent.SetDestination(transform.position);
                            walking = false;
                            attacking = false;
                        }
                    }
                    else
                    {
                        //Debug.Log("Hit " + rayHit.transform.gameObject.name);
                    }
                }
                else
                {
                    //Debug.Log("raycast did not detect anything");
                }
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                walking = false;
                //Debug.Log("There's no target");
            }
        }
    }

    private void OnAttack()
    {
        Vector3 location = transform.position + transform.up;
        if (Physics.Raycast(location, transform.forward, out rayHit, 1f, layerMask))
        {
            if (rayHit.transform.gameObject.tag == "Player")
            {
                Vector2 curPos = new Vector2(transform.position.x, transform.position.z);
                Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);
                float dis = Vector2.Distance(curPos, playerPos);
                int dmgToPlayer = Mathf.FloorToInt((5f * dis)) + Random.Range(-2, 2);
                rayHit.transform.gameObject.GetComponent<PlayerData>().DecreaseHP(dmgToPlayer);
            }
        }
    }

    IEnumerator Attack()
    {
        OnAttack();
        yield return new WaitForSeconds(1);
        attacking = false;
        yield return new WaitForSeconds(0.5f);
        inAttack = false;
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    public bool IsWalking()
    {
        return walking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sword")
        {
            TakeDamage(3);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
