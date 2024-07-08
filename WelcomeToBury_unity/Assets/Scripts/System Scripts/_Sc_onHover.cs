using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class _Sc_onHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    _Sc_mouseHoverManager _sc_mouseHoverManager;
    private void Start()
    {
        _sc_mouseHoverManager = _Sc_mouseHoverManager.instance;       
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _sc_mouseHoverManager.CurrentObject = this.transform;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _sc_mouseHoverManager.CurrentObject = null;
    }
}
