using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_cameraMovement : MonoBehaviour
{
     
    public static _Sc_cameraMovement instance = null;
    [SerializeField] Transform currentTarget = null;
    [SerializeField] Transform defaultTarget = null;
    [SerializeField] bool useBuffer = false;
    [SerializeField] float bufferDistance = 10.0f;
    [SerializeField] float lerpSmoothFollow = 1.0f;
    [SerializeField] float lerpSmoothZoom = 1.0f;
    [SerializeField] float lerpSmoothRot = 0.01f;
    [SerializeField] float camHeight = 18;
    [SerializeField] BoxCollider currentBound = null;
    [SerializeField] BoxCollider cameraBox = null;
    [SerializeField] bool camFollowActive = true;
    [SerializeField] Vector3 camZoomPosition1 = new Vector3(-15, 18, -13);
    [SerializeField] Vector3 camZoomPosition2 = new Vector3(-11.73f, 13.37f, -9.73f);
    [SerializeField] Quaternion targetAngle = Quaternion.identity;

    Vector3 moveTemp = Vector3.zero;
    Transform cameraChild = null;
    bool zoomed = false;
    bool zooming = false;
    [SerializeField] bool rotating = false;
  

    //tempVariables
    float tempDistance = 0;
    Vector2 tempTargetPos = Vector2.zero;
    Vector2 tempCamPos = Vector2.zero;
    private void Awake()
    {
        instance = this;
        AssignTarget(defaultTarget);
        cameraChild = transform.GetChild(0);
    }

    public void CallAnimCam(bool rotate = true, float rotValue = 0,float rotSpeed = 0)
    {
        if(rotate == true)
        {
            AssignTargetAngle(rotValue, rotSpeed);
            rotating = true;
        }
    }
    private void Update()
    {
        //DebugInputZoom
        if (Input.GetKeyDown(KeyCode.F))
        {
            zoomed = !zoomed;
            zooming = true;
        }
        if (zooming == true)
        {
            AdjustCamZoom();
        }



        //DebugInputAngle
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AssignTargetAngle(-15, 0.01f);
            rotating = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AssignTargetAngle(0, 0.01f);
            rotating = true;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AssignTargetAngle(15, 0.01f);
            rotating = true;

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AssignTargetAngle(20,0.01f);
            rotating = true;
            
        }
        

        //moveMethod3
        if(useBuffer == true)
        {
            if (camFollowActive == true)
            {
                if (currentTarget != null)
                {
                    tempCamPos = (new Vector2(transform.position.x, transform.position.z));
                    tempTargetPos = (new Vector2(currentTarget.position.x, currentTarget.position.z));

                    tempDistance = Vector2.Distance(tempTargetPos, tempCamPos);
                    if (useBuffer == true)
                    {
                        if (tempDistance > bufferDistance)
                        {
                            moveTemp = currentTarget.position;
                            moveTemp.y = camHeight;
                            transform.position = Vector3.MoveTowards(transform.position, moveTemp, lerpSmoothFollow * Time.deltaTime);
                        }
                    }
                    else if (tempDistance > 0)
                    {
                        moveTemp = currentTarget.position;
                        moveTemp.y = camHeight;
                        transform.position = Vector3.MoveTowards(transform.position, moveTemp, lerpSmoothFollow * Time.deltaTime);
                    }
                }
            }
        }               
    }

    private void LateUpdate()
    {
        if(camFollowActive == true)
        {
            if (currentTarget != null)
            {
                if(useBuffer == false)
                {
                    tempCamPos = (new Vector2(transform.position.x, transform.position.z));
                    tempTargetPos = (new Vector2(currentTarget.position.x, currentTarget.position.z));

                    tempDistance = Vector2.Distance(tempTargetPos, tempCamPos);
                    if (tempDistance > 0)
                    {
                        AdjustCamPosition();
                    }
                }                          
            }
        }

        if (rotating == true)
        {
            AdjustCamAngle();
        }
    }

    public void AdjustCamPosition()
    {
        //moveMethod1
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, currentTarget.position.x, lerpSmoothFollow), camHeight, Mathf.Lerp(transform.position.z, currentTarget.position.z, lerpSmoothFollow));
    }

    public void AdjustCamZoom()
    {
        if(zoomed == true)
        {
            cameraChild.localPosition = new Vector3(Mathf.Lerp(cameraChild.localPosition.x, camZoomPosition2.x, lerpSmoothZoom), Mathf.Lerp(cameraChild.localPosition.y, camZoomPosition2.y, lerpSmoothZoom), Mathf.Lerp(cameraChild.localPosition.z, camZoomPosition2.z, lerpSmoothZoom));
        }
        else
        {
            cameraChild.localPosition = new Vector3(Mathf.Lerp(cameraChild.localPosition.x, camZoomPosition1.x, lerpSmoothZoom), Mathf.Lerp(cameraChild.localPosition.y, camZoomPosition1.y, lerpSmoothZoom), Mathf.Lerp(cameraChild.localPosition.z, camZoomPosition1.z, lerpSmoothZoom));
        }
    }

    public void AdjustCamAngle()
    {
        //Debug.Log("currentAngle : " + transform.eulerAngles.y + " targetAngle: " + targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, lerpSmoothRot);              
    }


    public void AssignTarget(Transform newtarget)
    {
        currentTarget = newtarget;
    }

    public void AssignTargetAngle(float newAngleValue,float rotSpeed)
    {
        lerpSmoothRot = rotSpeed;
        targetAngle = Quaternion.Euler(transform.rotation.x, newAngleValue,transform.rotation.z);
    }
}
