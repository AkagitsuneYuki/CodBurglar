using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float rayLength;

    private RaycastHit rayHit;
    public LayerMask layerMask;

    [SerializeField] private GameObject impactParticles;
    private GameObject particleRef;
    [SerializeField] private AudioClip shootSound;
 
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength);

        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<AudioSource>().PlayOneShot(shootSound);

            if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
            {
                particleRef = Instantiate(impactParticles, rayHit.point, Quaternion.identity);
                Destroy(particleRef, 0.5f);
                
                if (rayHit.transform.gameObject.tag == "Enemy")
                {
                    //This is where the enemy takes damage
                    rayHit.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
    }

}
