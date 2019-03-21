﻿using System.Collections;
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

    public Text transcriptText; 
    public Text buttonText; 
    public Button button;
    private float RecordingTime;

    SpeechRecognition speechRecognition;
    public characterController characterController;

    // Use this for initialization
    void Start() {
        Debug.Log("VoiceController Start");
        recording = false;
        Debug.Log("recording" + recording);
        speechRecognition = new SpeechRecognition();
    }

    public void clickRecordButton(){
        if (!recording){
            buttonText.text = "Stop Recording";
            button.GetComponent<Image>().color = Color.red;
            recording = true;
            RecordingTime = 0;
            m_Character.velocity = new Vector2(0, 0);
            if (!Microphone.IsRecording(null)) {
                audioclip = Microphone.Start(null, false, 5, 16000);
                Debug.Log("Recording started");
            }
        } else {
            stopRecordingAndTranslateAction();
        }
        Debug.Log("recording" + recording);
    }

    public void stopMovement(){
        m_Character.velocity = new Vector2(0, 0);
    }
    
    void FixedUpdate() {
        if (recording) {
            RecordingTime += Time.deltaTime;
            if (RecordingTime > 5){
                stopRecordingAndTranslateAction();
            }
        } else {
            stopRecordingAndTranslateAction();
        }
    }

    void stopRecordingAndTranslateAction(){
    	buttonText.text = "Start Recording";
        button.GetComponent<Image>().color = Color.green;
        recording = false;
        RecordingTime = 0;
        if (Microphone.IsRecording(null)) {
            Microphone.End(null);
            Debug.Log("Recording stopped");
        }
    	if (audioclip != null) {
            audiodata = WavUtility.FromAudioClip(audioclip);
            string transcript = speechRecognition.GetTranscript(audiodata);
            string action = speechRecognition.GetAction(transcript);
            audioclip = null;
            Debug.Log("Transcript: " + transcript);
            Debug.Log("Action: " + action);
            transcriptText.text = "You said: " + transcript + "\nAction: " + action;
            characterController.Move(action);
        }
    }
}