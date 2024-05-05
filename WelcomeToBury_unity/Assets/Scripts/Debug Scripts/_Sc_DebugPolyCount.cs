using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class _Sc_DebugPolyCount : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] GameObject[] objects = null;
    [SerializeField] int systemTotalTris = 0;
    [SerializeField] int systemTotalVert = 0;
    [SerializeField] int activeGameobjectTotalTris = 0;
    [SerializeField] int activeGameobjectTotalVert = 0;

    [Header("Specific Object Stats")]
    [SerializeField] GameObject selectedObject = null;
    [SerializeField] int selectedObjectTris = 0;
    [SerializeField] int selectedObjectVert = 0;

    private void Update()
    {
        if (selectedObject != null)
        {
            GetObjectStats(selectedObject, true);
        }
    }
    private void Start()
    {
        //systemTotalTris = UnityEditor.UnityStats.triangles;
        //systemTotalVert = UnityEditor.UnityStats.vertices;

        objects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in objects)
        {
            GetObjectStats(obj);
        }
    }

    void GetObjectStats(GameObject obj, bool uniqueObj = false)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>();
        if(filters.Length > 0)
        {
            foreach (MeshFilter f in filters)
            {
                if (uniqueObj == false)
                {
                    activeGameobjectTotalTris += f.sharedMesh.triangles.Length / 3;
                    activeGameobjectTotalVert += f.sharedMesh.vertexCount;
                }
                else
                {
                    selectedObjectTris = f.sharedMesh.triangles.Length / 3;
                    selectedObjectVert = f.sharedMesh.vertexCount;
                }

            }
        }
        else
        {
            if (uniqueObj == false)
            {
                activeGameobjectTotalTris += 0;
                activeGameobjectTotalVert += 0;
            }
            else
            {
                selectedObjectTris = 0;
                selectedObjectVert = 0;
            }
        }
        

    }
}
