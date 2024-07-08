using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_craftSlot : MonoBehaviour
{
    public _So_item _item = null;
    public int index = 0;

    _Sc_tooltipTrigger _sc_tolltipTrigger = null;

    private void Awake()
    {
        _sc_tolltipTrigger = transform.GetChild(0).GetComponent<_Sc_tooltipTrigger>();
    }

    public void SetTool()
    {
        if (_sc_tolltipTrigger != null)
        {
            if (_item != null)
            {
                _sc_tolltipTrigger.header = _item.itemName.ToString();
            }
        }
    }
}
