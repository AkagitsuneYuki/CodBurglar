﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int maxHP = 100;
    [SerializeField] private PlayerDamageEffect dmg;

    [SerializeField] private bool pinkKey;
    [SerializeField] private bool yellowKey;

    [SerializeField] private GameObject weaponManager;

    [SerializeField] private Sword theSword;

    [SerializeField] private SamuraiBossAI samBoss;

    public bool HasPinkKey()
    {
        return pinkKey;
    }

    public void DecreaseHP(int damage)
    {
        if (theSword.IsBlocking())      //this decreases the amount of damage when the player is blocking with the sword
        {
            if(Mathf.RoundToInt(Random.Range(1, 100)) % 2 == 1)
            {
                damage = 0;
            }
            else
            {
                if(damage > 1)
                {
                    damage = 1;
                }
            }
        }


        dmg.OnHit(damage * 32); //this changes how intense and how long the damage effect stays on screen

        hp -= damage;     //comment this out when debugging
        if(hp < 0)
        {
            hp = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void IncreaseHP(int damage)
    {
        hp += damage;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        //This should be much more efficient now!
        switch (other.name)
        {
            default:
                if (other.name.Contains("The Yellow Key"))
                {
                    Destroy(other.gameObject);
                    yellowKey = true;
                }//the second keys for levels 1 and 2
                if (other.tag == "HP")
                {
                    IncreaseHP(10);
                    Destroy(other.gameObject);
                }
                if (other.name.Contains("SwordItem"))    //this is incase there are more than 1 sword item on the field
                {
                    Destroy(other.gameObject);
                    weaponManager.GetComponent<WeaponManager>().ObtainWeapon(3);
                }
                break;
            case "TutorialTrigger":
                GameObject.FindGameObjectWithTag("Tutorial Text").GetComponent<TutorialText>().SwitchText();
                Destroy(other.gameObject);
                break;
            case "The Pink Key":
                Destroy(other.gameObject);
                pinkKey = true;
                break;
            case "The Pink Door":
                if (pinkKey)
                {
                    Destroy(other.gameObject);
                }
                break;
            case "The Yellow Door":
                if (yellowKey)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        SceneManager.LoadScene(2);
                    }
                    else if (SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        //SceneManager.LoadScene(3);
                        Destroy(other.gameObject);
                        samBoss.InitFight();
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }
                }
                break;
            case "The Button":
                other.GetComponent<Button>().DestroyWall();
                Destroy(other.gameObject);
                break;
        }
    }
}
