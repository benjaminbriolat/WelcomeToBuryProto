using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Sc_craftItem : MonoBehaviour
{
    public _So_item item = null;

    private void Awake()
    {
        transform.GetComponent<Image>().sprite = item.image;
    }
}
