using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void GameButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
