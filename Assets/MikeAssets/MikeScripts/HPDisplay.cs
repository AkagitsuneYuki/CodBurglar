using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPDisplay : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            print("wtf?");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            text.SetText("HP: " + player.GetComponent<PlayerData>().GetHP() + "/20");
        }
        else
        {
            text.SetText("HP: 0/20");
        }
    }
}
