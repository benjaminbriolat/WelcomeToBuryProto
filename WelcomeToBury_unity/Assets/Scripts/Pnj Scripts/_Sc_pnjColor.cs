using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class _Sc_pnjColor : MonoBehaviour
{
    Renderer myRenderer = null;
    [SerializeField] Color baseColor = Color.white;
    [SerializeField] Color cracherDeNoirColor = Color.black;
    [SerializeField] Color SymptomeColor = Color.red;
    List<Material> myMaterials = null; 

    private void Awake()
    {
        myRenderer = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Renderer>();
        myMaterials = myRenderer.materials.ToList();
    }

    public void SetBaseColor()
    {
        for (int i = 0; i < myMaterials.Count; i++)
        {
            myMaterials[i].color = baseColor;
        }
        //myRenderer.material.color = baseColor;
    }
    public void SetSymptomeColor()
    {
        for (int i = 0; i < myMaterials.Count; i++)
        {
            myMaterials[i].color = SymptomeColor;
        }
        //myRenderer.material.color = SymptomeColor;
    }
    public void SetCracheurColor()
    {
        for (int i = 0; i < myMaterials.Count; i++)
        {
            myMaterials[i].color = cracherDeNoirColor;
        }
        //myRenderer.material.color = cracherDeNoirColor;
    }
}
