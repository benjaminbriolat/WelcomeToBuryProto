using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;

public class _Sc_debugPnjButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    Image ButtonImage = null;
    [SerializeField] Color BaseColor = Color.white;
    [SerializeField] Color HighlightColor;
    [SerializeField] Color UnusableColor;

    WaitForSeconds unselectWFS = new WaitForSeconds(0.01f);

    public bool isUsable = true;
    private Button myButton = null;
    [SerializeField] bool isDialogueButton = false;
    private void Awake()
    {
        ButtonImage = GetComponent<Image>();
        ButtonImage.color = BaseColor;
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        if(isDialogueButton == true)
        {
            SetUsable(false);
        }
    }

    public void SetUsable(bool _usable)
    {
        Color tempColor = ButtonImage.color;

        if (isDialogueButton == false)
        {
            if (_usable == false)
            {
                tempColor = UnusableColor;
                tempColor.a = 0.0f;
                myButton.interactable = false;
                isUsable = false;

                ButtonImage.transform.parent.GetComponent<CanvasGroup>().alpha = 0.0f;
                ButtonImage.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                tempColor = BaseColor;
                tempColor.a = 1.0f;
                myButton.interactable = true;
                isUsable = true;

                ButtonImage.transform.parent.GetComponent<CanvasGroup>().alpha = 1.0f;
                ButtonImage.transform.parent.gameObject.SetActive(true);
            }

            ButtonImage.color = tempColor;
        }
        else
        {
            //Temp disable dialogue button
            tempColor = UnusableColor;
            tempColor.a = 0.0f;
            myButton.interactable = false;
            isUsable = false;

            ButtonImage.transform.parent.GetComponent<CanvasGroup>().alpha = 0.0f;
            ButtonImage.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isUsable)
        {
            ButtonImage.color = HighlightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isUsable)
        {
            ButtonImage.color = BaseColor;
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        ButtonImage.transform.DORewind();
        ButtonImage.transform.DOKill();

        ButtonImage.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 1), 0.25f, 10, 1);

        StartCoroutine(AutoUnselectDelay());
    }

    private IEnumerator AutoUnselectDelay()
    {
        yield return unselectWFS;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void setLockedColor()
    {
        ButtonImage.color = BaseColor;
    }
}