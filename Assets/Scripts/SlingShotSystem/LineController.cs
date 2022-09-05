using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Transform targetTransform;
    private LineRenderer _lineRenderer;
    private LineRenderer LineRenderer => _lineRenderer == null ? _lineRenderer = GetComponent<LineRenderer>() : _lineRenderer;


    private void Update()
    {
        LineRenderer.SetPosition(0, transform.position);
        LineRenderer.SetPosition(1, targetTransform.position);
    }

}
