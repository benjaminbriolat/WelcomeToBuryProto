using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_debugCanvasFaceCam : MonoBehaviour
{
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
