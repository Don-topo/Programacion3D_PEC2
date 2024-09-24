using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform[] points;

    private int currentPosition = 0;
    [SerializeField] private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[currentPosition].position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPosition();
        MovePlatform();
    }

    void CheckPosition()
    {
        if (Vector3.Distance(transform.position, points[currentPosition].position) < 0.01f)
        {
            currentPosition = (currentPosition + 1) % points.Length;
        }
    }

    void MovePlatform()
    {
        transform.position = Vector3.Lerp(transform.position, points[currentPosition].position, Time.deltaTime * speed);                        
    }
}