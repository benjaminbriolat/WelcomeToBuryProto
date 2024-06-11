using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Sc_fichePatientUI : MonoBehaviour
{
    public static _Sc_fichePatientUI Instance;

    Transform UIparent = null;

    TextMeshProUGUI nameText = null;
    TextMeshProUGUI groupText = null;
    TextMeshProUGUI currentEtatText = null;

    Transform symptomeHorGroup = null;
    Transform symptome1 = null;
    Transform symptome2 = null;
    Transform symptome3 = null;
    Transform symptome4 = null;

    private void Awake()
    {
        Instance = this;

        UIparent = transform.GetChild(0).GetChild(0);

        nameText = UIparent.GetChild(0).GetComponent<TextMeshProUGUI>();
        groupText = UIparent.GetChild(1).GetComponent<TextMeshProUGUI>();
        currentEtatText = UIparent.GetChild(3).GetComponent<TextMeshProUGUI>();

        symptomeHorGroup = UIparent.GetChild(5);

        symptome1 = symptomeHorGroup.GetChild(0);
        symptome2 = symptomeHorGroup.GetChild (1);
        symptome3 = symptomeHorGroup.GetChild(2);
        symptome4 = symptomeHorGroup.GetChild(3);
    }

    public void SetFicheValues(_So_pnjInfos _so_pnjInfos, int _currentEtat, int _groupLevel, bool _symptome1, bool _symptome2, bool _symptome3, bool _symptome4)
    {
        nameText.text = _so_pnjInfos.pnjFirstName + " " + _so_pnjInfos.pnjLastName;
        groupText.text = _so_pnjInfos.pnjGroup.ToString() + " - " + "Amitié" + " " + _groupLevel.ToString();

        if(_currentEtat == 0)
        {
            currentEtatText.text = "En bonne santé";
        }
        else if(_currentEtat == 1)
        {
            currentEtatText.text = "Malade";
        }
        else if (_currentEtat == 2)
        {
            currentEtatText.text = "Cracheur de noir";
        }

        if(_symptome1)
        {
            symptome1.gameObject.SetActive(true);
        }
        else
        {
            symptome1.gameObject.SetActive(false);
        }

        if (_symptome2)
        {
            symptome2.gameObject.SetActive(true);
        }
        else
        {
            symptome2.gameObject.SetActive(false);
        }

        if (_symptome3)
        {
            symptome3.gameObject.SetActive(true);
        }
        else
        {
            symptome3.gameObject.SetActive(false);
        }

        if (_symptome4)
        {
            symptome4.gameObject.SetActive(true);
        }
        else
        {
            symptome4.gameObject.SetActive(false);
        }
    }
}
