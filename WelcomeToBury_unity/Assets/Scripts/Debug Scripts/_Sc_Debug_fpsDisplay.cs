using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Sc_Debug_fpsDisplay : MonoBehaviour
{
    [SerializeField] float framerate = 0;
    float timerUpdate = 0.2f;
    [SerializeField] bool activateDebug = true;

    TextMeshProUGUI textDisplay = null;

    private void Awake()
    {
        textDisplay = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textDisplay.gameObject.SetActive(activateDebug);  
    }
    public void UpdateFPSDisplay()
    {
        timerUpdate -= Time.deltaTime;
        if (timerUpdate <= 0f)
        {
            framerate = 1f / Time.unscaledDeltaTime;
            textDisplay.text = "FPS: " + Mathf.Round(framerate);
            timerUpdate = 0.2f;
        }
    }

    private void Update()
    {
        textDisplay.gameObject.SetActive(activateDebug);
        if (activateDebug == true)
        {
            UpdateFPSDisplay();
        }
    }
}
