using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingDogBehavior : MonoBehaviour
{

    // 0: facing up, 1: facing left, 2: facing down, 3: facing right
    public float directionfacing = 0f;
    public bool leftward = true;

    public LevelManager levelM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool gameOver(){

        if(levelM == null){

            return false;
        }
        else{

            return levelM.isGameOver;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

  if(!gameOver()){

        //call cat Function
        if (other.gameObject.CompareTag("Player"))
        {


            PlayerStateMachine cat = other.gameObject.GetComponent<PlayerStateMachine>();

            Vector2 direction = cat.GetRollDirection();

            //Debug.Log(direction);

            if (direction == Vector2.up && directionfacing == 2)
            {

                if (leftward)
                {
                    cat.ChangeDirection(Vector2.right);
                }
                else
                {
                    cat.ChangeDirection(Vector2.left);
                }


            }

            else if (direction == Vector2.down && directionfacing == 0)
            {

                if (leftward)
                {
                    cat.ChangeDirection(Vector2.left);
                }
                else
                {
                    cat.ChangeDirection(Vector2.right);
                }

            }

            else if (direction == Vector2.left && directionfacing == 3)
            {

                if (leftward)
                {
                    cat.ChangeDirection(Vector2.up);
                }
                else
                {
                    cat.ChangeDirection(Vector2.down);
                }

            }

            else if (direction == Vector2.right && directionfacing == 1)
            {

                if (leftward)
                {
                    cat.ChangeDirection(Vector2.up);
                }
                else
                {
                    cat.ChangeDirection(Vector2.down);
                }

            }

        }
  }

    }
}
