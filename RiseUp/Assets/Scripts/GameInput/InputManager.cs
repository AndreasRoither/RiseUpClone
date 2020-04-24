using UnityEngine;

namespace GameInput
{
    /// <summary>
    ///     Checks for any input and invokes an event if
    /// </summary>
    /// Author: Andreas Roither
    public class InputManager : MonoBehaviour
    {
        public static readonly InputEvent ContinuousInputEvent = new InputEvent();
        public static readonly InputEvent InputEndEvent = new InputEvent();

        private Device currentDevice;

        private void Awake()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                currentDevice = Device.Mobile;
            else
                currentDevice = Device.Pc;
        }

        private void Update()
        {
            // listen to any clicks
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (currentDevice == Device.Mobile)
                CheckMobileInput();
            else
                CheckPcInput();
        }

        /// <summary>
        ///     Pc input generally goes over mouse buttons
        /// </summary>
        private static void CheckPcInput()
        {
            if (Input.GetMouseButton(0))
                ContinuousInputEvent?.Invoke(Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition));
            else if (Input.GetMouseButtonUp(0))
                InputEndEvent?.Invoke(Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition));
        }

        /// <summary>
        ///     Mobile input uses touch
        /// </summary>
        private static void CheckMobileInput()
        {
            // Mobile input
            foreach (var touch in Input.touches)
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    InputEndEvent?.Invoke(Camera.allCameras[0].ScreenToWorldPoint(touch.position));
                    return;
                }
                else
                {
                    ContinuousInputEvent?.Invoke(Camera.allCameras[0].ScreenToWorldPoint(touch.position));
                    return;
                }
        }

        // depending on which device this runs on different inputs will be checked
        private enum Device
        {
            Pc,
            Mobile
        }
    }
}