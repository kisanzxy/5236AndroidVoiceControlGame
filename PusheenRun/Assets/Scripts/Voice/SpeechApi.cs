using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class SpeechApi
{

    private string apiBaseUrl = "https://speech.googleapis.com/v1/speech:recognize?&key=";
    private string apiKey = "AIzaSyA-7SQx5JOyQHT4WAZb-I3QHaBd1wQhpaM";

    // Send aurdio file to Google Speech Recognition Service.
    // Return HTTP Response Result as a Json string
	public string SendAudio(byte[] audio)
	{
        string file64 = Convert.ToBase64String(audio, Base64FormattingOptions.None);
        Debug.Log(file64);
        string httpURL = this.apiBaseUrl + this.apiKey;
        // string response;
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
            Debug.Log(result.Length);
			streamReader.Close();
		}
		return result;
	}
}


