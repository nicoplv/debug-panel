using UnityEngine;

namespace DebugPanelExamples
{
    public class DebugPanelExample : MonoBehaviour
    {
        #region Unity Methods

        public void Update()
        {
            DebugPanel.Log("Time", Time.time, "Time Information");
            DebugPanel.Log("Delta Time", Time.deltaTime, "Time Information");
        }

        #endregion
    }
}