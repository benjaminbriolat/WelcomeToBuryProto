using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugSwitchModels : MonoBehaviour
{
    [SerializeField] GameObject model1 = null;
    [SerializeField] GameObject model2 = null;
    
    public void switchModels()
    {
        if(model1 != null && model2 != null)
        {
            if(model1.activeSelf)
            {
                model2.SetActive(true);
                model1.SetActive(false);
            }
            else
            {
                model2.SetActive(false);
                model1.SetActive(true);
            }
        }
    }
}
