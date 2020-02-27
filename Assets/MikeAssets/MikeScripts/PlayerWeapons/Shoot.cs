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

    [SerializeField] private GameObject gun;
    Vector3 forwardPos;
    [SerializeField] private bool canShoot = true;

    [SerializeField] private WeaponData gunData;

    private void Update()
    {
        if (gun != null && gun.activeSelf && gunData.GetLoadedAmmo() > 0)
        {
            forwardPos = (transform.forward * rayLength) + (transform.right * Random.Range(-0.5f, 0.5f)) + (transform.up * Random.Range(-0.5f, 0.5f));

            Debug.DrawRay(transform.position, forwardPos);

            if (Input.GetButtonDown("Fire1"))
            {
                if (canShoot)
                {
                    StartCoroutine(wait());
                    canShoot = false;
                }
            }
        }
        
    }

    IEnumerator wait()
    {
        gunData.DecreaseAmmo();

        GetComponent<AudioSource>().PlayOneShot(shootSound);

        if (Physics.Raycast(transform.position, forwardPos, out rayHit, rayLength, layerMask))
        {
            particleRef = Instantiate(impactParticles, rayHit.point, Quaternion.identity);
            Destroy(particleRef, 0.5f);

            if (rayHit.transform.gameObject.tag == "Enemy")
            {
                //This is where the enemy takes damage

                Enemy enemy = rayHit.transform.gameObject.GetComponent<Enemy>();

                if (enemy == null)
                {
                    //melee cat
                    rayHit.transform.gameObject.GetComponent<MeleeCat>().TakeDamage(damage);
                }
                else
                {
                    //gunner
                    rayHit.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
        yield return new WaitForSeconds(0.25f);
        if(gunData.GetLoadedAmmo() == 0)
        {
            yield return new WaitForSeconds(2);
            gunData.Reload();
        }
        canShoot = true;
    }




}
