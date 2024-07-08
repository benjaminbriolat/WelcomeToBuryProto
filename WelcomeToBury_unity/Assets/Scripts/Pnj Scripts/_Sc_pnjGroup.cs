using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_pnjGroup : MonoBehaviour
{
    public string groupName;
    public List<Transform> pnjGroup;
    public int groupTrustLevel = 0;
    public int capTrustLevel = 3;
    public void UpdateTrustLevel()
    {
        groupTrustLevel += 1;
        for(int i = 0; i < pnjGroup.Count; i++)
        {
            if(groupTrustLevel >= capTrustLevel)
            {
                pnjGroup[i].GetComponent<_Sc_pnjState>().capTrustReached = true;
            }
            pnjGroup[i].GetComponent<_Sc_pnjState>().GroupLevel = groupTrustLevel;
        }
    }
}
