using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_mouseHoverManager : MonoBehaviour
{
    public static _Sc_mouseHoverManager instance;
    public Transform CurrentObject = null;

    private void Awake()
    {
        instance = this;
    }
}
