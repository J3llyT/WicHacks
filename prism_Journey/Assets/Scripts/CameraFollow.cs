using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    public Transform followTransform;
    public Bounds mapBounds;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;

    private void Start()
    {
        xMin = mapBounds.min.x;
        xMax = mapBounds.max.x;
        yMin = mapBounds.min.y;
        yMax = mapBounds.max.y;
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 2.0f;

        offset = transform.position - player.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       // camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);
        this.transform.position = new Vector3(camX, 0.0f, this.transform.position.z);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}