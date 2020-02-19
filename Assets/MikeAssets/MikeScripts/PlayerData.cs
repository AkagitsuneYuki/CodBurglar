using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasPinkKey()
    {
        return pinkKey;
    }

    public void DecreaseHP(int damage)
    {

        dmg.OnHit(damage * 32); //this changes how intense and how long the damage effect stays on screen

        hp -= damage;     //comment this out when debugging
        if(hp < 0)
        {
            hp = 0;
            SceneManager.LoadScene(1);
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
        if(other.name == "TutorialTrigger")
        {
            GameObject.FindGameObjectWithTag("Tutorial Text").GetComponent<TutorialText>().SwitchText();
            Destroy(other.gameObject);
        }

        if(other.name == "The Pink Key")
        {
            Destroy(other.gameObject);
            pinkKey = true;
        }

        if (other.name == "The Yellow Key")
        {
            Destroy(other.gameObject);
            yellowKey = true;
        }

        if (other.name == "The Pink Door")
        {
            if (pinkKey)
            {
                Destroy(other.gameObject);
            }

        }

        if (other.name == "The Yellow Door")
        {
            if (yellowKey)
            {
                Destroy(other.gameObject);
            }

        }

        if (other.name == "The Button")
        {
            other.GetComponent<Button>().DestroyWall();
            Destroy(other.gameObject);
        }

        if(other.tag == "HP")
        {
            IncreaseHP(10);
            Destroy(other.gameObject);
        }

    }
}
