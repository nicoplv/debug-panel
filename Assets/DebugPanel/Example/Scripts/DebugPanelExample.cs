using UnityEngine;

namespace DebugPanelExamples
{
    public class DebugPanelExample : MonoBehaviour
    {
        #region Unity Methods

        public void Update()
        {
            // Log the current time
            DebugPanel.Log("Time", Time.time);

            // Log the current delta time in a group "Time Information"
            DebugPanel.Log("Delta Time", Time.deltaTime, "Time Information");
        }

        #endregion
    }
}