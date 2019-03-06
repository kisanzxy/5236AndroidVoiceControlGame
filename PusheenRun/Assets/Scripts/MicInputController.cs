using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicInputController : MonoBehaviour
{

    public static float volume;
    public float difference = 3f;
    private float multiplier;
    private string device;
    private const float amplify = 1000;


AudioClip micRecord;
    // Start is called before the first frame update
    void Start()
    {
        volume = 5f;
        device = Microphone.devices[0];
        multiplier = 1f;
        Debug.Log(device);
        micRecord = Microphone.Start(device, true, 999, 44100);
        Debug.Log(micRecord);
    }

    // Update is called once per frame
    void Update()
    {
    
        volume = (float)Math.Round(GetMaxVolume(), 4) * difference * multiplier*amplify;
        //Debug.Log("volume: "+volume);
    }
    void OnGUI() {
        GUI.Label(new Rect(0, 30, 60, 40), "volume: ");
        multiplier = GUI.HorizontalSlider(new Rect(35, 25, 100, 30), multiplier, 1, 10);

    }
    private float GetMaxVolume()
    {
        float maxVolume = 0f;

        // store volum
        float[] volumeData = new float[128];

        int offset = Microphone.GetPosition(device) - 128 + 1;
        if (offset < 0)
        {
            return 0;
        }

        //snipe record from offset
        micRecord.GetData(volumeData, offset);
        Debug.Log(micRecord.GetData(volumeData, offset));
        //find max volum
        for (int i = 0; i < volumeData.Length; i++)
        {
            float tempMax = volumeData[i];
            if (tempMax > maxVolume)
            {
                maxVolume = tempMax;
            }
        }
        return maxVolume;
    }


    }
