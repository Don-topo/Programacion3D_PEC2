using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float rotateVelocity = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, rotateVelocity * Time.deltaTime, 0f), Space.Self);
    }
}
