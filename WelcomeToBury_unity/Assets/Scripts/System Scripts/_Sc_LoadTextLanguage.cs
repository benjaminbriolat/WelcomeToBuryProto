using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Sc_LoadTextLanguage : MonoBehaviour
{
    [SerializeField] string key = null;
    _Sc_LocalisationMaster _localisationMaster = null;
    string returnedString = null;
    TextMeshProUGUI textToSet = null;
    TextMeshPro textToSetNoUi = null;
    string lastBeforeData = null;
    string lastAfterData = null;
    //[SerializeField] string[] datas = null;
    string[] rows = null;
    [SerializeField] bool launchFromStart = true;

    [System.Serializable]
    public class textDatas
    {
        public TextAsset languageDatas = null;
        public string[] datas = null;
    }
    [SerializeField] List<textDatas> datasList = new List<textDatas>();
    void Awake()
    {

    }
    private void Start()
    {
        _localisationMaster = _Sc_LocalisationMaster.instance;
        _localisationMaster.addToList(this);
        if (key != "0")
        {
            if (launchFromStart == true)
            {
                setText("", key, "");
                Debug.Log(" Set text launch");
            }
        }
    }
    public void setText(string extraDataBefore, string sentString, string extraDataAfter)
    {
        key = sentString;
        textToSet = transform.GetComponent<TextMeshProUGUI>();
        textToSetNoUi = transform.GetComponent<TextMeshPro>();
        for (int j = 0; j < datasList.Count; j++)
        {
            Debug.Log(" Set text search lists");
            datasList[j].datas = datasList[j].languageDatas.text.Split(new char[] { '\n' });

            for (int i = 1; i < datasList[j].datas.Length - 1; i++)
            {
                
                rows = datasList[j].datas[i].Split(new char[] { '@' });
                Debug.Log(" Set text search rows");
                if (rows[0] == sentString)
                {
                    Debug.Log(" Set text found match");
                    returnedString = rows[_localisationMaster.languageUsed];
                    SetTextMeshPro(extraDataBefore, extraDataAfter);
                    Debug.Log(" Set text Value as : " + returnedString);
                    return;
                    
                }
            }
        }
    }
    public void setLanguageAnew()
    {
        if (rows != null)
        {
            if (rows.Length > 1)
            {
                returnedString = rows[_localisationMaster.languageUsed];
                SetTextMeshPro(lastBeforeData, lastAfterData);
            }
        }
    }
    private void SetTextMeshPro(string extraBefore, string extraAfter)
    {
        if (textToSet != null)
        {
            textToSet.text = extraBefore + returnedString + extraAfter;
            lastBeforeData = extraBefore;
            lastAfterData = extraAfter;
            Debug.Log(" Set text Done as: "+ returnedString);
        }
        else if (textToSetNoUi != null)
        {
            textToSetNoUi.text = extraBefore + returnedString + extraAfter;
            lastBeforeData = extraBefore;
            lastAfterData = extraAfter;
        }
    }
}
