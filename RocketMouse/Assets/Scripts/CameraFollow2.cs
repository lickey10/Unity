using System;
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
    public float MinX = -1000;//give a constraint for movement
    public float MaxX = -1000;//give a constraint for movement
    public float MinY = -1000;//give a constraint for movement
    public float MaxY = -1000;//give a constraint for movement
    [Space]
    public float smoothTime = 3;
    public float maxSpeed = 100;
    public float camHeight = 5;
    public float VerticalMovementSmoothing = 3;
    public float HorizontalMovementSmoothing = 3;
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
        if (MoveVertical)
            force.x = Input.GetAxis("Vertical");

        if (MoveHorizontal)
            force.z = Input.GetAxis("Horizontal");
    }

    private void MoveTarget()
    {
        if (target)
            target.AddForce(force, ForceMode.Force);
        else if(target2D)
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
            if (target2D)
            {
                Vector3 newPosition = Vector3.SmoothDamp(cam.transform.position, new Vector3(target2D.transform.position.x, target2D.transform.position.y + camHeight, -10), ref camVelocity, smoothTime, maxSpeed, Time.deltaTime + .5f);

                if (!MoveHorizontal || Math.Abs(newPosition.x - cam.transform.position.x) > HorizontalMovementSmoothing)
                    newPosition.x = cam.transform.position.x;

                //newPosition = Vector3.SmoothDamp(cam.transform.position, new Vector3(cam.transform.position.x, target2D.transform.position.y + camHeight, target2D.transform.position.z), ref camVelocity, smoothTime, maxSpeed, Time.deltaTime + .5f);

                if (!MoveVertical || Math.Abs(newPosition.y - cam.transform.position.y) > VerticalMovementSmoothing)//don't move camera unless we have moved enough to hit VerticalMovementSmoothing value
                    newPosition.y = cam.transform.position.y;
                //newPosition = Vector3.SmoothDamp(cam.transform.position, new Vector3(target2D.transform.position.x, cam.transform.position.y + camHeight, target2D.transform.position.z), ref camVelocity, smoothTime, maxSpeed, Time.deltaTime + .5f);

                cam.transform.position = validatePosition(newPosition);
            }
        }
    }

    private Vector3 validatePosition(Vector3 positionToValidate)
    {
        if (MinX > -1000 && positionToValidate.x < MinX && MoveHorizontal)
            positionToValidate.x = MinX;

        if (MaxX > -1000 && positionToValidate.x > MaxX && MoveHorizontal)
            positionToValidate.x = MaxX;

        if (MinY > -1000 && positionToValidate.y < MinY && MoveVertical)
            positionToValidate.y = MinY;

        if (MaxY > -1000 && positionToValidate.y > MaxY && MoveVertical)
            positionToValidate.y = MaxY;

        return positionToValidate;
    }
}
