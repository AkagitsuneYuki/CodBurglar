using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int hp = 50;
    [SerializeField] private int maxHP = 50;
    [SerializeField] private PlayerDamageEffect dmg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHP(int damage)
    {

        dmg.OnHit(damage * 32); //this changes how intense and how long the damage effect stays on screen

        //hp -= damage;     //comment this out when debugging
        if(hp < 0)
        {
            hp = 0;
            //SceneManager.LoadScene(0);
        }
    }

    public void IncreaseHP(int damage)
    {
        hp += damage;
        if (hp > 20)
        {
            hp = 20;
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
}
