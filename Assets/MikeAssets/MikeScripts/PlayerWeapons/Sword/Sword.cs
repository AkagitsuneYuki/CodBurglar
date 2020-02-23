using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] private Animator anime;
    [SerializeField] private CapsuleCollider hitbox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (anime.GetBool("Swing") == false)
            {
                anime.SetBool("Swing", true);
            }
        }
    }

    private void SwingEnd()
    {
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        anime.SetBool("Swing", false);
    }
}
