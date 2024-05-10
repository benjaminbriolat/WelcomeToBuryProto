using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _Sc_DebugCamSliders : MonoBehaviour
{

    [SerializeField] CanvasGroup canasGroup = null;
    [SerializeField] Slider zoomSlider = null;
    [SerializeField] Slider inclSlider = null;
    [SerializeField] Slider zoomratioSlider = null;
    [SerializeField] Slider inclRatioSlider = null;
    bool isOpen = false;
    [SerializeField] TextMeshProUGUI zoomValue = null;
    [SerializeField] TextMeshProUGUI inclValue = null;
    [SerializeField] TextMeshProUGUI zoomRatioValue = null;
    [SerializeField] TextMeshProUGUI inclRatioValue = null;

    public void SetSliders(float zoom, float incl, float zoomR, float inclR)
    {
        zoomSlider.value = zoom;
        inclSlider.value = incl;
        zoomratioSlider.value = zoomR;
        inclRatioSlider.value = inclR;
    }

    public void open()
    {
        if(isOpen)
        {
            isOpen = false;
            canasGroup.alpha = 0;
            canasGroup.interactable = false;
            canasGroup.blocksRaycasts = false;
        }
        else
        {
            isOpen = true;
            canasGroup.alpha = 1;
            canasGroup.interactable = true;
            canasGroup.blocksRaycasts = true;
        }
    }

    private void Update()
    {
        zoomValue.text =  zoomSlider.value.ToString("F2");

        inclValue.text = inclSlider.value.ToString("F2");
        zoomRatioValue.text = zoomratioSlider.value.ToString("F2");
        inclRatioValue.text = inclRatioSlider.value.ToString("F2");
    }
}
