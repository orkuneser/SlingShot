using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTrajectoryManager : MonoBehaviour
{
    public Ball ball;
    public float pushForce;
    public bool isDragging = false;

    
    private Vector3 _direction;
    private Vector3 _force;
    private float _distance;

    void Start()
    {
        ball.DesactivateRb();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }
    }

    void OnDragStart()
    {
        ball.DesactivateRb();
    }

    void OnDrag()
    {
        _distance = Vector3.Distance(BallController.Instance.clampPosition.transform.position, transform.position);
        _direction = (BallController.Instance.clampPosition.transform.position - transform.position).normalized;

        _force = _direction * _distance * pushForce;
    }

    void OnDragEnd()
    {
        ball.ActivateRb();

        ball.Push(_force);
    }
}
