using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamuraiBossAI : MonoBehaviour
{

    [SerializeField] private GameObject player;

    [SerializeField] private SamuraiBossAnimations anime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceThePlayer();
    }

    private void FaceThePlayer()
    {
        transform.LookAt(player.transform);
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
    }

    //Use this for when the fight begins. Use a trigger to initiate this.
    public void InitFight()
    {
        anime.SetPlayerFighting(true);
    }
}
