using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;

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
    [SerializeField] _Sc_DebugCamSliders camSliders = null;

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
    [SerializeField] float deadZoneZoom = 1;
    [SerializeField] float deadZoneNoZoom = 3;
    [SerializeField] float targetZoomPosition = 0;
    [SerializeField] float targetDeadZone = 0;


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
    float zoomRatio = 0;
    float inclineRatio = 0;
    private void Awake()
    {
        instance = this;
        LoadVirtualCamData(true,null,null);
        targetZoomPosition = camZoomPosition1;
        targetDeadZone = deadZoneNoZoom;
        inclineRatio = (float)35 / 55;
        zoomRatio = (float)13 / 23;
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
            inclineRatio = (float)inclinaisonDestination / inclinaisonOrigin;
            zoomRatio = (float)camZoomPosition2 / camZoomPosition1;
            if(camSliders != null)
            {
                camSliders.SetSliders(camZoomPosition1, inclinaisonOrigin, zoomRatio, inclineRatio);
            }
        }
        

    }

    public void CallAnimCam(bool rotate = true, float rotValue = 0,float rotSpeed = 0)
    {
        if(rotate == true)
        {
           
        }
    }

    //Debug CAM BUILD TWEAK
    ///////
    public void OnchangedValueAngle(Slider newAngle)
    {
        inclinaisonOrigin = newAngle.value;
        inclinaisonDestination = newAngle.value * inclineRatio;
        targetAngleX = inclinaisonOrigin;
    }
    public void OnchangedValueZoom(Slider newZoom)
    {
        camZoomPosition1 = newZoom.value;
        camZoomPosition2 = newZoom.value * zoomRatio;
        zoomed = false;
        targetDeadZone = deadZoneNoZoom;
        targetZoomPosition = camZoomPosition1;
    }

    public void changeZoomRatio(Slider ratio)
    {
        zoomRatio = ratio.value;
        camZoomPosition2 = camZoomPosition1 * zoomRatio;
        if(zoomed == true)
        {
            targetZoomPosition = camZoomPosition2;
        }
    }

    public void changeInclineRatio(Slider ratio)
    {
        inclineRatio = ratio.value;
        inclinaisonDestination = inclinaisonOrigin * inclineRatio;
        if (isInclined == true)
        {
            targetAngleX = inclinaisonDestination;
        }
    }

    public void resetCamValues()
    {
        LoadVirtualCamData(true, null, null);
    }
    ///////

    public void ZoomPressed()
    {
        if(camZoomActive == true)
        {
            if(zoomed == false)
            {
                zoomed = true;
                targetDeadZone = deadZoneZoom;
                targetZoomPosition = camZoomPosition2;
            }
            else
            {
                zoomed = false;
                targetDeadZone = deadZoneNoZoom;
                targetZoomPosition = camZoomPosition1;
            }
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
        if (transposer.m_CameraDistance != targetZoomPosition)
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
            transposer.m_CameraDistance = Mathf.Lerp(transposer.m_CameraDistance, targetZoomPosition, lerpSmoothZoom * Time.deltaTime);
            transposer.m_XDamping = Mathf.Lerp(transposer.m_XDamping, targetDeadZone, lerpSmoothZoom * Time.time);
            transposer.m_YDamping = Mathf.Lerp(transposer.m_YDamping, targetDeadZone, lerpSmoothZoom * Time.time);
            transposer.m_ZDamping = Mathf.Lerp(transposer.m_ZDamping, targetDeadZone, lerpSmoothZoom * Time.time);          
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
