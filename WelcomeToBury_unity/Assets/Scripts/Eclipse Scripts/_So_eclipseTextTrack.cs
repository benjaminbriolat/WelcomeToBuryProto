using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/eclipseText")]
public class _So_eclipseTextTrack : ScriptableObject
{
    [System.Serializable]
    public class EclipseLine
    {
        public string lineText = null;
    }
    public List<EclipseLine> eclipseLines = new List<EclipseLine>();
}
