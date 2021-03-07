using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    public bool isGameOver = false; 
    public string nextLevel;
    public Text gameText;
    public Image textBackground;
   
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        gameText.gameObject.SetActive(false);
        textBackground.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if(!isGameOver){

        if (Input.GetKeyDown(KeyCode.R)) {

           

            LevelReset();
     
        }
        }
    }


    public void LevelReset() {

         Debug.Log("Level Reset");

        isGameOver = true;

       Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat() {

         Debug.Log("Level Beat");

        gameText.gameObject.SetActive(true);
        textBackground.gameObject.SetActive(true);
        isGameOver = true;

        Invoke("LoadNextLevel", 2);
    
    }

    void LoadNextLevel() {

         SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel(){


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
