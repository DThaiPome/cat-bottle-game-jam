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
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

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

    private void OnCollisionEnter(Collision collision){

        //call cat Function

    }




}
