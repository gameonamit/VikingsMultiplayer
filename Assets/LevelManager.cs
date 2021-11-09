using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class LevelManager : MonoBehaviourPun
{
    public static LevelManager instance;
    public int CurrentRound = 1;
    public bool isPaused = false;

    [SerializeField] GameObject GameWonMenu;
    [SerializeField] TextMeshProUGUI GameWonTxt;

    private void Awake()
    {
        instance = this;
    }

    public void AddRounds()
    {
        CurrentRound++;
    }

    public void UpdateRounds(int rounds)
    {
        CurrentRound = rounds;
    }

    public void GameWon(bool isleft)
    {
        GameWonMenu.SetActive(true);
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (isleft)
        {
            GameWonTxt.text = "Bejohn Ironsite Won.";
        }
        else
        {
            GameWonTxt.text = "King Herald Won.";
        }
    }
}
