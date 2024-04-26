using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

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
    [SerializeField] bool camFollowActive = true;

    [Header("RotationValues")]
    [SerializeField] Quaternion targetAngle = Quaternion.identity;
    [SerializeField] float targetAngleX = 55;
    [SerializeField] float targetAngleY = 45;
    [SerializeField] float targetAngleZ = 0;

    [Header("Zoom Values")]
    [SerializeField] float camZoomPosition1 = 25;
    [SerializeField] float camZoomPosition2 = 15;
    [SerializeField] Vector3 deadZoneZoom = Vector3.zero;
    [SerializeField] Vector3 deadZoneNoZoom = Vector3.zero;


    Vector3 moveTemp = Vector3.zero;
    Transform camTransform = null;
    CinemachineVirtualCamera virCam = null;
    CinemachineFramingTransposer transposer = null;
    bool zoomed = false;
    bool zooming = false;


    //tempVariables    
    float tempDistance = 0;
    Vector2 tempTargetPos = Vector2.zero;
    Vector2 tempCamPos = Vector2.zero;
    private void Awake()
    {
        instance = this;
        camTransform = this.transform;
        virCam = camTransform.GetComponent<CinemachineVirtualCamera>();
        transposer = virCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        AssignTarget(defaultTarget);   
    }

    public void CallAnimCam(bool rotate = true, float rotValue = 0,float rotSpeed = 0)
    {
        if(rotate == true)
        {
           
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
            targetAngleY = 30;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetAngleY = 45;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetAngleY = 60;

        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            targetAngleX = 35;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            targetAngleX = 55;
        }

        targetAngle = Quaternion.Euler(targetAngleX, targetAngleY, targetAngleZ);

        if(camTransform.rotation != targetAngle)
        {
            AdjustCamAngle();
        }
    }

    public void AdjustCamZoom()
    {
        if(zoomed == true)
        {
            transposer.m_CameraDistance = Mathf.Lerp(transposer.m_CameraDistance, camZoomPosition2, lerpSmoothZoom * Time.deltaTime);
            transposer.m_XDamping = Mathf.Lerp(transposer.m_XDamping, deadZoneZoom.x, lerpSmoothZoom * Time.time);
            transposer.m_YDamping = Mathf.Lerp(transposer.m_YDamping, deadZoneZoom.y, lerpSmoothZoom * Time.time);
            transposer.m_ZDamping = Mathf.Lerp(transposer.m_ZDamping, deadZoneZoom.z, lerpSmoothZoom * Time.time);
        }
        else
        {
            transposer.m_CameraDistance = Mathf.Lerp(transposer.m_CameraDistance, camZoomPosition1, lerpSmoothZoom * Time.deltaTime); 
            transposer.m_XDamping = Mathf.Lerp(transposer.m_XDamping, deadZoneNoZoom.x, lerpSmoothZoom * Time.time);
            transposer.m_YDamping = Mathf.Lerp(transposer.m_YDamping, deadZoneNoZoom.y, lerpSmoothZoom * Time.time);
            transposer.m_ZDamping = Mathf.Lerp(transposer.m_ZDamping, deadZoneNoZoom.z, lerpSmoothZoom * Time.time);
        }
    }

    public void AdjustCamAngle()
    {
        Debug.Log("isUpdatingAngle");
        camTransform.rotation = Quaternion.Slerp(camTransform.rotation, targetAngle, lerpSmoothRot * Time.deltaTime);
    }

    public void AssignTarget(Transform newtarget)
    {
        currentTarget = newtarget;
        
    }    
}
