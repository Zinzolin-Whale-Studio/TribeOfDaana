using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.Log
{
    public class TestLogger : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ToDLog.Log("Test message");
        }

        
    }
}
