using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_debugCare : MonoBehaviour
{
    [SerializeField] _Sc_pnjState targetpnj = null;
    _Sc_cookbook _sc_cookBook = null;
    _Sc_Calendrier _sc_calendrier = null;

    private void Start()
    {
        _sc_cookBook = _Sc_cookbook.instance;
        _sc_calendrier = _Sc_Calendrier.instance;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Care();
        }
    }

    public void Care()
    {
        _sc_calendrier.AdvanceCalendar();
        if (targetpnj.symptome1 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment1", null);
        }
        else if(targetpnj.symptome2 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment2", null);
        }
        else if (targetpnj.symptome3 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment3", null);
        }
        else if (targetpnj.symptome4 == true)
        {
            _sc_cookBook.AdvanceDiscovery("treatment4", null);
        }
       // targetpnj.UpdateTrustLevel();
    }
}
