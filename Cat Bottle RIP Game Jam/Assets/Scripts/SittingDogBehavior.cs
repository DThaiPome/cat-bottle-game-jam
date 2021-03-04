using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingDogBehavior : MonoBehaviour
{

    public float directionfacing = 0f;
    public float leftward = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {



        //call cat Function
        if (other.gameObject.CompareTag("Player"))
        {


            PlayerStateMachine cat = other.gameObject.GetComponent<PlayerStateMachine>();

            Vector2 direction = cat.GetRollDirection();

            //Debug.Log(direction);

            if (direction == Vector2.up && directionfacing == 2)
            {

                if (leftward == 0)
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

                if (leftward == 0)
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

                if (leftward == 0)
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

                if (leftward == 0)
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
