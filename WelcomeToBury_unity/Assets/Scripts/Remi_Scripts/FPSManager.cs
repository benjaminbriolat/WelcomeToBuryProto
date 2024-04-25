using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    [SerializeField] int targetFPS = 120;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = targetFPS;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
