using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< HEAD
public class StageSelection : MonoBehaviour {
    public GameObject stageSelectPanel;
    public GameObject characterSelectPanel;
    
    // Load the development fight scene
    public void LoadSceneOnSelect() {
        SceneManager.LoadScene("Dev-Chris-AI");
    }

    public void LoadCharacterMenu() {
        stageSelectPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }
}
=======
public class StageSelectMenu : MonoBehaviour {
    public GameObject stageSelectPanel;
    public GameObject characterSelectPanel;
    
    public void LoadSceneOnSelect() {
        SceneManager.LoadScene("Dev-Chris-Stage");
    }

    public void LoadCharacterSelect() {
        stageSelectPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

}
>>>>>>> origin/Dev
