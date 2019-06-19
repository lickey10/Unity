using System;
using Lean;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public GameObject TiltSteerV;
        public GameObject TiltSteerH;

#if MOBILE_INPUT
        float turnSpeed = 10f;
        float maxTurnLean = 50.0f;
        float maxTilt = 50.0f;
        float sensitivity = 10.0f;
#else
        float turnSpeed = 10.0f;
        float maxTurnLean = 50.0f;
        float maxTilt = 50.0f;
        float sensitivity = 10.0f;
#endif

        private Vector3 euler = Vector3.zero;
        float h = 0;
        float v = 0;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        protected virtual void OnEnable()
        {
#if MOBILE_INPUT
            // Hook the OnFingerSet event
            Lean.LeanTouch.OnFingerSet += OnFingerSet;
            Lean.LeanTouch.OnFingerUp += OnFingerUp;
#endif
        }

        protected virtual void OnDisable()
        {
#if MOBILE_INPUT
            // Unhook the OnFingerSet event
            Lean.LeanTouch.OnFingerSet -= OnFingerSet;
#endif
        }

        private void FixedUpdate()
        {
            if (!gamestate.Instance.GetGamePaused())
            {
#if !MOBILE_INPUT
                // pass the input to the car!
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                v = CrossPlatformInputManager.GetAxis("Vertical");

                float handbrake = CrossPlatformInputManager.GetAxis("Jump");
                m_Car.Move(h, v, v, handbrake);
#else
            // pass the input to the car!
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            //v = CrossPlatformInputManager.GetAxis("Vertical");

            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);

            ////h = CrossPlatformInputManager.GetAxis("Horizontal");
            ////v = CrossPlatformInputManager.GetAxis("Vertical");
            ////float handbrake = CrossPlatformInputManager.GetAxis("Jump");

            //Vector3 accelerator = Input.acceleration;

            //// Rotate turn based on acceleration		
            //euler.y += accelerator.x * turnSpeed;
            //// Since we set absolute lean position, do some extra smoothing on it
            ////euler.z = Mathf.Lerp(euler.z, -accelerator.x * maxTurnLean, 0.2f);
            //euler.z = Mathf.Lerp(euler.z, accelerator.x * maxTurnLean, 0.2f);

            //// Since we set absolute lean position, do some extra smoothing on it
            //euler.x = Mathf.Lerp(euler.x, accelerator.y * maxTilt, 0.2f);

            //// Apply rotation and apply some smoothing
            //Quaternion rot = Quaternion.Euler(euler);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rot, sensitivity);

            ////TiltSteerH.GetComponent<TiltInput>().

            ////m_Car.Move(euler.z, v, v, 0f);
            ////if(h == 0)
            //    //m_Car.Move(Quaternion.Lerp(transform.rotation, rot, sensitivity).z, v, v, 0f);
            ////else
            ////    m_Car.Move(h, v, v, handbrake);
#endif
            }
        }

        public void OnFingerUp(Lean.LeanFinger finger)
        {
            v = 0;
        }

        public void OnFingerSet(Lean.LeanFinger finger)
        {
            // Right side of the screen?
            if (finger.ScreenPosition.x > Screen.width / 2)
            {
                if(finger.ScreenPosition.y > Screen.height / 2)//the top half of the screen
                {
                    // Does it exist?
                    //if (RightObject != null)
                    //{
                    // Position it in front of the finger
                    //RightObject.position = finger.GetWorldPosition(10.0f);

                    //reverse
                    v = -30;
                    //}
                }
                else //the bottom half of the screen
                {
                    //forward
                    v = 30;
                }
            }
            // Left side?
            else
            {
                // Does it exist?
                //if (RightObject != null)
                //{
                    // Position it in front of the finger
                    //LeftObject.position = finger.GetWorldPosition(10.0f);

                //}
            }

            // NOTE: If you want to prevent fingers from crossing sides then you can check finger.StartScreenPosition first
        }
    }
}
