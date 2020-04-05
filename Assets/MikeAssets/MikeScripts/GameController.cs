using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject boss;
    private bool goingToTBC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!goingToTBC)
        {
            if(boss == null)
            {
                goingToTBC = true;
                StartCoroutine(GoToTBC());
            }
        }
    }

    IEnumerator GoToTBC()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);  //this is the tbc screen
    }

}
