using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasing : MonoBehaviour
{
    [SerializeField] fieldOfView fov;

    public Transform Target;

    void Start()
    {
        Target = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    
    void Update()
    {
        if (fov.canSeePlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 3);
        }
    }
}


