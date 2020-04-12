using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ninja : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private GameObject target;

    [SerializeField] private int health;

    [SerializeField] private GameObject deathObject;

    private RaycastHit rayHit;
    public LayerMask layerMask;
    [SerializeField] private float rayLength;

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
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            GameObject fuck = Instantiate(deathObject, transform.position, transform.rotation);
            fuck.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
        if (target != null)
        {
            if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
            {
                if (rayHit.transform.gameObject.tag == "Player")
                {
                    Vector2 curPos = new Vector2(transform.position.x, transform.position.z);
                    Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);
                    float dis = Vector2.Distance(curPos, playerPos);
                    if (dis >= 10f)
                    {
                        navMeshAgent.SetDestination(target.transform.position);
                    }
                    else
                    {
                        navMeshAgent.SetDestination(target.transform.position + target.transform.forward * 10);
                    }
                }
            }
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
