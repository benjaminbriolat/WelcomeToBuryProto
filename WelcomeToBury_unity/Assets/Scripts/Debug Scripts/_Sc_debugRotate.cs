using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_debugRotate : MonoBehaviour
{
    float speed = 75.0f;
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime) ;
    }
}
