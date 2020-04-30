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
                //we make the jojo at the same y-pos as the player
                Vector3 rayPos = new Vector3(location.x, target.transform.position.y, location.z);
                //store the current pos in a buffer
                Vector3 buffer = transform.position;
                //set the pos to the new pos
                transform.position = rayPos;

                transform.LookAt(target.transform);
                if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
                {
                    //Debug.DrawRay(buffer + Vector3.up, transform.forward * 10, Color.red);

                    Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
                    transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
                    transform.position = buffer;
                    //Debug.Log("Shot the raycast");
                    if (rayHit.transform.gameObject.tag == "Player")
                    {
                        //Debug.Log("Found the player");

                        float dis = Vector3.Distance(transform.position, target.transform.position);
                        // this is probably a shitty way to do this but I'm lazy so  ¯\_(ツ)_/¯
                        if (dis <= rayLength)
                        {
                            //Debug.Log("player is within the ray length");
                            if (dis > 1.5f)
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
                }
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                walking = false;
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
