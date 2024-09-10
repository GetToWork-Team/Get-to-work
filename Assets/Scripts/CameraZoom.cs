using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    [SerializeField] private float zoom;
    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float zoomMin;
    [SerializeField] private float zoomMax;
    [SerializeField] private float velocity;
    [SerializeField] private float smoothTime;
    [SerializeField] private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        zoom = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, zoom, ref velocity, smoothTime);
    }
}
