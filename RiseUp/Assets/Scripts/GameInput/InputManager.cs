using UnityEngine;

namespace GameInput
{
    /// <summary>
    /// Checks for any input and invokes an event if 
    /// </summary>
    /// Author: Andreas Roither
    public class InputManager : MonoBehaviour
    {
        #region Fields

        public static readonly InputEvent ContinuousInputEvent = new InputEvent();
        public static readonly InputEvent InputEndEvent = new InputEvent();

        // depending on which device this runs on different inputs will be checked
        private enum Device
        {
            Pc,
            Mobile
        }

        private Device currentDevice;

        #endregion

        #region Methods

        private void Awake()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                currentDevice = Device.Mobile;
            }
            else
            {
                currentDevice = Device.Pc;
            }
        }

        private void Update()
        {
            // listen to any clicks
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (currentDevice == Device.Mobile)
            {
                CheckMobileInput();
            }
            else
            {
                CheckPcInput();
            }
        }

        /// <summary>
        /// Pc input generally goes over mouse buttons
        /// </summary>
        private static void CheckPcInput()
        {
            if (Input.GetMouseButton(0))
            {
                ContinuousInputEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        /// <summary>
        /// Mobile input uses touch
        /// </summary>
        private static void CheckMobileInput()
        {
            // Mobile input
            foreach (var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    InputEndEvent?.Invoke(touch.position);
                    return;
                }
                else
                {
                    ContinuousInputEvent?.Invoke(touch.position);
                    return;
                }
            }
        }

        #endregion
    }
}