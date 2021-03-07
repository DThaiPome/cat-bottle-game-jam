using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class EndLevels : MonoBehaviour
{

    public void ResetGame(){

        Debug.Log("You have clicked to reset");
         SceneManager.LoadScene("Level1");


    }

    public void EndGame(){

         Debug.Log("You have clicked to end");
         Application.Quit();
        

    }

    public void CallMainMenu(){

        Debug.Log("You have clicked to reset");
         SceneManager.LoadScene("MainMenu");


    }


}
