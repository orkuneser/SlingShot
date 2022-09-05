using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryManager : MonoBehaviour
{
    Camera cam;

    public Ball ball;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 4f;

    bool isDragging = false;

    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 direction;
    Vector3 force;
    float distance;

    //---------------------------------------
    void Start()
    {
        cam = Camera.main;
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

    //-Drag--------------------------------------
    void OnDragStart()
    {
        ball.DesactivateRb();
        startPoint = GetMousePosition();

        trajectory.Show();
    }

    void OnDrag()
    {
        endPoint = GetMousePosition();
        distance = Vector3.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        distance = Mathf.Clamp(distance, 0, 0.6f); // Dots Spacing Limit

        force = direction * distance * pushForce;

        //just for debug
        Debug.DrawLine(startPoint, endPoint);

        trajectory.UpdateDots(ball.pos, force);
    }

    void OnDragEnd()
    {
        //push the ball
        ball.ActivateRb();

        ball.Push(force);

        trajectory.Hide();
    }

    private Vector3 GetMousePosition()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 movePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.y));
        //movePosition.x = Mathf.Clamp(movePosition.x, -4, 4);
        //movePosition.z = Mathf.Clamp(movePosition.z, BallController.Instance.clampPosition.transform.position.z - BallController.Instance.maxRopeTension, BallController.Instance.clampPosition.transform.position.z);

        return movePosition;
    }
}
