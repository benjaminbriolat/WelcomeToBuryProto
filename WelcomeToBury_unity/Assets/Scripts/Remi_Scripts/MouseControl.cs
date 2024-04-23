using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


public class MouseControl : MonoBehaviour
{

    private float o_size;
    private Vector3 o_position;
    private Quaternion o_rotation;
    private Tweener move;

    private float time;

    Vector2 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        o_size = Camera.main.fieldOfView;
        o_position = Camera.main.transform.position;
        o_rotation = Camera.main.transform.rotation;

        time = 0.2f;

        oldPosition = Input.mousePosition;

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 deltaPosition = (Vector2)Input.mousePosition - oldPosition;

        if (Input.GetMouseButton(0))
        {
            if (time >= 0f)
                time -= Time.deltaTime;


            Camera.main.transform.RotateAround(Camera.main.transform.GetChild(0).position, Vector3.up, deltaPosition.x*0.025f);

            Vector3 euler = Camera.main.transform.rotation.eulerAngles;
            if( (euler.x >= 10f && deltaPosition.y > 0) || (euler.x <= 85f && deltaPosition.y < 0))
                Camera.main.transform.RotateAround(Camera.main.transform.GetChild(0).position, -Camera.main.transform.right, deltaPosition.y * 0.025f);

            //Camera.main.transform.rotation = Quaternion.Euler(Mathf.Clamp(euler.x, 25f, 90f), euler.y, euler.z);


        } else if (Input.GetMouseButtonUp(0))
        {

            if (time >= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (move != null)
                        move.Kill();

                    //Debug.Log(hit.collider.name);
                    Vector3 translate = Camera.main.transform.GetChild(0).position - hit.point;

                    move = Camera.main.transform.DOMove(Camera.main.transform.position - translate, 1f);
                }
            }

            time = 0.2f;

        } else if (Input.mouseScrollDelta.y != 0)
        {
            Camera.main.fieldOfView += Input.mouseScrollDelta.y * 0.1f ;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 2f, 60f);

        } else if (Input.GetMouseButtonUp(1))
        {
            if (move != null)
                move.Kill();

            Camera.main.transform.position = o_position;
            Camera.main.fieldOfView = o_size;
            Camera.main.transform.rotation = o_rotation;
        }

        oldPosition = Input.mousePosition;
    }
}
