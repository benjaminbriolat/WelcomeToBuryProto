using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _Sc_movement : MonoBehaviour
{
    public static _Sc_movement instance = null;
    Camera cam = null;
    NavMeshAgent agent = null;

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
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}