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
    private Vector3 smoothPos;
    public float smoothSpeed = 0.5f;

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
        camY = Mathf.Clamp(0.0f, yMin , yMax);
        camX = Mathf.Clamp(followTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);
        //this.transform.position = new Vector3(camX, 0.0f, 0.0f);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, 0.0f, this.transform.position.z), smoothSpeed);
        this.transform.position = smoothPos;
    }

    //void LateUpdate()
    //{
    //    //transform.position = player.transform.position + offset;
    //}
}