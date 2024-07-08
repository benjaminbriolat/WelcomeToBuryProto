using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class _Sc_DebugBlackScreen : MonoBehaviour
{
    public static _Sc_DebugBlackScreen instance;
    private CanvasGroup _canvasGroup = null;
    private _Sc_Calendrier _sc_calendrier = null;
    _Sc_CraftManager _craftManager = null;

    public float fadeTime = 1.0f;
    public float spanWait = 0.35f;
    public float dayWait = 2.0f;

    [SerializeField] bool isDayFading = false;
    [SerializeField] bool isSpanFading = false;
    private void Awake()
    {
        instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        _sc_calendrier = _Sc_Calendrier.instance;
        _craftManager = _Sc_CraftManager.instance;
    }
    public void SetBlackScreen(bool _state, bool day)
    {
        if(_state == true)
        {            
            if(day == true)
            {
                isDayFading = true;
                _canvasGroup.DOFade(1.0f, fadeTime);
                StartCoroutine(BlackScreenFadeOut(day));
            }
            else
            {
                isSpanFading = true;
                _canvasGroup.DOFade(1.0f, fadeTime);
                StartCoroutine(BlackScreenFadeOut(day));
            }     
        }
        else
        {
            _canvasGroup.DOFade(0.0f, fadeTime);
            _sc_calendrier.SetLight();
            isDayFading = false;
            isSpanFading = false;
            _craftManager.Checkreceipes();
        }
    }

    private IEnumerator BlackScreenFadeOut(bool _day)
    {
        if(_day == true)
        {
            yield return new WaitForSeconds(fadeTime + dayWait);           
        }
        else
        {            
            yield return new WaitForSeconds(fadeTime + spanWait);            
        }

        _sc_calendrier.EndAdvanceCalendar(_day);
        SetBlackScreen(false, _day);
    }

    public float getFadingTime()
    {
        float totalTime = 0;
        if(isDayFading)
        {
            totalTime = (fadeTime*2) + dayWait + 0.02f;
        }
        else if (isSpanFading)
        {
            totalTime = (fadeTime * 2) + spanWait + 0.02f;
        }

        return totalTime;
    }
}
