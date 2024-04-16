using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine.UI;

namespace testFlaskClientCS
{
    public static class Hindi{
        public static string output;
    }
public class FlaskClientCS : MonoBehaviour
{
    
    [SerializeField] private Text outputText;
    //global string called output
   

    [System.Serializable]
    public class TranscriptionResponse
    {
        public string Transcription;
    }

    string url = "http://192.168.56.1:5000/transcribe"; // Replace with your Flask server URL

    public IEnumerator TranscribeAudio(string audioPath, Action callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("audio", audioPath);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        Debug.Log("Transcription: " + www.downloadHandler.text);

        string jsonResponse = www.downloadHandler.text;

        // Parse JSON response
        TranscriptionResponse response = JsonUtility.FromJson<TranscriptionResponse>(jsonResponse);

        // Convert Unicode escape sequences to Hindi text
        string hindiText = Regex.Unescape(response.Transcription);
        Hindi.output = hindiText;
        outputText.text = hindiText;
        
        Debug.Log(hindiText);

        callback();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
        string audioPath = @"C:\Users\bhave\Desktop\Bhaveen Clone\UnitedInUnity\Are You There\Assets\Scripts\RecordedAudio.wav";
        StartCoroutine(TranscribeAudio(audioPath, DisplayHindiText));
        }
    }

    //display function of hinditext attached to textbox on canvas
    public void DisplayHindiText()
    {
        outputText.text = Hindi.output;
    }
    
}
}