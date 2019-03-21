using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class SpeechRecognition
{
    private string apiBaseUrl = "https://speech.googleapis.com/v1/speech:recognize?&key=";
    private string apiKey = "AIzaSyA-7SQx5JOyQHT4WAZb-I3QHaBd1wQhpaM";

    // Send aurdio file to Google Speech Recognition Service.
    // Return HTTP Response Result as a Json string
	public string GetTranscript(byte[] audio)
	{
        string file64 = Convert.ToBase64String(audio, Base64FormattingOptions.None);
        Debug.Log(file64);
        string httpURL = this.apiBaseUrl + this.apiKey;
        string result = "";

		// Send audio file to speech recognition service by HTTP POST
        Debug.Log("Sending 'HTTP POST' request to URL: " + httpURL);
		HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(httpURL);
		request.Method = "POST";
        request.ContentType = "application/json";
		using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream())) {
            string json = "{ \"config\": { \"languageCode\" : \"en-US\" }, \"audio\" : { \"content\" : \"" + file64 + "\"}}";
            Debug.Log("Request: " + json);
            requestStream.Write(json);
            requestStream.Flush();
            requestStream.Close();
		}
		
        // Get the response
		using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
			result = streamReader.ReadToEnd();
            Debug.Log("Response: " + result);
            Debug.Log("Response length: " + result.Length);
			streamReader.Close();
		}

        String transcript = "nothing";
        // Get valid speech
        if (result.Length > 3){
            VoiceResult voiceResult = JsonUtility.FromJson<VoiceResult>(result);
            transcript = voiceResult.results[0].alternatives[0].transcript;
        }
        return transcript;
	}

    // Get the action from the transcript
    public string GetAction(string transcript) {
        string action = "None";

        if (transcript != "nothing"){
            if (transcript.Contains("left")){
                action = "Left";
            } else if (transcript.Contains("right")){
                action = "Right";
            } else if (transcript.Contains("up")){
                action = "Up";
            } else if (transcript.Contains("down")){
                action = "Down";
            }
        }

        return action;
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


