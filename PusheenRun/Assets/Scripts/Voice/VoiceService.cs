using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VoiceService {

	SpeechApi speechApi;
	private static VoiceService instance;

	public static VoiceService GetInstance() {
		if (instance == null) {
			instance = new VoiceService();
		}
		return instance;
	}

	// Use this for initialization
	private VoiceService () {
		speechApi = new SpeechApi();
	}

	// Get action
	public string GetActionRecognition(byte[] audio) {
		// Invalid speech -> No action
		string result = "None";

		// Get speech recognition result from service
		string response = speechApi.SendAudio(audio);
		
		if(response.Length != 3){
			VoiceResult voiceResult = JsonUtility.FromJson<VoiceResult>(response);
			string transcript = voiceResult.results[0].alternatives[0].transcript;
			Debug.Log(transcript);

			if (transcript.Contains("left")){
				result = "Left";
			} else if (transcript.Contains("right")){
				result = "Right";
			} else if (transcript.Contains("up")){
				result = "Up";
			} else if (transcript.Contains("down")){
				result = "Down";
			}
		}

		return result;
	}
}

[Serializable]
public class VoiceResult {
	public SpeechRecognitionResult[] results;
}

[Serializable]
public class SpeechRecognitionResult {
	public Alternative[] alternatives;
	public int channelTag;
}

[Serializable]
public class Alternative {
	public string transcript;
	public float confidence;
	public WordInfo[] words; 
}

[Serializable]
public class WordInfo {
	public string startTime;
	public string endTime;
	public string word;
}

