﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private GameObject target;

    [SerializeField] private int health;

    private RaycastHit rayHit;
    public LayerMask layerMask;
    [SerializeField] private float rayLength;

    [SerializeField] private GameObject deathObject;

    [SerializeField] private Renderer renderer;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material savedMaterial;
    [SerializeField] private int flashCount;
    [SerializeField] private float waitTime;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        renderer = GetComponent<Renderer>();
        savedMaterial = renderer.material;
    }

    private void Update()
    {
        FollowPlayer();

        if(target != null)
        {
            //Debug.DrawLine(transform.position, target.transform.position, Color.red);
        }

    }

    public void TakeDamage(int dmg)
    {
        // CALL FLASH COROUTINE
        //StartCoroutine(Flash());

        health -= dmg;
        if(health <= 0)
        {
            GameObject fuck = Instantiate(deathObject, transform.position, transform.rotation);
            fuck.transform.position = new Vector3(transform.position.x, transform.position.y - 1.019f, transform.position.z);
            Destroy(gameObject);
        }
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < flashCount; i++)
        {
            renderer.material = highlightMaterial;

            yield return new WaitForSeconds(waitTime);

            renderer.material = savedMaterial;

            yield return new WaitForSeconds(waitTime);

        }
    }

    //I'm gonna have to ask someone how to do this in a way that's better for the player, should probably ask TJ?
    private void FollowPlayer()
    {
        if(target != null)
        {
            //store the current pos in a buffer
            Vector3 buffer = transform.position;
            if (target.transform.position.y > transform.position.y)
            {
                Vector3 location = transform.position;
                //we make the jojo at the same y-pos as the player
                Vector3 rayPos = new Vector3(location.x, target.transform.position.y, location.z);

                //set the pos to the new pos
                transform.position = rayPos;
            }
            if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
            {
                if (rayHit.transform.gameObject.tag == "Player")
                {

                    //Debug.Log(gameObject.name + " is following " + target.name);

                    Vector2 curPos = new Vector2(transform.position.x, transform.position.z);
                    Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);

                    float dis = Vector2.Distance(curPos, playerPos);
                    if (dis <= rayLength / 3 || dis >= rayLength * 1.25f)
                    {
                        if (GetComponent<EnemyShoot>().GetCanHitplayer())
                        {
                            navMeshAgent.SetDestination(transform.position);
                        }
                        else
                        {
                            if (dis >= 3f)
                            {
                                navMeshAgent.SetDestination(target.transform.position);
                            }
                            else
                            {
                                navMeshAgent.SetDestination(transform.position);
                            }
                        }
                    }
                    else
                    {
                        navMeshAgent.SetDestination(target.transform.position);
                    }
                }
            }

            transform.position = buffer;
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sword")
        {
            TakeDamage(3);
        }
    }

}
