using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_debugCamera : MonoBehaviour
{
    /*
    private BoxCollider2D cameraBox = null;
    private Transform playerTransform = null;
    [SerializeField] float lerpSmooth = 1.0f;
    public _Sc_BoundaryManager currentBoundary = null;
    public _Sc_BoundaryManager previousBoundary = null;

    private _Sc_TransitionScreen _sc_transitionScreen = null;
    public bool TransitionDone = true;
    private float boundaryDistanceX = 0;
    private float boundaryDistanceY = 0;

    [SerializeField] LayerMask HumanCullingMask;
    [SerializeField] LayerMask GhostCullingMask;
    Camera cameraComponent = null;

    [SerializeField] bool cinematicMode = false;
    [SerializeField] Vector3 cinematicDebugTarget = Vector3.zero;
    _Sc_CameraAnimationsManager _sc_cameraAnimationManager = null;

    float originalLerpSmooth = 0;
    [SerializeField] float animationProximity = 0.3f;
    float distanceTemp = 0;
    bool endOnItsOwn = true;
    Transform specialtarget = null;
    GameObject boundariesParent = null;
    private void Awake()
    {
        instance = this;
        originalLerpSmooth = lerpSmooth;
        _sc_transitionScreen = transform.GetChild(0).GetChild(1).GetComponent<_Sc_TransitionScreen>();
        cameraComponent = transform.GetChild(0).GetComponent<Camera>();
    }

    void Start()
    {
        cameraBox = GetComponent<BoxCollider2D>();
        _sc_cameraAnimationManager = transform.GetComponent<_Sc_CameraAnimationsManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SetCullingMask(0);
        /*if(currentBoundary == null)
        {
           boundariesParent = GameObject.Find("All Boundary Managers");
           if(boundariesParent != null)
           {
               newBoundary(boundariesParent.transform.GetChild(0).GetComponent<_Sc_BoundaryManager>());
           }
        }*/
/*    }

    public void LaunchCameraAnimation(Vector3 newtarget, float speed, float destinationDistance, bool selfStop, bool endAnim, Transform targetSent)
    {
        if (endAnim == false)
        {
            lerpSmooth = speed;
            endOnItsOwn = selfStop;
            specialtarget = targetSent;
            animationProximity = destinationDistance;
            cinematicDebugTarget = newtarget;
            cinematicMode = true;
            //launchAnim(speed);
        }
        else
        {
            lerpSmooth = originalLerpSmooth;
            cinematicMode = false;
        }
    }



    void LateUpdate()
    {
        CheckBoundaryToFollow();
    }

    void CheckBoundaryToFollow()
    {
        if (currentBoundary != null)
        {
            FollowPlayer();
        }
    }
*/
/*
    void FollowPlayer()
    {
        if (cinematicMode == false)
        {
            if (GameObject.Find("Boundary") != null)
            {
                GameObject _Boundary = GameObject.Find("Boundary");
                BoxCollider2D _BoundaryCollider = _Boundary.GetComponent<BoxCollider2D>();
                transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, playerTransform.position.x, lerpSmooth), _BoundaryCollider.bounds.min.x + cameraBox.size.x / 2, _BoundaryCollider.bounds.max.x - cameraBox.size.x / 2),
                                                  Mathf.Clamp(Mathf.Lerp(transform.position.y, playerTransform.position.y, lerpSmooth), _BoundaryCollider.bounds.min.y + cameraBox.size.y / 2, _BoundaryCollider.bounds.max.y - cameraBox.size.y / 2),
                                                  transform.position.z);
            }
        }
        else
        {
            if (GameObject.Find("Boundary") != null)
            {
                if (specialtarget != null)
                {
                    GameObject _Boundary = GameObject.Find("Boundary");
                    BoxCollider2D _BoundaryCollider = _Boundary.GetComponent<BoxCollider2D>();
                    transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, specialtarget.position.x, lerpSmooth), _BoundaryCollider.bounds.min.x + cameraBox.size.x / 2, _BoundaryCollider.bounds.max.x - cameraBox.size.x / 2),
                                                      Mathf.Clamp(Mathf.Lerp(transform.position.y, specialtarget.position.y, lerpSmooth), _BoundaryCollider.bounds.min.y + cameraBox.size.y / 2, _BoundaryCollider.bounds.max.y - cameraBox.size.y / 2),
                                                      transform.position.z);
                }
                else
                {
                    if (_sc_cameraAnimationManager.isInWait == false)
                    {
                        distanceTemp = Vector2.Distance(transform.position, cinematicDebugTarget);
                        //Debug.Log("anim checkBoundary " + distanceTemp);
                        if (distanceTemp > animationProximity)
                        {
                            //Debug.Log("anim farEnough ");
                            GameObject _Boundary = GameObject.Find("Boundary");
                            BoxCollider2D _BoundaryCollider = _Boundary.GetComponent<BoxCollider2D>();
                            transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, cinematicDebugTarget.x, lerpSmooth), _BoundaryCollider.bounds.min.x + cameraBox.size.x / 2, _BoundaryCollider.bounds.max.x - cameraBox.size.x / 2),
                                                              Mathf.Clamp(Mathf.Lerp(transform.position.y, cinematicDebugTarget.y, lerpSmooth), _BoundaryCollider.bounds.min.y + cameraBox.size.y / 2, _BoundaryCollider.bounds.max.y - cameraBox.size.y / 2),
                                                              transform.position.z);
                        }
                        else
                        {
                            //Debug.Log("anim launchArrival");                      
                            _sc_cameraAnimationManager.launchArrivalDelay();
                        }
                    }
                }

            }
        }
    }*/

    /*public void launchAnim(float duration)
    {
        if (GameObject.Find("Boundary") != null)
        {
            GameObject _Boundary = GameObject.Find("Boundary");
            BoxCollider2D _BoundaryCollider = _Boundary.GetComponent<BoxCollider2D>();            
            for (float t = duration; t > 0; t -= Time.deltaTime)
            {
                Debug.Log("duration is " + duration + " and t is = " + t);
                transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 0, 5), t / duration);
                transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, cinematicDebugTarget.x, duration), _BoundaryCollider.bounds.min.x + cameraBox.size.x / 2, _BoundaryCollider.bounds.max.x - cameraBox.size.x / 2),
                                              Mathf.Clamp(Mathf.Lerp(transform.position.y, cinematicDebugTarget.y,  duration), _BoundaryCollider.bounds.min.y + cameraBox.size.y / 2, _BoundaryCollider.bounds.max.y - cameraBox.size.y / 2),
                                              transform.position.z);
            }            
        }        
        // put code to to tell when the lerping action is done
    }*/

  /*  private int TransitionSide()
    {
        // 1 = RIGHT. 2 = LEFT. 3 = UP. 4 = DOWN
        boundaryDistanceX = (Mathf.Abs((currentBoundary.transform.position - previousBoundary.transform.position).x));
        boundaryDistanceY = (Mathf.Abs((currentBoundary.transform.position - previousBoundary.transform.position).y));

        if (boundaryDistanceX > boundaryDistanceY)
        {
            if (currentBoundary.transform.position.x > previousBoundary.transform.position.x)
            {
                return 1;
            }
            else if (currentBoundary.transform.position.x < previousBoundary.transform.position.x)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        else if (boundaryDistanceX < boundaryDistanceY)
        {
            if (currentBoundary.transform.position.y > previousBoundary.transform.position.y)
            {
                return 3;
            }
            else if (currentBoundary.transform.position.y < previousBoundary.transform.position.y)
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    public void newBoundary(_Sc_BoundaryManager _boundary)
    {
        Debug.Log("PlayerIs NewBoundCalled ");
        if (currentBoundary != _boundary)
        {
            Debug.Log("PlayerIs isNew ");
            previousBoundary = currentBoundary;
            currentBoundary = _boundary;

            //Debug.Log("BoundaryTrue " + currentBoundary.name);

        }
    }

    public void SetCullingMask(int _state)
    {
        if (_state == 0) //Human Form
        {
            cameraComponent.cullingMask = HumanCullingMask;
        }
        else if (_state == 1)
        {
            cameraComponent.cullingMask = GhostCullingMask;
        }
    }
*/
}
