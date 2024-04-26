using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_stickToPosition : MonoBehaviour
{
    [SerializeField] Transform target = null;
   
    void Update()
    {
        transform.position = target.position;
    }
}
