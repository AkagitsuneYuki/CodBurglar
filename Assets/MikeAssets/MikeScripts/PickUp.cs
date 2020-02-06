using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PickUp : MonoBehaviour
{

    [SerializeField] private AudioClip test;
    [SerializeField] private AudioSource playerAudio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "key")
        {
            GameObject key = other.gameObject;          //Get the key object
            //ParticleSystem keyParticles = key.GetComponent<ParticleSystem>();   //store the particle system of the key

            key.GetComponent<Renderer>().enabled = false;       //this is optional, this makes it so that the key looks like it's gone before being destroyed

            PlayerInventory.keyCount++;                 //increase the key count

            print("I have " + PlayerInventory.keyCount + " key(s)");

            //playerAudio.PlayOneShot(test);              //play the pickup sound
            //keyParticles.Play();                        //play the particle effect
            Destroy(key, 1f);//keyParticles.main.duration);   //destroy the key after the particle system finishes
        }

        if (other.gameObject.tag == "HP")
        {
            if(gameObject.GetComponent<PlayerData>().GetHP() < 20)
            {
                GameObject key = other.gameObject;          //Get the key object
                                                            //ParticleSystem keyParticles = key.GetComponent<ParticleSystem>();   //store the particle system of the key

                key.GetComponent<Renderer>().enabled = false;       //this is optional, this makes it so that the key looks like it's gone before being destroyed

                gameObject.GetComponent<PlayerData>().IncreaseHP(1);

                Destroy(key, 0f);//keyParticles.main.duration);   //destroy the key after the particle system finishes
            }
        }


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "door")
        {
            if (PlayerInventory.keyCount >= 1)
            {
                GameObject door = other.gameObject;          

                door.GetComponent<Renderer>().enabled = false;

                PlayerInventory.keyCount--;

                Destroy(door, 0f);
            }
        }
    }
}
