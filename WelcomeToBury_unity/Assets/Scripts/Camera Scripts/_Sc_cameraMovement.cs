using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_cameraMovement : MonoBehaviour
{
     
    public static _Sc_cameraMovement instance = null;
    [SerializeField] Transform currentTarget = null;
    [SerializeField] Transform defaultTarget = null;
    [SerializeField] float bufferDistance = 10.0f;
    [SerializeField] float lerpSmooth = 1.0f;
    [SerializeField] float camHeight = 18;
    [SerializeField] BoxCollider currentBound = null;
    [SerializeField] BoxCollider cameraBox = null;
    [SerializeField] bool camFollowActive = true;
 

    //tempVariables
    float tempDistance = 0;
    Vector2 tempTargetPos = Vector2.zero;
    Vector2 tempCamPos = Vector2.zero;
    private void Awake()
    {
        instance = this;
        assignTarget(defaultTarget);
    }
    
    private void LateUpdate()
    {
        if(camFollowActive == true)
        {
            if (currentTarget != null)
            {
                tempCamPos = (new Vector2(transform.position.x, transform.position.z));
                tempTargetPos = (new Vector2(currentTarget.position.x, currentTarget.position.z));

                tempDistance = Vector2.Distance(tempTargetPos, tempCamPos);
                if (tempDistance > bufferDistance)
                {
                    AdjustCamPosition();
                }
            }
        }        
    }

    public void AdjustCamPosition()
    {
        //Bound method
        transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, currentTarget.position.x, lerpSmooth), currentBound.bounds.min.x + cameraBox.size.x / 2, currentBound.bounds.max.x - cameraBox.size.x / 2),
                                                 camHeight,
                                                 Mathf.Clamp(Mathf.Lerp(transform.position.z, currentTarget.position.z, lerpSmooth), currentBound.bounds.min.z + cameraBox.size.z / 2, currentBound.bounds.max.z - cameraBox.size.z / 2));

        
        /*
        Debug.Log("cam X: " + transform.position.x + " target X: " + currentTarget.position.x);
        Debug.Log("cam Z: " + transform.position.z + " target Z: " + currentTarget.position.z);
        */

    }


    public void assignTarget(Transform newtarget)
    {
        currentTarget = newtarget;
    }
}
