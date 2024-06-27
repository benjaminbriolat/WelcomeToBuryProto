using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class _Sc_DebugBlackScreen : MonoBehaviour
{
    public static _Sc_DebugBlackScreen instance;
    private CanvasGroup _canvasGroup = null;
    private _Sc_Calendrier _sc_calendrier = null;

    private float fadeTime = 2.0f;
    private float spanWait = 0.5f;
    private float dayWait = 2.0f;
    private void Awake()
    {
        instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        _sc_calendrier = _Sc_Calendrier.instance;
    }
    public void SetBlackScreen(bool _state, bool day)
    {
        if(_state == true)
        {            
            if(day == true)
            {
                _canvasGroup.DOFade(1.0f, fadeTime);
                StartCoroutine(BlackScreenFadeOut(day));
            }
            else
            {
                _canvasGroup.DOFade(1.0f, fadeTime);
                StartCoroutine(BlackScreenFadeOut(day));
            }     
        }
        else
        {
            _canvasGroup.DOFade(0.0f, fadeTime);
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
}
