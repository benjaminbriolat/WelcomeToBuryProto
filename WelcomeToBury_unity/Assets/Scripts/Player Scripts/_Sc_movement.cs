using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class _Sc_movement : MonoBehaviour
{
    public static _Sc_movement instance = null;
    Camera cam = null;
    NavMeshAgent agent = null;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float runSpeed = 12.0f;

    [SerializeField] float distanceFromPoint = 0.0f;
    [SerializeField] float distanceToRun = 15.0f;
    [SerializeField] string layerName = "Walkable";

    public bool canSetSpeed = true;

    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        cam = Camera.main;
    }

    public void getMouseLeftClick(Vector2 _mousePos)
    {
        Debug.Log("LeftCLickReceived");
        Ray ray = cam.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity,LayerMask.GetMask(layerName)))
        {
            agent.SetDestination(hit.point);
        }

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
}