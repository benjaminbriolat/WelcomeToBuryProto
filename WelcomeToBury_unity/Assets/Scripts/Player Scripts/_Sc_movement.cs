using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

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
    _Sc_cerveau _sc_cerveau = null;

    [SerializeField] Transform walkSpeedUi;
    [SerializeField] Transform walkSpeedSliderUi;

    [SerializeField] Transform runSpeedUi;
    [SerializeField] Transform runSpeedSliderUi;

    [SerializeField] Transform runDistanceUi;
    [SerializeField] Transform runDistanceSliderUi;


    //Debug
    float defaultWalkSpeed;
    float defaultRunSpeed;
    float defaultRunDistance;

    public bool NewCustomDestination = false;

    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
        _sc_debugCharAnimations = GetComponent<_Sc_debugCharAnimations>();
    }
    private void Start()
    {
        cam = Camera.main;
        _sc_cerveau = _Sc_cerveau.instance;
        LayerGround = LayerMask.NameToLayer(layerName);
        defaultWalkSpeed = walkSpeed;
        defaultRunSpeed = runSpeed;
        defaultRunDistance = distanceToRun;

        SetUiDebugValues();
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
                    NewCustomDestination = true;
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
        }
        else
        {
            if (canSetSpeed == true)
            {
                agent.speed = walkSpeed;
            }
        }
    }
    private void setSpeedFromNavHit(NavMeshHit hit)
    {
        distanceFromPoint = Vector3.Distance(hit.position, this.transform.position);
        if (distanceFromPoint > distanceToRun)
        {
            agent.speed = runSpeed;
        }
        else
        {
            if (canSetSpeed == true)
            {
                agent.speed = walkSpeed;
            }
        }
    }
    private void Update()
    {
        _sc_debugCharAnimations.setMoving(agent.velocity.magnitude);
    }

    public void setClosestDestination(Transform _target)
    {
        if(_sc_cerveau.isInMenu == false)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(_target.position, out hit, 5.0f, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
            setSpeedFromNavHit(hit);
        }        
    }

    /// Debug ///
    public void SetUiDebugValues()
    {
        walkSpeedUi.GetComponent<TextMeshProUGUI>().text = walkSpeed.ToString("F1");
        runSpeedUi.GetComponent<TextMeshProUGUI>().text = runSpeed.ToString("F1");
        runDistanceUi.GetComponent<TextMeshProUGUI>().text = distanceToRun.ToString("F1");

        walkSpeedSliderUi.GetComponent<Slider>().value = walkSpeed;
        runSpeedSliderUi.GetComponent<Slider>().value = runSpeed;
        runDistanceSliderUi.GetComponent<Slider>().value = distanceToRun;
    }

    public void OnWalkValueChanged(float _newWalkSpeed)
    {
        walkSpeed = _newWalkSpeed;
    }
    public void OnRunValueChanged(float _newRunSpeed)
    {
        runSpeed = _newRunSpeed;
    }
    public void OnDistanceValueChanged(float _newRunDistance)
    {
        distanceToRun = _newRunDistance;
    }

    public void OnResetValues()
    {
        walkSpeed = defaultWalkSpeed;
        runSpeed = defaultRunSpeed;
        distanceToRun = defaultRunDistance;

        SetUiDebugValues();
    }
    /// End Debug ///
}