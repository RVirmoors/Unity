using System.Collections;
using System.Collections.Generic;
//using BlobHandles;
using UnityEngine;

namespace OscCore.Demo
{
    public class OscToRotation : MonoBehaviour
    {        
        public OscReceiver Receiver;
        float x, y, z;

        void Awake() {
            //Receiver = GameObject.Find("Directional Light").GetComponent<OscReceiver>();
            Receiver.Server.TryAddMethod("/accelerometer/x", ReadValues);
        }
        void ReadValues(OscMessageValues values)
        {
            x = values.ReadFloatElement(0);
        }

        void Update() {
            transform.Rotate(x, 0, 0);
        }
    }
}