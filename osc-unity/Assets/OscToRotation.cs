using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OscCore.Demo
{
    public class OscToRotation : MonoBehaviour
    {        
        public OscReceiver Receiver;
        float x, y, z;

        void Awake() {
            Receiver.Server.TryAddMethod("/accelerometer/x", ReadX);
            Receiver.Server.TryAddMethod("/accelerometer/y", ReadY);
            Receiver.Server.TryAddMethod("/accelerometer/z", ReadZ);
        }
        void ReadX(OscMessageValues values)
        {
            x = values.ReadFloatElement(0);
        }
        void ReadY(OscMessageValues values)
        {
            y = values.ReadFloatElement(0);
        }
        void ReadZ(OscMessageValues values)
        {
            z = values.ReadFloatElement(0);
        }

        void Update() {
            float pitch, roll;
            int sign = (z > 0) ? 1 : -1;
            float miu = 0.001f;
            
            pitch = -90 + Mathf.Atan2(-x, Mathf.Sqrt(y*y + z*z)) * 180/Mathf.PI;
            roll  = -90 + Mathf.Atan2(y, sign* Mathf.Sqrt(z*z+ miu*x*x)) * 180/Mathf.PI; 

            transform.eulerAngles = new Vector3(0f, pitch, roll);
        }
    }
}