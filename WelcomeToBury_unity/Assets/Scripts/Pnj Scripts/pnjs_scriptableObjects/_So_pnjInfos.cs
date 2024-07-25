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
        Martin,
        Bonpain,
        Lereau,
        Collin,
        Fremont,
        Mathieu,
        Druide,
        Garde,
        Lavalois,
        Bovarois,
        Lechic,
        Laclarc,
        Klein,
        Lesimple
    }

    public PnjGroup pnjGroup;
}
