using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class _Sc_tooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float Delay = 0.5f;
    public string header;
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        _Sc_tooltipSystem.Hide();
    }

    private void OnDisable()
    {
        if(_Sc_tooltipSystem.lastTooltipTrigger == this)
        {
            _Sc_tooltipSystem.Hide();
        }
    }
    private void OnDestroy()
    {
        if (_Sc_tooltipSystem.lastTooltipTrigger == this)
        {
            _Sc_tooltipSystem.Hide();
        }
    }

    private IEnumerator ShowDelay()
    {
        yield return new WaitForSeconds(Delay);
        _Sc_tooltipSystem.Show(header, content);
        _Sc_tooltipSystem.lastTooltipTrigger = this;
    }
}
