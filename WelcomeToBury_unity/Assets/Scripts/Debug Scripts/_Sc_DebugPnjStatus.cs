using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugPnjStatus : MonoBehaviour
{
    public bool isSick = false;
    [SerializeField] Material sickMat = null;
    [SerializeField] Material sickMat1 = null;
    [SerializeField] Material sickMat2 = null;
    [SerializeField] Material sickMat3 = null;
    [SerializeField] Material healthyMat = null;
    MeshRenderer render = null;
    int currentSymptomProgress = 0;
    [SerializeField] int capToNewSymptom = 3;
    private void Awake()
    {
        render = transform.GetComponent<MeshRenderer>();
        transform.parent.GetComponent<_Sc_EpidemicManager>().AddPnj(this.transform);
    }

    public void setStatus(bool setSick, int symptom)
    {
        isSick = setSick;
        if(isSick)
        {
            if(symptom == 0)
            {
                render.material = sickMat;
            }
            if (symptom == 1)
            {
                render.material = sickMat1;
            }
            if (symptom == 2)
            {
                render.material = sickMat2;
            }
            if (symptom == 3)
            {
                render.material = sickMat3;
            }
        }
        else
        {
            render.material = healthyMat;
        }
    }
    /*public void progressDisease()
    {
        if(isSick == true)
        {
            currentSymptomProgress += 1;
            if(currentSymptomProgress >= capToNewSymptom)
            {

            }
        }
    }*/
}
