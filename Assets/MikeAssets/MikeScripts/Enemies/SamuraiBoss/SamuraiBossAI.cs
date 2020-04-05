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
        WalkToANewSpot();

        float ChangeStateTimer = 0f;    //this is to keep track of how long the boss has been in the current state
        float positionTimer = 0f;

        while (true)
        {
            ChangeStateTimer += Time.deltaTime;

            //this should be more efficient i think
            switch (fightState)
            {
                case "idle":
                    positionTimer += Time.deltaTime;
                    if(positionTimer > Random.Range(3f, 5f))
                    {
                        WalkToANewSpot();
                        positionTimer = 0;
                    }
                    if (ChangeStateTimer >= Random.Range(10f, 15f))
                    {
                        positionTimer = 0;
                        fightState = fightStates[Mathf.FloorToInt(Random.Range(0, 4))];
                        //fightState = fightStates[2];    //testing charged attack

                        ChangeStateTimer = 0;
                    }
                    break;
                case "normal attack":
                    if (!anime.GetWalking() && !anime.GetNormalAtk())   //if i'm not walking
                    {
                        anime.BeginWalk();  //begin walk
                        navMesh.SetDestination(player.transform.position);  //target the player
                    }

                    else    //if i'm walking
                    {
                        //do the normal attack when close to the player
                        if (anime.GetNormalAtk() == false)
                        {
                            navMesh.SetDestination(player.transform.position);
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.5f);
                            fightState = fightStates[0];
                            WalkToANewSpot();
                            break;
                        }
                        if (disBetweenMeAndPlayer <= rayLength && anime.GetNormalAtk() == false)
                        {
                            DoNormalAtk();
                        }
                    }
                    break;
                // Spin attack
                case "spin attack":
                    // set the target to the player and the anime to the spin attack
                    Debug.Log("spin attack");
                    if (!anime.GetSpinAtk())
                    {
                        anime.BeginSpinAttack();
                        //break;
                    }
                    else
                    {
                        positionTimer += Time.deltaTime;
                        if(ChangeStateTimer >= Random.Range(7f, 12f))
                        {
                            anime.ExitSpinAtk();
                            fightState = fightStates[0];
                            //break;
                        }
                        else
                        {
                            if(positionTimer >= 1)
                            {
                                AttackCalculation();
                                positionTimer = 0;
                            }
                            navMesh.SetDestination(player.transform.position);
                        }
                    }
                    break;
                // else if doing the charge attack
                case "charged attack":
                    //  do the charge attack function

                    fightState = fightStates[0];
                    break;
                default:
                    Debug.Log("I think Mike fucked up? Check the samurai boss ai script");
                    fightState = fightStates[0];
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
                    player.GetComponent<PlayerData>().DecreaseHP(5);    //need to do something about this
                }
            }
        }
    }

    private void WalkToANewSpot()
    {
        int newTarget = Mathf.FloorToInt(Random.Range(0, targetLocations.Length));
        //move to a new target

        Debug.Log("Moving to target " + newTarget);

        navMesh.SetDestination(targetLocations[newTarget].transform.localPosition);
    }

    #endregion

}
