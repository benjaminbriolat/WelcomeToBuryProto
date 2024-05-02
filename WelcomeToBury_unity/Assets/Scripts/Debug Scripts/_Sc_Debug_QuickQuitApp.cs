using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_Debug_QuickQuitApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape))
       {
            Debug.Log("quit");
            Application.Quit();
       }
    }
}
