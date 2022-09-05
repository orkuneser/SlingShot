using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RayTrajectory : MonoBehaviour
{
    #region OLD SYSTEM VARIABLES
    //private int totalBounce = 3;
    //private float lineOffSet = 0.01f;
    //public float rayDistance = 30;
    #endregion


    public LineRenderer lineRenderer;
    public int reflections;
    public float maxLenght;

    Ray ray;
    RaycastHit hit;

    private Vector3 _direction;
    private float _distance;

    private RayTrajectoryManager _rayTrajectoryManager;
    private RayTrajectoryManager RayTrajectoryManager => _rayTrajectoryManager == null ? _rayTrajectoryManager = GetComponent<RayTrajectoryManager>() : _rayTrajectoryManager;
    private void Update()
    {
        if (RayTrajectoryManager.isDragging)
        {
            _direction = BallController.Instance.clampPosition.transform.position - transform.position;
            _direction.y = 0;

            _distance = Vector3.Distance(BallController.Instance.clampPosition.transform.position, transform.position);

            if (_distance >= 2)
            {
                ray = new Ray(transform.position, _direction);

                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, transform.position);

                CheckRaycast();
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
        #region OLD SYSTEM

        //Vector3 direction = BallController.Instance.clampPosition.transform.position - transform.position;
        //Vector3 origin = transform.position + lineOffSet * direction;

        //direction.y = 0;

        //if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance))
        //{
        //    direction = Vector3.Reflect(direction.normalized, hit.normal);
        //    origin = hit.point + lineOffSet * direction;

        //    lineRenderer.SetPosition(0, origin);
        //    lineRenderer.SetPosition(1, hit.point);
        //}
        //else
        //{
        //    lineRenderer.SetPosition(0, transform.position);
        //    lineRenderer.SetPosition(1, transform.position + direction * rayDistance);
        //}

        #endregion
    }

    private void CheckRaycast()
    {
        float remainingLenght = maxLenght;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLenght))
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLenght -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.tag != "wall")
                    break;
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLenght);
            }
        }
    }
}
