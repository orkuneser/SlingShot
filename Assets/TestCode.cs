using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestCode : MonoBehaviour
{
    public Rigidbody referenceObject;
    public GameObject startPosition;
    public Vector3 oldPosition;
    public Vector3 touchStartPosition;
    public float maxDistance;
    public float power;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPosition = GetMousePos();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var mousePosition = GetMousePos();
            var allowedPosition = mousePosition - touchStartPosition;
            allowedPosition = Vector3.ClampMagnitude(allowedPosition, maxDistance);
            var pos = startPosition.transform.position - (-allowedPosition);
            pos.z = Mathf.Clamp(pos.z, -maxDistance, startPosition.transform.position.z);
            transform.position = pos;
            // 

            //var newPosition = touchStartPosition - updatePosition;
            //var helperPos =  newPosition-transform.localPosition;

            //var distance = Vector3.Distance(startPosition, helperPos);
            ////helperPos = Vector3.MoveTowards(transform.localPosition, helperPos, maxDistance);
            ////transform.localPosition = -(helperPos.normalized * Mathf.Clamp(helperPos.magnitude,0, power));
            //if (distance > maxDistance)
            //{
            //    transform.localPosition = -(helperPos.normalized * Mathf.Clamp(helperPos.magnitude, 0, power));
            //    return;
            //}
            //transform.position -= newPosition;
            //touchStartPosition = GetMousePos();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

        }
    }

    public Vector3 GetMousePos()
    {
        var resultValue = Input.mousePosition;
        resultValue.z = 13;
        var newPos = Camera.main.ScreenToWorldPoint(resultValue);
        var t = newPos;
        t.y = 0;
        t.z = newPos.y;
        return t;
    }
    
    void OnDrawGizmosSelected()
    {
        if (startPosition != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            //Direction 
            var t = startPosition.transform.position - (-transform.position);
            // Direction 
            Gizmos.DrawLine(startPosition.transform.position, -transform.position);
        }
    }
}
