using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuuus : MonoBehaviour
{
    [SerializeField] private string continueScene;
    [SerializeField] private string newGameScene;

    public void ContinueGame()
    {
        // Load the scene where your game continues
        SceneManager.LoadScene(continueScene);
    }

    public void NewGame()
    {
        // Load the scene where your new game starts
        SceneManager.LoadScene(newGameScene);
    }
}