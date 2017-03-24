using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleMenu : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject titleMenuPanel;

    public void LoadMenuPanel()
    {
        titleMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
