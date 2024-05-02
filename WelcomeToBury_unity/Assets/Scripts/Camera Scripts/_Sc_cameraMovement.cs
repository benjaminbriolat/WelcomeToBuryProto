using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class _Sc_cameraMovement : MonoBehaviour
{
     
    public static _Sc_cameraMovement instance = null;
    [SerializeField] CinemachineBrain cinemachineBrain = null;
    [SerializeField] _SO_VirtualCameraMovements defaultVirtualCamData = null;
    [SerializeField] Transform defaultCamera = null;
    [SerializeField] bool useBuffer = false;
    [SerializeField] float bufferDistance = 10.0f;
    [SerializeField] float lerpSmoothZoom = 1.0f;
    [SerializeField] float lerpSmoothRot = 0.01f;
    [SerializeField] bool camZoomActive = true;
    [SerializeField] bool camInclineActive = true;

    [Header("RotationValues")]
    [SerializeField] Quaternion targetAngle = Quaternion.identity;
    [SerializeField] float targetAngleX = 55;
    [SerializeField] float targetAngleY = 45;
    [SerializeField] float targetAngleZ = 0;
    [SerializeField] float inclinaisonOrigin = 0;
    [SerializeField] float inclinaisonDestination = 0;

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
    bool isInclined = false;


    //tempVariables    
    float tempDistance = 0;
    Vector2 tempTargetPos = Vector2.zero;
    Vector2 tempCamPos = Vector2.zero;
    private void Awake()
    {
        instance = this;
        LoadVirtualCamData(true,null,null);
    }

    public void LoadVirtualCamData(bool useDefault ,_SO_VirtualCameraMovements virCamData, Transform targetCamera)
    {
        zoomed = false;
        if (useDefault == false)
        {
            camTransform = targetCamera;
            virCam = camTransform.GetComponent<CinemachineVirtualCamera>();
            transposer = virCam.GetCinemachineComponent<CinemachineFramingTransposer>();

            inclinaisonOrigin = virCamData.inclinaisonValueOrigin;
            inclinaisonDestination = virCamData.inclinaisonValueDestination;
            camZoomPosition1 = virCamData.zoomValueOrigin;
            camZoomPosition2 = virCamData.zoomValueDestination;
            camZoomActive = virCamData.zoomActive;
            camInclineActive = virCamData.inclineActive;
            targetAngleX = virCamData.defaultAngleX;
            targetAngleY = virCamData.defaultAngleY;
            targetAngleZ = virCamData.defaultAngleZ;

        }
        else
        {
            camTransform = defaultCamera;
            virCam = camTransform.GetComponent<CinemachineVirtualCamera>();
            transposer = virCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            inclinaisonOrigin = defaultVirtualCamData.inclinaisonValueOrigin;
            inclinaisonDestination = defaultVirtualCamData.inclinaisonValueDestination;
            camZoomPosition1 = defaultVirtualCamData.zoomValueOrigin;
            camZoomPosition2 = defaultVirtualCamData.zoomValueDestination;
            camZoomActive = defaultVirtualCamData.zoomActive;
            camInclineActive = defaultVirtualCamData.inclineActive;
            targetAngleX = defaultVirtualCamData.defaultAngleX;
            targetAngleY = defaultVirtualCamData.defaultAngleY;
            targetAngleZ = defaultVirtualCamData.defaultAngleZ;
        }
        
    }

    public void CallAnimCam(bool rotate = true, float rotValue = 0,float rotSpeed = 0)
    {
        if(rotate == true)
        {
           
        }
    }

    public void ZoomPressed()
    {
        if(camZoomActive == true)
        {
            zoomed = !zoomed;
            zooming = true;
        }       
    }

    public void InclinePressed()
    {
        if(camInclineActive == true)
        {
            if (isInclined == false)
            {
                isInclined = true;
                targetAngleX = inclinaisonDestination;
            }
            else
            {
                isInclined = false;
                targetAngleX = inclinaisonOrigin;
            }
        }        
    }
    public void LeftArrowPressed()
    {
        targetAngleY = 30;
    }
    public void DownArrowPressed()
    {
        targetAngleY = 45;
    }
    public void RightArrowPressed()
    {
        targetAngleY = 60;
    }
    private void Update()
    {
        if (zooming == true)
        {
            AdjustCamZoom();
        }

        targetAngle = Quaternion.Euler(targetAngleX, targetAngleY, targetAngleZ);

        if(camTransform.rotation != targetAngle)
        {
            AdjustCamAngle();
        }
    }

    public void AdjustCamZoom()
    {
        if (cinemachineBrain.IsBlending == false)
        {
            if (zoomed == true)
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
    }

    public void AdjustCamAngle()
    {
        Debug.Log("isUpdatingAngle");
        if(cinemachineBrain.IsBlending == false)
        {
            camTransform.rotation = Quaternion.Slerp(camTransform.rotation, targetAngle, lerpSmoothRot * Time.deltaTime);
        }
    } 
}
