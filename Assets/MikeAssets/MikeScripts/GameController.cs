using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{

    // the pause menu
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private FirstPersonController fps;
    [SerializeField] private WeaponManager playerWeapons;


    [SerializeField] private GameObject boss;
    private bool goingToTBC;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPause();

        if (!goingToTBC)
        {
            if(boss == null)
            {
                goingToTBC = true;
                //StartCoroutine(GoToTBC());
            }
        }
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        fps.enabled = false;
        playerWeapons.enabled = false;
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        fps.enabled = true;
        playerWeapons.enabled = true;
        //enable the scripts again
    }

    IEnumerator GoToTBC()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);  //this is the tbc screen
    }

}
