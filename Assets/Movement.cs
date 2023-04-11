using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.TransformDirection(Vector3.back) * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * speed;
        }

    }
}
