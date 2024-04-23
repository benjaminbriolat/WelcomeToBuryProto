using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sc_DebugBoxColliderGizmo : MonoBehaviour
{
    [SerializeField] BoxCollider box = null;

    private void OnDrawGizmos()
    {
        if(box != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawWireCube(box.transform.position, box.size);
        }       
    }
}
