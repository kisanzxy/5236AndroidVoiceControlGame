using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading;

public class VoiceController : MonoBehaviour {
    AudioClip audioclip;
    bool recording;
    byte[] audiodata;
    public Rigidbody2D m_Character;

    public CharacterVoiceControl characterVoiceControl;

    // Use this for initialization
    void Start() {
        Debug.Log("VoiceController Start");
        Debug.Log(characterVoiceControl);
    }

    void OnMouseDown(){
        Debug.Log("OnMouseDown bk");
        recording = true;
        Debug.Log(recording);
        m_Character.velocity = new Vector2(0, 0);
    }

    void OnMouseUp(){
        Debug.Log("OnMouseUp bk");
        recording = false;
        Debug.Log(recording);
    }
    
    // Update is called once per frame
    void Update() {
        if (recording) {
            if (!Microphone.IsRecording(null)) {
                audioclip = Microphone.Start(null, false, 5, 16000);
                Debug.Log("Recording started");
            }
        } else {
            if (Microphone.IsRecording(null)) {
                Microphone.End(null);
                Debug.Log("Recording stopped");
                if (audioclip != null) {
                    audiodata = WavUtility.FromAudioClip(audioclip);
                    string action = VoiceService.GetInstance().GetActionRecognition(audiodata);
                    audioclip = null;
                    Debug.Log(action);
                    characterVoiceControl.Move(action);
                }
            }
        }
    }
}
