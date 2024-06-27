using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class _Sc_smallTalkCanvas : MonoBehaviour
{
    public static _Sc_smallTalkCanvas instance = null;
    CanvasGroup _canvasGroup = null;
    TextMeshProUGUI _text = null;
    bool fadeOut = false;
    [SerializeField] float fadeOutSpeed = 1;
    float currentValue = 0;
    [SerializeField] float minDisplayTime = 1f;
    [SerializeField] float delayPerChar = 0.25f;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        _canvasGroup = transform.GetComponent<CanvasGroup>();
        _text = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void SetDisplay(Transform target, string newText,float delayStart)
    {
            
        StopAllCoroutines();
        fadeOut = false;
        currentValue = 0;
        _canvasGroup.alpha = 0;
        StartCoroutine(DelayDisplay(target,newText, delayStart));
        

    }

    public void SetBubble(Transform target, string newText, float delayStart)
    {
        
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        _text.text = newText;
        _canvasGroup.alpha = 1;
        VisualFeedback();
        float delayTime = (delayPerChar * newText.Length) + minDisplayTime;       
        StartCoroutine(fadeOutDelayed(delayTime));
        
    }


    private void VisualFeedback()
    {
        transform.DORewind();
        transform.DOKill();
        transform.DOPunchScale(new Vector3(-0.05f, 0.05f, 0.0f), 0.35f, 5, 1);
    }

    private void Update()
    {
        if(fadeOut)
        {
            currentValue -= fadeOutSpeed;
            _canvasGroup.alpha = currentValue;
            if(currentValue <= 0)
            {
                fadeOut = false;
            }
        }
    }

    private IEnumerator DelayDisplay(Transform target, string newText, float delayStart)
    {
        yield return new WaitForSeconds(delayStart);
        SetBubble(target, newText, delayStart);
    }

    private IEnumerator fadeOutDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentValue = 1;
        fadeOut = true;
    }
}
