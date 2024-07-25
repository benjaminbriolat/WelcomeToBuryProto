using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_pnjGroup : MonoBehaviour
{
    public string groupName;
    public List<Transform> pnjGroup;
    public int groupTrustLevel = 0;
    public int capTrustLevel = 3;

    public bool hasSoucier = false;
    public bool hasHeal = false;
    public void UpdateTrustLevel()
    {
        if(hasSoucier == true && hasHeal == true)
        {
            groupTrustLevel = 1;

            for (int i = 0; i < pnjGroup.Count; i++)
            {
                if (groupTrustLevel >= capTrustLevel)
                {
                    pnjGroup[i].GetComponent<_Sc_pnjState>().capTrustReached = true;
                }
                pnjGroup[i].GetComponent<_Sc_pnjState>().GroupLevel = groupTrustLevel;
                pnjGroup[i].GetComponent<_Sc_pnjState>().UpdateTrustLevel();
            }
        }
        else
        {
            groupTrustLevel = 0;
        }

        /*if(groupTrustLevel == 1)
        {
            for (int i = 0; i < pnjGroup.Count; i++)
            {
                if (groupTrustLevel >= capTrustLevel)
                {
                    pnjGroup[i].GetComponent<_Sc_pnjState>().capTrustReached = true;
                }
                pnjGroup[i].GetComponent<_Sc_pnjState>().GroupLevel = groupTrustLevel;
                pnjGroup[i].GetComponent<_Sc_pnjState>().UpdateTrustLevel();
            }
        }*/
    }

    public void UpdateSoucier()
    {
        hasSoucier = true;
        UpdateTrustLevel();
    }
    public void UpdateHeal()
    {
        hasHeal = true;
        UpdateTrustLevel();
    }
}
