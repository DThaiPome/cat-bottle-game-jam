using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDogBehavior : MonoBehaviour
{
    //public Vector3 startPosition;
    //public Vector3 endPosition;
    //Vector3 currentPoint;
    //int pointSelection;
    //public float moveLength = 3f;
    public float moveSpeed = 0.5f;

    public float minX = 0f;
    public float maxX = 0f;
    public float minY = 0f;
    public float maxY = 0f;

    static float t = 0f;

    public LevelManager levelM;
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if(!gameOver()){

       transform.position = new Vector3(Mathf.Lerp(minX, maxX, t),Mathf.Lerp(minY, maxY, t), 0);

        t += moveSpeed * Time.deltaTime;


        if (t > 1.0f)
        {
            float tempX = maxX;
            maxX = minX;
            minX = tempX;

            float tempY = maxY;
            maxY = minY;
            minY = tempY;
            t = 0.0f;
        }

        }

    }

    private void OnTriggerEnter2D(Collider2D other){

  if(!gameOver()){

        //call cat Function
        if(other.gameObject.CompareTag("Player")){


            PlayerStateMachine cat = other.gameObject.GetComponent<PlayerStateMachine>();

            Vector2 direction = cat.GetRollDirection();

            //Debug.Log(direction);

            if(direction == Vector2.up){

                cat.ChangeDirection(Vector2.down);


            }

            else if(direction == Vector2.down){

                cat.ChangeDirection(Vector2.up);

            }

            else if(direction == Vector2.left){

                cat.ChangeDirection(Vector2.right);
                //direction = cat.GetRollDirection();
                //Debug.Log(direction);

            }

            else if(direction == Vector2.right){

                cat.ChangeDirection(Vector2.left);

            }

        }
  }

    }




}
