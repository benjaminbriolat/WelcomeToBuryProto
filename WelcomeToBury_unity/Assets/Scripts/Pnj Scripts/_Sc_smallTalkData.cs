using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_smallTalkData : MonoBehaviour
{
    public static _Sc_smallTalkData instance = null;

    [System.Serializable]
    public class textType
    {
        public string typeName = null;
        public List<string> textList = null;
    }
    [SerializeField] List<textType> textTypes = new List<textType>();
    private void Awake()
    {
        instance = this;
    }

    public string getText(int type)
    {
        return textTypes[type].textList[(int)Random.Range(0, textTypes[type].textList.Count)];
    }
}
