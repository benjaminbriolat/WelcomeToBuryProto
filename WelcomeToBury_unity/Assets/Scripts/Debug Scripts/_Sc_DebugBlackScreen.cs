using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class _Sc_DebugBlackScreen : MonoBehaviour
{
    public static _Sc_DebugBlackScreen instance;
    private CanvasGroup _canvasGroup = null;

    private void Awake()
    {
        instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetBlackScreen(bool _state)
    {
        if(_state == true)
        {            
            _canvasGroup.DOFade(1.0f, 2.0f);
            StartCoroutine(BlackScreenFadeOut());
        }
        else
        {
            _canvasGroup.DOFade(0.0f, 2.0f);
        }
    }

    private IEnumerator BlackScreenFadeOut()
    {
        yield return new WaitForSeconds(2.5f);
        SetBlackScreen(false);
    }
}
