using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamuraiBossAI : MonoBehaviour
{

    [SerializeField] private GameObject player;

    [SerializeField] private SamuraiBossAnimations anime;

    [SerializeField] private NavMeshAgent navMesh;

    [SerializeField] private GameObject[] targetLocations;

    [SerializeField] private string fightState;
    [SerializeField] private string[] fightStates;

    private RaycastHit rayHit;
    [SerializeField] private int rayLength;
    [SerializeField] private LayerMask layerMask;

    private float disBetweenMeAndPlayer;

    // Start is called before the first frame update
    void Start()
    {
        fightState = fightStates[0];
    }

    // Update is called once per frame
    void Update()
    {
        FaceThePlayer();

        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
        disBetweenMeAndPlayer = Vector3.Distance(playerPos, myPos);
    }

    private void FaceThePlayer()
    {
        transform.LookAt(player.transform);
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
    }

    //Use this for when the fight begins. Use a trigger to initiate this.
    public void InitFight()
    {
        anime.SetPlayerFighting(true);
        StartCoroutine(BossAI());
    }

    #region AI_stuff

    IEnumerator BossAI()
    {

        float ChangeStateTimer = 0f;

        while (true)
        {
            //this should be more efficient i think
            switch (fightState)
            {
                case "idle":
                    ChangeStateTimer += Time.deltaTime;
                    WalkToANewSpot();
                    //fightState = fightStates[Mathf.FloorToInt(Random.Range(0, 2))];
                    if (ChangeStateTimer >= Random.Range(10f, 15f))
                    {
                        fightState = fightStates[Mathf.FloorToInt(Random.Range(0, 4))];
                    }
                    break;
                case "normal attack":
                    //if i'm not walking
                    if (!anime.GetWalking() && !anime.GetNormalAtk())
                    {
                        //begin walk
                        anime.BeginWalk();
                        //target the player
                        navMesh.SetDestination(player.transform.position);
                    }
                    //if i'm walking
                    else
                    {
                        //do the normal attack when close to the player
                        if (anime.GetNormalAtk() == false)
                        {
                            navMesh.SetDestination(player.transform.position);
                        }
                        if (disBetweenMeAndPlayer <= 5f && anime.GetNormalAtk() == false)
                        {
                            DoNormalAtk();
                        }
                    }
                    break;
                // Spin attack
                case "spin attack":
                    // set the target to the player and the anime to the spin attack
                    break;
                // else if doing the charge attack
                case "charged attack":
                    //  do the charge attack function
                    break;
                default:
                    Debug.Log("I think Mike fucked up?");
                    break;
            }
            yield return new WaitForSeconds((1/60));
        }
    }

    private void DoNormalAtk()
    {
        navMesh.SetDestination(transform.position);
        anime.BeginNormalAtk();
        AttackCalculation();
    }

    private void AttackCalculation()
    {
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            if (rayHit.transform.gameObject.tag == "Player")
            {
                if (disBetweenMeAndPlayer <= rayLength)
                {
                    player.GetComponent<PlayerData>().DecreaseHP(5);
                }
            }
        }
    }

    private void WalkToANewSpot()
    {
        //print("The distance is " + disBetweenMeAndPlayer + " units");
        if (disBetweenMeAndPlayer <= 5f)
        {
            int newTarget = Mathf.FloorToInt(Random.Range(0, targetLocations.Length));
            //move to a new target
            navMesh.SetDestination(targetLocations[newTarget].transform.localPosition);
        }
    }

    #endregion

}
