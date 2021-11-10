using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject LobbyMenu;
    [SerializeField] GameObject mainMenu;

    public void PlayGame()
    {
        LobbyMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
