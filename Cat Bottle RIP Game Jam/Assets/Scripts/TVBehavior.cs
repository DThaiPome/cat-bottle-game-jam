using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVBehavior : MonoBehaviour
{
    public bool broken = false;
    public LevelManager levelM;

    // Start is called before the first frame update
    void Start()
    {
        this.levelM = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (broken && !this.gameOver())
        {
            levelM.LevelBeat();
        }
    }

    bool gameOver()
    {

        if (levelM == null)
        {

            return false;
        }
        else
        {

            return levelM.isGameOver;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameOver())
        {

            //call cat Function
            if (other.gameObject.CompareTag("Player"))
            {
                broken = true;
            }
        }

    }
}
