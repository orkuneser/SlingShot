using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour
{
    #region SINGLETON
    public static BallController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public float maxRopeTension; // Upgrade

    public Transform controllableBall;
    public GameObject clampPosition;
    public GameObject LineObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LineObject.transform.position = new Vector3(0, 0, transform.position.z);
            LineObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            controllableBall.position = new Vector3(GetMousePosition().x, controllableBall.position.y, GetMousePosition().z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            LineObject.SetActive(false);
        }
    }

    private Vector3 GetMousePosition()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(controllableBall.position).z;
        Vector3 movePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        movePosition.x = Mathf.Clamp(movePosition.x, -4, 4);
        movePosition.z = Mathf.Clamp(movePosition.z, clampPosition.transform.position.z - maxRopeTension, clampPosition.transform.position.z);

        return movePosition;
    }
}
