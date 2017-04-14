using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
