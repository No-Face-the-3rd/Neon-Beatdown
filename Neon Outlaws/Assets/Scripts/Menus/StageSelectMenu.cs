using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageSelectMenu : MonoBehaviour {
    menuInputState inputState;
    MenuInputListener menuInputListener;

    public GameObject stageSelectPanel;
    public GameObject characterSelectPanel;
    public GameObject[] stageButtons; // Selectable images (down below)
    public GameObject[] stagePreviewImages;      // Large images
    //public GameObject stageSelectImage; // Highlight border

    void Awake() {
        //stageSelectImage.transform.position = stageButtons[0].transform.position;
        stagePreviewImages[0].SetActive(true);
    }

    void FixedUpdate() {
        if (menuInputListener != null)
        {
            TakeInput(menuInputListener.getCurState());
        }
        else
        {
            menuInputListener = PlayerLocator.locator.getMenuListener(1);
        }

        for (int i = 0; i < stageButtons.Length; i++)
        {
            for (int j = 0; i < stagePreviewImages.Length; i++)
            {
                //stageSelectImage.transform.position = stageButtons[i].transform.position;
                stagePreviewImages[j] = stageButtons[i]; 
            }
        }
    }

    void TakeInput(menuInputState theMenuInputState) {
        inputState = new menuInputState(theMenuInputState);
    }

    public void LoadSceneOnSelect() {
        SceneManager.LoadScene("Dev-Chris-Stage");
    }

    public void LoadCharacterSelect() {
        stageSelectPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

}