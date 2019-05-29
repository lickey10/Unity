using UnityEngine;

namespace UnitySampleAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {
        public PowerbarController PowerbarController;

        public void SetDownState(string name)
        {
            CrossPlatformInputManager.SetButtonDown(name);

            PowerbarController.AddPower();
        }
        
        public void SetUpState(string name)
        {
            CrossPlatformInputManager.SetButtonUp(name);

            PowerbarController.SubtractPower();
        }
        
        public void SetAxisPositiveState(string name)
        {
            CrossPlatformInputManager.SetAxisPositive(name);

        }
        
        public void SetAxisNeutralState(string name)
        {
            CrossPlatformInputManager.SetAxisZero(name);
        }
        
        public void SetAxisNegativeState(string name)
        {
            CrossPlatformInputManager.SetAxisNegative(name);
        }
    }
}