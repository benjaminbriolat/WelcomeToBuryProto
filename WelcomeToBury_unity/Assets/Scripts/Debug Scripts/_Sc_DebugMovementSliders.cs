using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _Sc_DebugMovementSliders : MonoBehaviour
{
    _Sc_movement _sc_movements = null;
    Slider mySlider = null;
    [SerializeField] TextMeshProUGUI myValueText = null;

    private void Start()
    {
        _sc_movements = _Sc_movement.instance;
        mySlider = GetComponent<Slider>();
    }

    public void setWalkValue()
    {
        _sc_movements.OnWalkValueChanged(mySlider.value);
        if(myValueText != null)
        {
            myValueText.text = mySlider.value.ToString("F1");
        }
    }
    public void setRunValue()
    {
        _sc_movements.OnRunValueChanged(mySlider.value);
        if(myValueText != null)
        {
            myValueText.text = mySlider.value.ToString("F1");
        }
    }
    public void setDistanceValue()
    {
        _sc_movements.OnDistanceValueChanged(mySlider.value);
        if(myValueText != null)
        {
            myValueText.text = mySlider.value.ToString("F1");
        }
    }

    public void resetValues()
    {
        _sc_movements.OnResetValues();
    }
}
