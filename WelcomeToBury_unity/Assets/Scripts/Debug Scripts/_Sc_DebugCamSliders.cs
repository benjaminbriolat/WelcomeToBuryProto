using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class _Sc_DebugCamSliders : MonoBehaviour
{

    [SerializeField] CanvasGroup canasGroup = null;
    [SerializeField] Slider zoomSlider = null;
    [SerializeField] Slider inclSlider = null;
    [SerializeField] Slider zoomratioSlider = null;
    [SerializeField] Slider inclRatioSlider = null;
    [SerializeField] Slider dampingSlider = null;
    [SerializeField] Transform button = null;
    public bool isOpen = false;
    [SerializeField] TextMeshProUGUI zoomValue = null;
    [SerializeField] TextMeshProUGUI inclValue = null;
    [SerializeField] TextMeshProUGUI zoomRatioValue = null;
    [SerializeField] TextMeshProUGUI inclRatioValue = null;
    [SerializeField] TextMeshProUGUI dampingValue = null;

    public void SetSliders(float zoom, float incl, float zoomR, float inclR,float damp)
    {
        zoomSlider.value = zoom;
        inclSlider.value = incl;
        zoomratioSlider.value = zoomR;
        inclRatioSlider.value = inclR;
        dampingSlider.value = damp;
    }

    public void open()
    {
        if(button != null)
        {
            button.DOKill();
            button.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.15f, 10, 1);
        }
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
        dampingValue.text = dampingSlider.value.ToString("F2");
    }
}
