using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PnjInfos", order = 1)]
public class _So_pnjInfos : ScriptableObject
{
    public string pnjFirstName = null;
    public string pnjLastName = null;
    public enum PnjCategory
    {
        NV1,
        NV2,
        NV3
    }

    public PnjCategory pnjCategory;

    public enum PnjGroup
    {
        Famille1,
        Famille2,
        Famille3,
        Famille4,
        Famille5,
    }

    public PnjGroup pnjGroup;

}
