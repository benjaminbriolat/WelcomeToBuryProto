using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_debugUselessRessources : MonoBehaviour
{
    public static _Sc_debugUselessRessources instance = null;
    public bool desactivate = false;
    private void Awake()
    {
        instance = this;
    }
}
