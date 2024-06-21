using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_tooltipSystem : MonoBehaviour
{
    public static _Sc_tooltipSystem instance;
    public _Sc_tooltip _sc_tooltip;
    public static _Sc_tooltipTrigger lastTooltipTrigger = null;
    private void Awake()
    {
        instance = this;
        _sc_tooltip = GetComponent<_Sc_tooltip>();
        Hide();
    }

    public static void Show(string header, string content = "")
    {
        instance._sc_tooltip.SetText(header, content);
        instance._sc_tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance._sc_tooltip.gameObject.SetActive(false);
    }
}
