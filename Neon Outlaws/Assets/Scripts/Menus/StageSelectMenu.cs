using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectMenu : MonoBehaviour {
    TitleMenu titleMenu;
    menuInputState inputState;
    MenuInputListener menuInputListener;
    public ObjectDB oDB;

    public GameObject stageSelectPanel;
    public GameObject characterSelectPanel;
    public GameObject[] stageButtons; // Selectable images (down below)
    public GameObject[] stagePreviewImages; // Large images
    int index = 0;

    void Awake() {
        //stagePreviewImages[index].SetActive(true);
    }

    void FixedUpdate() {
        if (menuInputListener != null) {
            TakeInput(menuInputListener.getCurState());
        }
        else {
            menuInputListener = PlayerLocator.locator.getMenuListener(1);
        }

        if (inputState.accept.wasPressed) {
            if (oDB.selectedStage == 2)
                oDB.selectedStage = Random.Range(0, 2);

            for (index = 0; index < stagePreviewImages.Length; index++) {
                if (titleMenu.menuEventSystem.currentSelectedGameObject == stageButtons[0])
                    index = 0;
                else if (titleMenu.menuEventSystem.currentSelectedGameObject == stageButtons[1])
                    index = 1;
                else if (titleMenu.menuEventSystem.currentSelectedGameObject == stageButtons[2])
                    index = 2;
            }

            if (index < 0)
                index = stagePreviewImages.Length;
            if (index > stagePreviewImages.Length)
                index = 0;
        }
    }

    void TakeInput(menuInputState theMenuInputState) {
        inputState = new menuInputState(theMenuInputState);
    }

    public void LoadStageSceneOnSelect() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Dev-Chris-Stage");
    }

    public void LoadCharacterSelect() {
        stageSelectPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }
}