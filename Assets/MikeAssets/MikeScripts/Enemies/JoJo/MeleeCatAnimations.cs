using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCatAnimations : MonoBehaviour
{

    [SerializeField] private MeleeCat meleeCatReference;
    [SerializeField] private Animator anime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationForJoJo();
    }

    private void SetAnimationForJoJo()
    {
        anime.SetBool("Walking", meleeCatReference.IsWalking());
        anime.SetBool("Attacking", meleeCatReference.IsAttacking());
    }

}
