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

    }

    void Update()
    {
        IncreaseIdleTimer();
    }

    public void SetPlayerFighting(bool tf)
    {
        isPlayerFighting = tf;
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

    public void BeginNormalAtk()
    {
        for (int i = 0; i < anime.parameterCount; i++)
        {
            if (anime.parameters[i].type == AnimatorControllerParameterType.Bool)
            {
                if (anime.parameters[i].name != "NormalAtk")
                {
                    anime.SetBool(anime.parameters[i].name, false);
                }
                else
                {
                    anime.SetBool("NormalAtk", true);
                }
            }
        }
    }

    private void ExitNormalAtk()
    {
        anime.SetBool("NormalAtk", false);
        ResetIdleTimer();
    }

    public bool GetNormalAtk()
    {
        return anime.GetBool("NormalAtk");
    }

    #endregion

    #region Walking
    // this can only work if the walking parameter is the last one
    public void BeginWalk()
    {
        SetIdleCondition(false);
        // for every parameter
        for(int i = 0; i < anime.parameterCount; i++)
        {
            Debug.Log(anime.parameters[i].type);
            // if the parameter is a bool
            if (anime.parameters[i].type == AnimatorControllerParameterType.Bool)
            {
                
                // if the parameter isn't walking
                if (anime.parameters[i].name != "Walking")
                {
                    anime.SetBool(anime.parameters[i].name, false);
                }
                // if the parameter is walking
                else
                {
                    anime.SetBool("Walking", true);
                }
            }
        }
    }

    public bool GetWalking()
    {
        return anime.GetBool("Walking");
    }

    #endregion

}
