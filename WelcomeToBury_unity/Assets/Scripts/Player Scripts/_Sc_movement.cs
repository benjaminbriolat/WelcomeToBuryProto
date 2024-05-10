using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class _Sc_movement : MonoBehaviour
{
    public static _Sc_movement instance = null;
    Camera cam = null;
    NavMeshAgent agent = null;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float runSpeed = 12.0f;

    [SerializeField] float distanceFromPoint = 0.0f;
    [SerializeField] float distanceToRun = 15.0f;
    [SerializeField] LayerMask myLayerMask;
    [SerializeField] string layerName = "Walkable";
    private int LayerGround;

    public bool canSetSpeed = true;

    _Sc_debugCharAnimations _sc_debugCharAnimations = null;

    //Debug
    float defaultWalkSpeed;
    float defaultRunSpeed;
    float defaultRunDistance;

    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
        _sc_debugCharAnimations = GetComponent<_Sc_debugCharAnimations>();
    }
    private void Start()
    {
        cam = Camera.main;

        LayerGround = LayerMask.NameToLayer(layerName);
        defaultWalkSpeed = walkSpeed;
        defaultRunSpeed = runSpeed;
        defaultRunDistance = distanceToRun;

    }

    public void getMouseLeftClick(Vector2 _mousePos)
    {
        //Debug.Log("LeftCLickReceived");
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Pointer over UI, dont walk
        }
        else
        {
            Ray ray = cam.ScreenPointToRay(_mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity,myLayerMask))
            {
                if(hit.transform.gameObject.layer == LayerGround)
                {
                    agent.SetDestination(hit.point);
                    setSpeed(hit);
                    //Debug.Log("SetDestination");
                }
            }
        }
    }

    private void setSpeed(RaycastHit hit)
    {
        distanceFromPoint = Vector3.Distance(hit.point, this.transform.position);
        if (distanceFromPoint > distanceToRun)
        {
            agent.speed = runSpeed;
            //Debug.Log("Run");
        }
        else
        {
            if (canSetSpeed == true)
            {
                agent.speed = walkSpeed;
                //Debug.Log("Walk");
            }
        }
    }

    private void Update()
    {
        _sc_debugCharAnimations.setMoving(agent.velocity.magnitude);
    }

    
    /// Debug ///
    public void OnValuesChanged(float _newWalkSpeed, float _newRunSpeed, float _newRunDistance)
    {
        walkSpeed = _newWalkSpeed;
        runSpeed = _newWalkSpeed;
        distanceToRun = _newRunDistance;
    }
    public void OnResetValues()
    {
        walkSpeed = defaultWalkSpeed;
        runSpeed = defaultRunSpeed;
        distanceToRun = defaultRunDistance;
    }
    /// End Debug ///
}