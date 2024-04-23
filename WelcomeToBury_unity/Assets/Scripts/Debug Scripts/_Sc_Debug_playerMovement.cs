using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_Debug_playerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody rb = null;
    [SerializeField] bool goRight = false;
    [SerializeField] bool goLeft = false;
    [SerializeField] bool goUp = false;
    [SerializeField] bool goDown = false;

    // tempVariables
    float tempX = 0;
    float tempZ = 0;
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            goUp = true;
        }
        else
        {
            goUp = false;
        }


        if (Input.GetKey(KeyCode.S))
        {
            goDown = true;
        }
        else
        {
            goDown = false;
        }



        if (Input.GetKey(KeyCode.A))
        {
            goLeft = true;
        }
        else
        {
            goLeft = false;
        }



        if (Input.GetKey(KeyCode.D))
        {
            goRight = true;
        }
        else
        {
            goRight = false;
        }


        if (goRight == true && goLeft == true)
        {
            tempX = 0;
        }
        else if(goRight == false && goLeft == false)
        {
            tempX = 0;
        }
        else if(goRight == true)
        {
            tempX = 1;
        }
        else if (goLeft == true)
        {
            tempX = -1;
        }


        if (goUp == true && goDown == true)
        {
            tempZ = 0;
        }
        else if (goUp == false && goDown == false)
        {
            tempZ = 0;
        }
        else if (goUp == true)
        {
            tempZ = 1;
        }
        else if (goDown == true)
        {
            tempZ = -1;
        }

        rb.velocity = new Vector3(tempX*speed, 0, tempZ*speed);
        Debug.Log(rb.velocity);
    }
}
