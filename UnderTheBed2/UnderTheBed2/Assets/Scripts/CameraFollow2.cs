using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Camera cam;
    public Rigidbody target;
    public Rigidbody2D target2D;
    public bool MoveVertical = true;
    public bool MoveHorizontal = true;
    [Space]
    public float smoothTime = 3;
    public float maxSpeed = 100;
    public float camHeight = 5;
    private Vector3 force;
    private Vector3 camVelocity;
    private float origCamX = 0;
    private float origCamY = 0;
    private float origCamZ = 0;

    private void Start()
    {
        if (target)
        {
            origCamX = target.transform.position.x;
            origCamY = target.transform.position.y;
            origCamZ = target.transform.position.z;
        }
        else
        {
            origCamX = target2D.transform.position.x;
            origCamY = target2D.transform.position.y;
            origCamZ = target2D.transform.position.z;
        }
    }

    private void Update()
    {
        GetForce();
    }

    private void FixedUpdate()
    {
        MoveTarget();
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void GetForce()
    {
        //if(MoveVertical)
            force.x = Input.GetAxis("Vertical");

        //if(MoveHorizontal)
            force.z = Input.GetAxis("Horizontal");
    }

    private void MoveTarget()
    {
        if (target)
            target.AddForce(force, ForceMode.Force);
        else
            target2D.AddForce(force, ForceMode2D.Force);
    }

    private void MoveCamera()
    {
        if (target)
        {
            Vector3 newPosition = Vector3.SmoothDamp(cam.transform.position, target.transform.position, ref camVelocity, smoothTime, maxSpeed, Time.deltaTime);
            newPosition.y = camHeight;
            cam.transform.position = newPosition;
            cam.transform.LookAt(target.transform, Vector3.up);
        }
        else //2D
        {
            Vector3 newPosition = Vector3.SmoothDamp(cam.transform.position, new Vector3(target2D.transform.position.x, target2D.transform.position.y + camHeight, target2D.transform.position.z), ref camVelocity, smoothTime, maxSpeed, Time.deltaTime + .5f);

            if (!MoveHorizontal)
                newPosition.x = cam.transform.position.x;

            //newPosition = Vector3.SmoothDamp(cam.transform.position, new Vector3(cam.transform.position.x, target2D.transform.position.y + camHeight, target2D.transform.position.z), ref camVelocity, smoothTime, maxSpeed, Time.deltaTime + .5f);

            if (!MoveVertical)
                newPosition.y = origCamY + camHeight;
                //newPosition = Vector3.SmoothDamp(cam.transform.position, new Vector3(target2D.transform.position.x, cam.transform.position.y + camHeight, target2D.transform.position.z), ref camVelocity, smoothTime, maxSpeed, Time.deltaTime + .5f);
            
            cam.transform.position = newPosition;
            //cam.transform.LookAt(target2D.transform, Vector3.up);
        }
    }
}
