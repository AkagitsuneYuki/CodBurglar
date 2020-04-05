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

    private int ammo;

    private void Update()
    {
        ammo = gunData.GetLoadedAmmo();
        //if the gun is an object, is active, and has ammo
        if (gun != null && gun.activeSelf && ammo > 0)
        {
            // create the position that the bullet hits, makes it random in relation to the area the player is aiming at
            forwardPos = (transform.forward * rayLength) + (transform.right * Random.Range(-0.5f, 0.5f)) + (transform.up * Random.Range(-0.5f, 0.5f));

            // draw the ray for where the bullet is going to go
            Debug.DrawRay(transform.position, forwardPos);

            // if the player is pressing the shoot button and can shoot
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                StartCoroutine(wait());
                canShoot = false;
            }

            // reload the gun with the r key
            if (Input.GetKeyDown(KeyCode.R) && ammo > 0 && ammo < gunData.GetMaxLoadedAmmo())
            {
                StartCoroutine(LocalReload());
            }
        }
        
    }

    // this makes the player wait between shooting
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
                //This is where the enemy takes damage, I should try to switch to using switches instead

                //gunner
                if(rayHit.transform.gameObject.TryGetComponent(out Enemy enemy))
                {
                    rayHit.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
                //jojo
                else if (rayHit.transform.gameObject.TryGetComponent(out MeleeCat jojo))
                {
                    rayHit.transform.gameObject.GetComponent<MeleeCat>().TakeDamage(damage);
                }
                //yakuza
                else if (rayHit.transform.gameObject.TryGetComponent(out YakuzaCat yakuza))
                {
                    rayHit.transform.gameObject.GetComponent<YakuzaCat>().TakeDamage(damage);
                }
                // ninja
                else if (rayHit.transform.gameObject.TryGetComponent(out Ninja ninjaCat))
                {
                    rayHit.transform.gameObject.GetComponent<Ninja>().TakeDamage(damage);
                }
            }
        }
        yield return new WaitForSeconds(0.25f);
        if(gunData.GetLoadedAmmo() == 0)
        {
            gunData.SetReloading(true);
            yield return new WaitForSeconds(2);
            gunData.Reload();
        }
        canShoot = true;
    }

    IEnumerator LocalReload()
    {
        canShoot = false;
        gunData.SetReloading(true);
        yield return new WaitForSeconds(2);
        gunData.Reload();
        canShoot = true;
    }


}
