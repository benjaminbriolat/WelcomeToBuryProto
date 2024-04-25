using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_Debug_CamTrigger : MonoBehaviour
{
    _Sc_cameraMovement _sc_CameraMovement = null;
    [SerializeField] float camAngle = 0;
    [SerializeField] float camSpeed= 0.01f;
    private void Start()
    {
        _sc_CameraMovement = _Sc_cameraMovement.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("collPlayer");
            _sc_CameraMovement.CallAnimCam(true, camAngle, camSpeed);
        }
    }

}
