using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);
        public GameObject CameraBoundsObject;

        private float rightBound;
        private float leftBound;
        private float topBound;
        private float bottomBound;
        float camX = -1;
        float camY = -1;
        float camZ = -1;
        Bounds bounds = new Bounds();

        private void Start()
        {
            bounds = CameraBoundsObject.GetComponent<Collider>().bounds;

            
        }

        void Update()
        {


            camX = Mathf.Clamp(target.transform.position.x, bounds.min.x, bounds.max.x);
            camZ = Mathf.Clamp(target.transform.position.y, bounds.min.y, bounds.max.y);
            camZ = Mathf.Clamp(target.transform.position.z, bounds.min.z, bounds.max.z);

            //cam.transform.position = new Vector3(camX, camY, cam.transform.position.z);
        }

        private void LateUpdate()
        {
            //transform.position = target.position + offset;
            transform.position = new Vector3(camX, camY, target.position.z) + offset;
            //transform.LookAt(target);
            //transform.rotation = new Quaternion(transform)

            Vector3 eulers = transform.eulerAngles;
            //eulers.x = 0;
            transform.eulerAngles = eulers;
        }
    }
}
