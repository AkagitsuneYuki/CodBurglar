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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        if (yakuzaRef.IsAttacking())
        {
            GetComponent<SpriteRenderer>().sprite = atk;
        }
        else if (yakuzaRef.IsWalking())
        {
            GetComponent<SpriteRenderer>().sprite = walking;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = def;
        }
    }
}
