using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_LocalisationMaster : MonoBehaviour
{
    public static _Sc_LocalisationMaster instance = null;
    [SerializeField] List<_Sc_LoadTextLanguage> listTextObject = null;
    public int languageUsed = 0;
    [SerializeField] List<string> languages = null;
    private void Awake()
    {
        instance = this;
        listTextObject = new List<_Sc_LoadTextLanguage>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (languageUsed < languages.Count+2 )
            {
                languageUsed += 1;
            }
            else
            {
                languageUsed = 3;
            }
            changeLanguage();
        }
    }
    public void addToList(_Sc_LoadTextLanguage sentText)
    {
        listTextObject.Add(sentText);
    }

    public void changeLanguage()
    {
        for (int i = 0; i < listTextObject.Count; i++)
        {
            listTextObject[i].setLanguageAnew();
        }
    }
}
