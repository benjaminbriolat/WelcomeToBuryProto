using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_Debug_CamSwitcher : MonoBehaviour
{
    [SerializeField] GameObject camtoActivate = null;
    [SerializeField] GameObject camToDesacttivate = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camtoActivate.SetActive(true);
            camToDesacttivate.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camToDesacttivate.SetActive(true);
            camtoActivate.SetActive(false);
        }
    }
}
