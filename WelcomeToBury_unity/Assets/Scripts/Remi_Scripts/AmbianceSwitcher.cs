using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.Rendering.HighDefinition;

public class AmbianceSwitcher : MonoBehaviour
{
    [SerializeField]
    private Transform lights, models;//fogs, 

    private Material[] materials;

    private float[][] light_temps, light_intens;//, fog_dists;


    // Start is called before the first frame update
    void Start()
    {

        //MATERIALS TRANSPARENCY (dirt)
        materials = new Material[models.childCount];

        for (int i = 0; i < materials.Length; i++) { 
            materials[i] = models.GetChild(i).GetComponent<MeshRenderer>().sharedMaterials[1];
            materials[i].SetFloat("_AlphaRemapMax", 0f);
            //Debug.Log(materials[i].GetFloat("_AlphaRemapMax"));
        }

        //LIGHTS temperature (color) & intensity
        light_temps = new float[2][];
        light_intens = new float[2][];

        for (int i = 0; i < 2; i++)
        {
            light_temps[i] = new float[lights.GetChild(i).childCount];
            light_intens[i] = new float[lights.GetChild(i).childCount];

            for (int j = 0; j < light_temps[i].Length; j++) {
                light_temps[i][j] = lights.GetChild(i).GetChild(j).GetComponent<Light>().colorTemperature;
                light_intens[i][j] = lights.GetChild(i).GetChild(j).GetComponent<HDAdditionalLightData>().intensity;
            }

        }

        //

        lights.GetChild(1).gameObject.SetActive(false);

        //

        //Invoke("SwitchTextures", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchAmbiance(int num)
    {
        for (int i = 0; i < materials.Length; i++)
            materials[i].DOFloat((float)num, "_AlphaRemapMax", 2f);

        for (int i = 0; i < light_temps[0].Length; i++) {
            Light light = lights.GetChild(0).GetChild(i).GetComponent<Light>();
            HDAdditionalLightData hdlight = lights.GetChild(0).GetChild(i).GetComponent<HDAdditionalLightData>();
            //light.DOIntensity(light_intens[1][i], 2f);
            //hdlight.intensity = light_intens[1][i];
            //light.colorTemperature = light_temps[1][i];
            DOTween.To(() => hdlight.intensity, x => hdlight.intensity = x, light_intens[num][i], 4f);
            //lights.GetChild(0).GetChild(i).GetComponent<Light>().DOIntensity(light_intens[1][i], 2f);
            DOTween.To(() => light.colorTemperature, x => light.colorTemperature = x, light_temps[num][i], 4f);
            //lights.GetChild(i).GetComponent<Light>().DO
        }
    }
}
