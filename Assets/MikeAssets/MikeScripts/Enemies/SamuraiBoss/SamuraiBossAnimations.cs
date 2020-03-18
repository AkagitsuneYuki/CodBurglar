using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBossAnimations : MonoBehaviour
{

    [SerializeField] private Animator anime;
    [SerializeField] private bool isPlayerFighting;

    private int spinCounter = 0;
    [SerializeField] private int maxSpins;

    void Start()
    {
        anime = GetComponent<Animator>();
        if(anime == null)
        {
            Debug.Log("Mike fucked up, he forgot to give the boss an animator!");
        }

        isPlayerFighting = true;    //delete this line when finished debugging

        StartCoroutine(BossAI());
    }

    void Update()
    {
        IncreaseIdleTimer();
    }

    #region idle

    private void IncreaseIdleTimer()
    {
        if (anime.GetBool("IsIdle") && isPlayerFighting)
        {
            anime.SetFloat("IdleTimer", anime.GetFloat("IdleTimer") + Time.deltaTime);
        }
    }

    private void SetIdleCondition(bool newState)
    {
        anime.SetBool("IsIdle", newState);
    }

    private void ResetIdleTimer()
    {
        anime.SetFloat("IdleTimer", 0f);
        SetIdleCondition(true);
    }

    #endregion

    #region Attacks

    private void ExitChargeAtk()
    {
        anime.SetBool("ChargeAtk", false);
        ResetIdleTimer();
    }

    private void ExitSpinAtk()
    {
        if(spinCounter == maxSpins)     //set the maxspins variable to a relatively high amount, but not too high
        {
            anime.SetBool("SpinAtk", false);
            spinCounter = 0;
            ResetIdleTimer();
        }
        else
        {
            spinCounter++;
        }
    }

    private void ExitNormalAtk()
    {
        anime.SetBool("NormalAtk", false);
        ResetIdleTimer();
    }

    #endregion

    #region Walking
    // this can only work if the walking parameter is the last one
    private void BeginWalk()
    {
        // for every parameter
        for(int i = 0; i < anime.parameterCount; i++)
        {
            // if the parameter is a bool
            if (anime.parameters[i].GetType() == typeof(bool))
            {
                // if the parameter isn't walking
                if (anime.parameters[i].name != "Walking")
                {
                    // if the parameter is true then break
                    if (anime.GetBool(anime.parameters[i].name))
                    {
                        break;
                    }
                }
                // if the parameter is walking
                else
                {
                    anime.SetBool("Walking", true);
                }
            }
        }
    }

    #endregion

    IEnumerator BossAI()
    {
        while (true)
        {
            
            //SetIdleCondition(false);
            //anime.SetBool("ChargeAtk", true);
            yield return new WaitForSeconds(20);
        }
    }
}
