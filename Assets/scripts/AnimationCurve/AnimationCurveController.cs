using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveController : MonoBehaviour
{
    [SerializeField] public float speed = 1f; //1.0f  // [] <--Attribute
    public Vector3 startPoint; //(x,y,z) r,g,b - red,green,blue
    public Vector3 endPoint;
    public float currentTime;
    public AnimationCurve curve;

    void Start()
    {
        Debug.Log($"Transform position: {startPoint}");
        startPoint = transform.position;
        Debug.Log($"Transform position: {transform.position}");
        Debug.Log($"Start position {startPoint}");
        endPoint = Vector3.Scale(startPoint, new Vector3(-1,1,1));
        Debug.Log($"End position: {endPoint}");

    }

    void Update()
    {
        currentTime += Time.deltaTime * speed;
        transform.position = Vector3.LerpUnclamped(startPoint, endPoint, curve.Evaluate(currentTime));      
    }
}
