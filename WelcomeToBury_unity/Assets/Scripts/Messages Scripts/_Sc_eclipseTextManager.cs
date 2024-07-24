using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class _Sc_eclipseTextManager : MonoBehaviour
{
    public _So_eclipseTextTrack _textData = null;
    CanvasGroup _canvasGroup = null;
    CanvasGroup _textCanvas = null;
    _Sc_LoadTextLanguage _textLoader = null;
    _Sc_DebugBlackScreen _debugBlackScreen = null;
    [SerializeField] int currentLine = 0;
    Button nextButton = null;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _textCanvas = transform.GetChild(1).GetComponent<CanvasGroup>();
        _textLoader = transform.GetChild(1).GetComponent<_Sc_LoadTextLanguage>();
        _debugBlackScreen = transform.parent.parent.GetComponent<_Sc_DebugBlackScreen>();
        nextButton = transform.GetChild(2).transform.GetChild(1).GetComponent<Button>();
        nextButton.interactable = false;
    }
    

    public void SendLine(bool fade)
    {        
        if (currentLine <= _textData.eclipseLines.Count - 1)
        {
            if(fade)
            {
                StartCoroutine(FadeText());
            }
            else
            {
                _textCanvas.alpha = 1;
                _textLoader.setText("", _textData.eclipseLines[currentLine].lineText, "");
                nextButton.interactable = true;
                currentLine++;
            }            
           
        }
        else
        {
            nextButton.interactable = false;
            StartCoroutine(FadeBox());
        }
    }

    private IEnumerator FadeText()
    {
        nextButton.interactable = false;
        _textCanvas.DOFade(0.0f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        _textLoader.setText("", _textData.eclipseLines[currentLine].lineText, "");
        _textCanvas.DOFade(1.0f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        currentLine++;
        nextButton.interactable = true;
    }

    private IEnumerator FadeBox()
    {
        _canvasGroup.DOFade(0.0f, 1);
        yield return new WaitForSeconds(1);
        _debugBlackScreen.SetBlackScreen(false, true, true);
    }

}
