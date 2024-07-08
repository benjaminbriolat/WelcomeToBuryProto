using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugSymptomeManager : MonoBehaviour
{
    public static _Sc_DebugSymptomeManager Instance;

    public bool Symptome1Discovered = false;
    public bool Symptome2Discovered = false;
    public bool Symptome3Discovered = false;
    public bool Symptome4Discovered = false;

    public string symptome1Name = "1";
    public string symptome2Name = "2";
    public string symptome3Name = "3";
    public string symptome4Name = "4";

    private void Awake()
    {
        Instance = this;
    }
}
