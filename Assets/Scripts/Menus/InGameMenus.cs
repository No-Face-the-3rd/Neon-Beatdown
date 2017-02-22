using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour
{
    public void LoadSceneOnSelect()
    {
        SceneManager.LoadScene("Dev-Chris-Menus");
    }
}
