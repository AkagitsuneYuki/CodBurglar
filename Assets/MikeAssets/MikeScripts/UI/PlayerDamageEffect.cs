using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageEffect : MonoBehaviour
{

    [SerializeField] private Image image;

    private int damageTimer = 0;
    private bool coolDown;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = true;

        StartCoroutine(ChangeAlpha());

        //print(image.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void OnHit(int dmg)
    {
        damageTimer += dmg;
        //coolDown = false;
    }

    IEnumerator ChangeAlpha()
    {
        while (true)//(damageTimer > 0)
        {
            if(Time.timeScale > 0)
            {
                if (coolDown && damageTimer > 0)
                {
                    if (damageTimer > 601)
                    {
                        damageTimer = 601;
                    }
                    damageTimer--;
                }
                if (damageTimer == 0)
                {
                    image.color = new Color(1, 1, 1, 0);
                }
                else
                {
                    float newAlpha = damageTimer / 255f;
                    if (newAlpha > 1f)
                    {
                        newAlpha = 1f;
                    }
                    image.color = new Color(1, 1, 1, newAlpha);
                }
            }
            yield return new WaitForSeconds(1/2);
        }
    }
}
