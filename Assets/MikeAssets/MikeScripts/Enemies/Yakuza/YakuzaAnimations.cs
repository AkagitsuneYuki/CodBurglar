using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakuzaAnimations : MonoBehaviour
{
    [SerializeField] private YakuzaCat yakuzaRef;
    [SerializeField] private Animator anime;

    [SerializeField] private Sprite walking;
    [SerializeField] private Sprite def;
    [SerializeField] private Sprite atk;

    private RaycastHit rayHit;
    public LayerMask layerMask;
    [SerializeField] private float rayLength;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    public void OnAttack()
    {
        yakuzaRef.OnAttack();
    }

    private void SetAnimation()
    {
        // i need to replace these with the final animations
        if (yakuzaRef.IsAttacking())
        {
            //GetComponent<SpriteRenderer>().sprite = atk;
            anime.SetBool("atk", true);
            //anime.SetBool("walk", false);
        }
        else
        {
            anime.SetBool("atk", false);
        }
        if (yakuzaRef.IsWalking())
        {
            //GetComponent<SpriteRenderer>().sprite = walking;
            
            anime.SetBool("walk", true);
        }
        else
        {
            anime.SetBool("walk", false);
        }
        /*else
        {
            //GetComponent<SpriteRenderer>().sprite = def;
            anime.SetBool("atk", false);
            anime.SetBool("walk", false);
        }*/
    }

    public void FinishAtk()
    {
        yakuzaRef.exitAtk();
    }
}
