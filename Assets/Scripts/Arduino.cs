using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;
using System.IO.Ports;
using System.Threading;

public class Arduino : MonoBehaviour {
    private List<VisualEffect> visualeffects = new List<VisualEffect>();
    [SerializeField, Range(0, 5)]
    float Orb_Radius = 0f;
    [SerializeField, Range(0, 5)]
    float Orb_Intensity = 0f;
    private SerialPort serialPort;

    private float x_val = 0;
    private float y_val = 0;
    private float button_state = 0;

    private void Awake() {
        ShowPort();
    }

    private void Update() {
        if (serialPort != null) {
            serialPort.Write("A");
            string message = serialPort.ReadTo("EOL").Trim();
            //UnityEngine.Debug.Log("Message: " + message);
            string[] value = message.Split(',');
            float LED_Brightness = float.Parse(value[0]);
            x_val = float.Parse(value[1]) / 512f * 30f;
            //UnityEngine.Debug.Log("X: " + x_val); 
            y_val = float.Parse(value[2]) / 512f * 30f;
            //UnityEngine.Debug.Log("Y: " + y_val); 
            button_state = float.Parse(value[3]);
            //UnityEngine.Debug.Log("Button State: " + button_state);
            //UnityEngine.Debug.Log("Arduino Values: " + LED_Brightness);

            //UnityEngine.Debug.Log(visualeffects.Count);
            for (int i = visualeffects.Count - 1; i > -1; i--) {
                if (visualeffects[i] != null) {

                    if (LED_Brightness > 400)
                    {
                        visualeffects[i].SetFloat("Radius_Size", (LED_Brightness - 400f) / 100f);
                    }
                    else
                    {
                        visualeffects[i].SetFloat("Radius_Size", 0);
                    }
                    //visualeffects[i].SetFloat("Radius_Size", (1000 - 400f) / 1000f);
                } else {
                    visualeffects.RemoveAt(i);
                }
            }
        } else {
            UnityEngine.Debug.Log("Serial Port is null!");
        }
    }

    private void OnDestroy() {
        if (serialPort != null) {
            serialPort.Close();
        }
    }

    private void ShowPort() {
        foreach (string portname in SerialPort.GetPortNames()) {
            Debug.Log("Trying: " + portname);
            var stream = new SerialPort(portname, 9600);
            stream.ReadTimeout = 500;

            try {
                stream.Open();
                stream.WriteLine("A");
                stream.Close();
                Thread.Sleep(500);
                serialPort = new SerialPort(portname, 9600);
                serialPort.ReadTimeout = 500;
                serialPort.Open();
            } catch (Exception e) {
                // Debug.Log(e);
                //Debug.Log("device NOT connected to: " + portname);
            } finally {
                stream.Close();
            }
        }
    }

    public void AddOrb(VisualEffect orb) {
        visualeffects.Add(orb);
    }

    public float XVal { get { return x_val; } }

    public float YVal { get { return y_val; } }

    public float ButtonState { get { return button_state; } }
}
