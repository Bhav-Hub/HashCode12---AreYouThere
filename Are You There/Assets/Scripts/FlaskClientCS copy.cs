// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
// using System;
// using System.Text.RegularExpressions;
// using System.Xml;
// using UnityEngine.UI;

// namespace FlaskClientCS
// {
// public class TranscriptionManager : MonoBehaviour
// {
//     [SerializeField] private Text outputText;
//     //global string called output
//     public static class Hindi{
//         public static string output;
//     }

//     string url = "http://192.168.56.1:5000/transcribe"; // Replace with your Flask server URL

//     public IEnumerator TranscribeAudio(string audioPath, Action callback)
//     {
//         WWWForm form = new WWWForm();
//         form.AddField("audio", audioPath);

//         UnityWebRequest www = UnityWebRequest.Get(url);
//         yield return www.SendWebRequest();
//         Debug.Log("Transcription: " + www.downloadHandler.text);

//         string unicodeString = www.downloadHandler.text;

//         // Convert Unicode escape sequences to Hindi text
//         string hindiText = Regex.Unescape(unicodeString);
//         Hindi.output = hindiText;
//         outputText.text = hindiText;
        
//         Debug.Log(hindiText);

//         callback();
//     }

//     void Start()
//     {
//         string audioPath = @"C:\Users\bhave\Desktop\Bhaveen Clone\UnitedInUnity\Are You There\Assets\Scripts\RecordedAudio.wav";
//         StartCoroutine(TranscribeAudio(audioPath, DisplayHindiText));
//     }

//     //display function of hinditext attached to textbox on canvas
//     public void DisplayHindiText()
//     {
//         outputText.text = Hindi.output;
//     }
    
// }
// }