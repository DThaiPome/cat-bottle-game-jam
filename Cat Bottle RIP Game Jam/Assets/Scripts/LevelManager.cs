using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    public static bool isGameOver = false; 
    public string nextLevel;
   
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

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
