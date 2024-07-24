using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class _Sc_DebugBlackScreen : MonoBehaviour
{
    public static _Sc_DebugBlackScreen instance;
    private CanvasGroup _canvasGroup = null;
    private CanvasGroup _eclipseCanvasGroup = null;
    private _Sc_Calendrier _sc_calendrier = null;
    _Sc_CraftManager _craftManager = null;

    public float fadeTime = 1.0f;
    public float fadeOutTimeEclipse = 2.0f;
    public float spanWait = 0.35f;
    public float dayWait = 2.0f;
    public float eclipseWait = 2.0f;

    [SerializeField] bool isDayFading = false;
    [SerializeField] bool isSpanFading = false;
    [SerializeField] bool isEclispeFading = false;

    [SerializeField] _Sc_eclipseTextManager _eclipseTextManager = null;
    private void Awake()
    {
        instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
        _eclipseCanvasGroup = transform.GetChild(0).GetChild(1).GetComponent<CanvasGroup>();
        _eclipseTextManager = transform.GetChild(0).GetChild(1).GetComponent<_Sc_eclipseTextManager>();
    }
    private void Start()
    {
        _sc_calendrier = _Sc_Calendrier.instance;
        _craftManager = _Sc_CraftManager.instance;
    }
    public void SetBlackScreen(bool _state, bool day,bool eclipse = false, _So_eclipseTextTrack newData = null)
    {
        if(_state == true)
        {       
            if(eclipse == false)
            {
                if (day == true)
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
                _eclipseTextManager._textData = newData;
                isEclispeFading = true;
                _canvasGroup.DOFade(1.0f, fadeTime);
                StartCoroutine(BlackScreenFadeOut(day,true));
                StartCoroutine(EclipseTextCorou());
            }
        }
        else
        {
            if(eclipse)
            {
                _eclipseCanvasGroup.interactable = false;
                _eclipseCanvasGroup.blocksRaycasts = false;
                _canvasGroup.DOFade(0.0f, fadeOutTimeEclipse);
                _eclipseCanvasGroup.DOFade(0.0f, fadeOutTimeEclipse);
            }
            else
            {
                _canvasGroup.DOFade(0.0f, fadeTime);
            }           
            _sc_calendrier.SetLight();
            isDayFading = false;
            isSpanFading = false;
            isEclispeFading = false;
            _craftManager.Checkreceipes();
        }
    }

    private IEnumerator BlackScreenFadeOut(bool _day,bool _eclipse = false)
    {

        if (_eclipse == false)
        {
            if (_day == true)
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

    private IEnumerator EclipseTextCorou()
    {
        _eclipseTextManager.SendLine(false);
        yield return new WaitForSeconds(fadeTime);
        _eclipseCanvasGroup.DOFade(1.0f, fadeTime);
        _eclipseCanvasGroup.interactable = true;
        _eclipseCanvasGroup.blocksRaycasts = true;
        _sc_calendrier.EndAdvanceCalendar(true);
    }
}
