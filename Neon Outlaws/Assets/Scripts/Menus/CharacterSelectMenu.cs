using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectMenu : MonoBehaviour {
    public GameObject characterSelectPanel;
    public GameObject mainMenuPanel;


    // Load the development fight scene
    public void LoadSceneOnSelect()
    {
        SceneManager.LoadScene("Dev-Chris-AI");
    }

    public void LoadMainMenu()
    {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}