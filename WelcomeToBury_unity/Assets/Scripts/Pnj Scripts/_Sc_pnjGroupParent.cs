using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class _Sc_pnjGroupParent : MonoBehaviour
{
    public static _Sc_pnjGroupParent instance;
    public List<_Sc_pnjGroup> pnjGroupList;

    private void Awake()
    {
        instance = this;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<_Sc_pnjGroup>() != null)
            {
                pnjGroupList.Add(child.GetComponent<_Sc_pnjGroup>());
            }
        }
    }
}
