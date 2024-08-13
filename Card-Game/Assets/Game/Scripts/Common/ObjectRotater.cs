using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotater : MonoBehaviour
{
    public Vector3 velocity;
    void FixedUpdate()
    {
        transform.Rotate(velocity);
    }
}
