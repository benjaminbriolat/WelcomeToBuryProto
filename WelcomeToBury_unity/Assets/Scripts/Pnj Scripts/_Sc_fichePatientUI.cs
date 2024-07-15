using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class _Sc_fichePatientUI : MonoBehaviour
{
    public static _Sc_fichePatientUI Instance;

    Transform UIparent = null;

    TextMeshProUGUI nameText = null;
    TextMeshProUGUI groupText = null;
    TextMeshProUGUI currentEtatText = null;

    [SerializeField] Transform symptomeTitle = null;
    [SerializeField] Transform symptomeHorGroup = null;
    [SerializeField] Transform symptome1 = null;
    [SerializeField] Transform symptome2 = null;
    [SerializeField] Transform symptome3 = null;
    [SerializeField] Transform symptome4 = null;

    [SerializeField] Image picto1 = null;
    [SerializeField] Image picto2 = null;
    [SerializeField] Image picto3 = null;
    [SerializeField] Image picto4 = null;
    public bool displayPictos = true;


    _Sc_DebugSymptomeManager _sc_debugSymptomeManager = null;
    _Sc_cookbook _sc_cookBook = null;
    
    CanvasGroup myCanvasGroup = null;
    _Sc_selectPnj _sc_SelectPnj = null;
    [SerializeField] Sprite unknownRemedy = null;
    private void Awake()
    {
        Instance = this;

        UIparent = transform.GetChild(0).GetChild(0);

        nameText = UIparent.GetChild(1).GetComponent<TextMeshProUGUI>();
        groupText = UIparent.GetChild(0).GetComponent<TextMeshProUGUI>();
        currentEtatText = UIparent.GetChild(3).GetComponent<TextMeshProUGUI>();

        //symptomeHorGroup = UIparent.GetChild(5);
        symptomeTitle = UIparent.GetChild(4);
        symptomeHorGroup = symptomeTitle.GetChild(0);

        symptome1 = symptomeHorGroup.GetChild(0);
        symptome2 = symptomeHorGroup.GetChild (1);
        symptome3 = symptomeHorGroup.GetChild(2);
        symptome4 = symptomeHorGroup.GetChild(3);

        picto1 = symptome1.transform.GetChild(1).GetComponent<Image>();
        picto2 = symptome2.transform.GetChild(1).GetComponent<Image>();
        picto3 = symptome3.transform.GetChild(1).GetComponent<Image>();
        picto4 = symptome4.transform.GetChild(1).GetComponent<Image>();

        /*symptome1.GetComponent<TextMeshProUGUI>().text = "?";
        symptome2.GetComponent<TextMeshProUGUI>().text = "?";
        symptome3.GetComponent<TextMeshProUGUI>().text = "?";
        symptome4.GetComponent<TextMeshProUGUI>().text = "?";*/

        myCanvasGroup = GetComponent<CanvasGroup>();
        _sc_SelectPnj = _Sc_selectPnj.Instance;
        setCanvas(false);
    }
    

    public void setCanvasFromUI()
    {
        setCanvas(false, true);
    }
    public void setCanvas(bool _state, bool fromUI = false)
    {
        if (_state == true)
        {
            myCanvasGroup.alpha = 1f;
        }
        else
        {
            myCanvasGroup.alpha = 0f;
            if (fromUI == true)
            {
                if (_sc_SelectPnj != null)
                {
                    _sc_SelectPnj.UnSelectPnj(fromUI);
                }
            }
        }
    }

    private void Start()
    {
        _sc_debugSymptomeManager = _Sc_DebugSymptomeManager.Instance;
        _sc_cookBook = _Sc_cookbook.instance;

        picto1.sprite = _sc_cookBook.getRemede("treatment1").image;
        picto2.sprite = _sc_cookBook.getRemede("treatment2").image;
        picto3.sprite = _sc_cookBook.getRemede("treatment3").image;
        picto4.sprite = _sc_cookBook.getRemede("treatment4").image;

        //SetPictos();

        symptome1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(0);
        symptome2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(1);
        symptome3.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(2);
        symptome4.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(3);
    }

    public void SetFicheValues(_Sc_pnjState _sc_pnjState, _So_pnjInfos _so_pnjInfos, int _currentEtat, int _groupLevel, bool _symptome1, bool _symptome2, bool _symptome3, bool _symptome4)
    {
        if(_sc_pnjState.DialogueOk == false)
        {
            SetFicheInfosBase(_sc_pnjState, _so_pnjInfos, _currentEtat, _groupLevel);
        }
        else
        {
            SetFicheInfosDialogue(_sc_pnjState, _so_pnjInfos, _currentEtat, _groupLevel, _symptome1, _symptome2, _symptome3, _symptome4);
        }

        if(_sc_pnjState.SoucierOk == true)
        {
            SetFicheInfosSoucier(_sc_pnjState, _so_pnjInfos, _currentEtat, _groupLevel, _symptome1, _symptome2, _symptome3, _symptome4);
        }

        setCanvas(true);
    }

    public void SetPictos()
    {
        /*picto1.enabled = displayPictos;
        picto2.enabled = displayPictos;
        picto3.enabled = displayPictos;
        picto4.enabled = displayPictos;*/
    }

    // NEVER CALLED
    private void SetFicheInfosFull(_Sc_pnjState _sc_pnjState, _So_pnjInfos _so_pnjInfos, int _currentEtat, int _groupLevel, bool _symptome1, bool _symptome2, bool _symptome3, bool _symptome4)
    {
        nameText.text = _so_pnjInfos.pnjFirstName + " " + _so_pnjInfos.pnjLastName;
        groupText.text = _so_pnjInfos.pnjGroup.ToString() + " - " + "Amitié" + " " + _groupLevel.ToString();

        if (_currentEtat == 0)
        {
            currentEtatText.text = "En bonne santé";
        }
        else if (_currentEtat == 1)
        {
            currentEtatText.text = "Malade";
        }
        else if (_currentEtat == 2)
        {
            currentEtatText.text = "Cracheur de noir";
        }

        if (_symptome1)
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
   
    private void SetFicheInfosBase(_Sc_pnjState _sc_pnjState, _So_pnjInfos _so_pnjInfos, int _currentEtat, int _groupLevel)
    {
        nameText.text = "???";
        groupText.text = "???";

        currentEtatText.text = "???";

        if(_currentEtat == 0)
        {
            symptome1.gameObject.SetActive(false);
            symptome2.gameObject.SetActive(false);
            symptome3.gameObject.SetActive(false);
            symptome4.gameObject.SetActive(false);
        }
    }
    
    private void SetFicheInfosDialogue(_Sc_pnjState _sc_pnjState, _So_pnjInfos _so_pnjInfos, int _currentEtat, int _groupLevel, bool _symptome1, bool _symptome2, bool _symptome3, bool _symptome4)
    {
        nameText.text = _so_pnjInfos.pnjFirstName + " " + _so_pnjInfos.pnjLastName;

        if (_sc_pnjState.capTrustReached == true)
        {
            groupText.text = "Les" + " " + _so_pnjInfos.pnjGroup.ToString() + " " + "vous font confiance";
            groupText.gameObject.SetActive(true);

        }
        else
        {
            //groupText.text = "Les" + " " + _so_pnjInfos.pnjGroup.ToString() + " ne vous font pas encore confiance";
            groupText.gameObject.SetActive(false);
        }
        //groupText.text = _so_pnjInfos.pnjGroup.ToString() + " - " + "Amitié" + " " + _groupLevel.ToString();

        if (_currentEtat == 0)
        {
            currentEtatText.text = "En bonne santé";
            symptomeHorGroup.gameObject.SetActive(false);
            symptomeTitle.gameObject.SetActive(false);
        }
        else if (_currentEtat == 1)
        {
            currentEtatText.text = "Malade";
            symptomeHorGroup.gameObject.SetActive(false);
            symptomeTitle.gameObject.SetActive(false);
            /*if(_sc_pnjState.receivedSeSoucier == true)
            {
                symptomeHorGroup.gameObject.SetActive(true);
                symptomeTitle.gameObject.SetActive(true);                
            }
            else
            {
                symptomeHorGroup.gameObject.SetActive(false);
                symptomeTitle.gameObject.SetActive(false);
            }*/
            /// SYMPTOM 1
            if (_symptome1 /*&& _sc_pnjState.receivedSeSoucier == true*/)
            {
                if (_sc_cookBook.receipes[0].metSymptom == true)
                {
                    symptomeHorGroup.gameObject.SetActive(true);
                    symptomeTitle.gameObject.SetActive(true);
                    Debug.Log("S1 dialogue FP active");
                    symptome1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(0);
                    symptome1.gameObject.SetActive(true);
                    if (_sc_cookBook.getDiscoveredSymptom(0))
                    {
                        picto1.transform.gameObject.SetActive(true);
                        picto1.sprite = _sc_cookBook.getRemede("treatment1").image;
                    }
                    else
                    {
                        picto1.transform.gameObject.SetActive(false);
                        picto1.sprite = unknownRemedy;
                    }
                }
                else
                {
                    symptome1.gameObject.SetActive(false);
                }
            }
            else
            {
                symptome1.gameObject.SetActive(false);
            }

            /// SYMPTOM 2
            if (_symptome2 /*&& _sc_pnjState.receivedSeSoucier == true*/)
            {
                if (_sc_cookBook.receipes[1].metSymptom == true)
                {
                    symptomeHorGroup.gameObject.SetActive(true);
                    symptomeTitle.gameObject.SetActive(true);
                    Debug.Log("S2 dialogue FP active");
                    symptome2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(1);
                    symptome2.gameObject.SetActive(true);
                    if (_sc_cookBook.getDiscoveredSymptom(1))
                    {
                        picto2.transform.gameObject.SetActive(true);
                        picto2.sprite = _sc_cookBook.getRemede("treatment2").image;
                    }
                    else
                    {
                        picto2.transform.gameObject.SetActive(false); 
                        picto2.sprite = unknownRemedy;
                    }
                }
                else
                {
                    symptome2.gameObject.SetActive(false);
                }
            }
            else
            {
                symptome2.gameObject.SetActive(false);
            }

            /// SYMPTOM 3
            if (_symptome3 /*&& _sc_pnjState.receivedSeSoucier == true*/)
            {
                if (_sc_cookBook.receipes[2].metSymptom == true)
                {
                    symptomeHorGroup.gameObject.SetActive(true);
                    symptomeTitle.gameObject.SetActive(true);
                    Debug.Log("S3 dialogue FP active");
                    symptome3.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(2);
                    symptome3.gameObject.SetActive(true);
                    if (_sc_cookBook.getDiscoveredSymptom(2))
                    {
                        picto3.transform.gameObject.SetActive(true);
                        picto3.sprite = _sc_cookBook.getRemede("treatment3").image;
                    }
                    else
                    {
                        picto3.transform.gameObject.SetActive(false);
                        picto3.sprite = unknownRemedy;
                    }
                }
                else
                {
                    symptome3.gameObject.SetActive(false);
                }
            }
            else
            {
                symptome3.gameObject.SetActive(false);
            }

            /// SYMPTOM 4
            if (_symptome4 /*&& _sc_pnjState.receivedSeSoucier == true*/)
            {
                if (_sc_cookBook.receipes[3].metSymptom == true)
                {
                    symptomeHorGroup.gameObject.SetActive(true);
                    symptomeTitle.gameObject.SetActive(true);
                    Debug.Log("S4 dialogue FP active");
                    symptome4.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(3);
                    symptome4.gameObject.SetActive(true);
                    if (_sc_cookBook.getDiscoveredSymptom(3))
                    {
                        picto4.transform.gameObject.SetActive(true);
                        picto4.sprite = _sc_cookBook.getRemede("treatment4").image;
                    }
                    else
                    {
                        picto4.transform.gameObject.SetActive(false);
                        picto4.sprite = unknownRemedy;
                    }
                }
                else
                {
                    symptome4.gameObject.SetActive(false);
                }
            }
            else
            {
                symptome4.gameObject.SetActive(false);
            }

        }
        else if (_currentEtat == 2)
        {
            currentEtatText.text = "Cracheur de noir";
        }
    }
    
    private void SetFicheInfosSoucier(_Sc_pnjState _sc_pnjState, _So_pnjInfos _so_pnjInfos, int _currentEtat, int _groupLevel, bool _symptome1, bool _symptome2, bool _symptome3, bool _symptome4)
    {
        nameText.text = _so_pnjInfos.pnjFirstName + " " + _so_pnjInfos.pnjLastName;

        if(_sc_pnjState.capTrustReached == true)
        {
            groupText.text = "Les" + " " +  _so_pnjInfos.pnjGroup.ToString()  + " " + "vous font confiance";
            groupText.gameObject.SetActive(true);

        }
        else
        {
            groupText.gameObject.SetActive(false);
        }

        //groupText.text = _so_pnjInfos.pnjGroup.ToString() + " - " + "Amitié" + " " + _groupLevel.ToString();

        if (_currentEtat == 0)
        {
            currentEtatText.text = "En bonne santé";
            symptomeHorGroup.gameObject.SetActive(false);
            symptomeTitle.gameObject.SetActive(false);
        }
        else if (_currentEtat == 1)
        {
            currentEtatText.text = "Malade";
            symptomeHorGroup.gameObject.SetActive(true);
            symptomeTitle.gameObject.SetActive(true);

            /// SYMPTOM 1
            if (_symptome1 && _sc_cookBook.receipes[0].metSymptom == true)
            {                
                symptomeHorGroup.gameObject.SetActive(true);
                symptomeTitle.gameObject.SetActive(true);
                Debug.Log("S1 care FP active");
                symptome1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(0);
                symptome1.gameObject.SetActive(true);
                if (_sc_cookBook.getDiscoveredSymptom(0))
                {
                    picto1.transform.gameObject.SetActive(true);
                    picto1.sprite = _sc_cookBook.getRemede("treatment1").image;
                }
                else
                {
                    picto1.transform.gameObject.SetActive(false);
                    picto1.sprite = unknownRemedy;
                }                
            }
            else
            {
                symptome1.gameObject.SetActive(false);
            }

            /// SYMPTOM 2
            if (_symptome2 && _sc_cookBook.receipes[1].metSymptom == true)
            {
                
                symptomeHorGroup.gameObject.SetActive(true);
                symptomeTitle.gameObject.SetActive(true);
                Debug.Log("S2 care FP active");
                symptome2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(1);
                symptome2.gameObject.SetActive(true);
                if (_sc_cookBook.getDiscoveredSymptom(1))
                {
                    picto2.transform.gameObject.SetActive(true);
                    picto2.sprite = _sc_cookBook.getRemede("treatment2").image;
                }
                else
                {
                    picto2.transform.gameObject.SetActive(false);
                    picto2.sprite = unknownRemedy;
                }
                
            }
            else
            {
                symptome2.gameObject.SetActive(false);
            }

            /// SYMPTOM 3
            if (_symptome3 && _sc_cookBook.receipes[2].metSymptom == true)
            {
                
                symptomeHorGroup.gameObject.SetActive(true);
                symptomeTitle.gameObject.SetActive(true);
                Debug.Log("S3 care FP active");
                symptome3.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(2);
                symptome3.gameObject.SetActive(true);
                if (_sc_cookBook.getDiscoveredSymptom(2))
                {
                    picto3.transform.gameObject.SetActive(true);
                    picto3.sprite = _sc_cookBook.getRemede("treatment3").image;
                }
                else
                {
                    picto3.transform.gameObject.SetActive(false);
                    picto3.sprite = unknownRemedy;
                }                
            }
            else
            {
                symptome3.gameObject.SetActive(false);
            }

            /// SYMPTOM 4
            if (_symptome4 && _sc_cookBook.receipes[3].metSymptom == true)
            {
                
                symptomeHorGroup.gameObject.SetActive(true);
                symptomeTitle.gameObject.SetActive(true);
                Debug.Log("S4 care FP active");
                symptome4.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sc_cookBook.getAilmentName(3);
                symptome4.gameObject.SetActive(true);
                if (_sc_cookBook.getDiscoveredSymptom(3))
                {
                    picto4.transform.gameObject.SetActive(true);
                    picto4.sprite = _sc_cookBook.getRemede("treatment4").image;
                }
                else
                {
                    picto4.transform.gameObject.SetActive(false);
                    picto4.sprite = unknownRemedy;
                }                
            }
            else
            {
                symptome4.gameObject.SetActive(false);
            }
        }
        else if (_currentEtat == 2)
        {
            currentEtatText.text = "Cracheur de noir";
        }
        
    }
}
