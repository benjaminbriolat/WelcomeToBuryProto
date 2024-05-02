using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_Debug_CamSwitcher : MonoBehaviour
{
    [SerializeField] GameObject camtoActivate = null;
    [SerializeField] GameObject camToDesacttivate = null;
    [SerializeField] _SO_VirtualCameraMovements virCamData = null;    
    _Sc_cameraMovement _cameraMovement = null;

    private void Start()
    {
        _cameraMovement = _Sc_cameraMovement.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(virCamData != null)
            {
                _cameraMovement.LoadVirtualCamData(false, virCamData, camtoActivate.transform);
            }
            camtoActivate.SetActive(true);
            camToDesacttivate.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cameraMovement.LoadVirtualCamData(true,null,null);
            camToDesacttivate.SetActive(true);
            camtoActivate.SetActive(false);
        }
    }
}
