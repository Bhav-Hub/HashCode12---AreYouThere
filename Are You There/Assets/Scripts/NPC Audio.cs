using UnityEngine;

public class FindAudioListeners : MonoBehaviour
{
    void Start()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        Debug.Log("Number of Audio Listeners: " + listeners.Length);
        Debug.Log("Audio Listener 1 found at"+ listeners[0].gameObject.name);
        Debug.Log("Audio Listener 2 found at"+ listeners[1].gameObject.name);
    }
}
