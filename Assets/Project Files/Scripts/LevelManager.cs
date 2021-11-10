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
    [SerializeField] TextMeshProUGUI InfoTxt;

    private CameraController cameraController;

    private void Awake()
    {
        instance = this;
        cameraController = FindObjectOfType<CameraController>();
    }

    public void AddRounds()
    {
        CurrentRound++;
    }

    public void UpdateRounds(int rounds)
    {
        CurrentRound = rounds;
    }

    public void GameWon(bool isleft, Vector3 camPositon, Quaternion camRotation)
    {
        GameWonMenu.SetActive(true);
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (isleft)
        {
            GameWonTxt.text = "Bejohn Ironsite Won.";
            InfoTxt.text = "He was a Norse Viking chief and legendary king of sweden who fought bravely for his country and brought honor to his country.";
        }
        else
        {
            GameWonTxt.text = "King Herald Won.";
            InfoTxt.text = "His victory and success in unifying Norway earned Harald the achievement of becoming the first King of Norway and receiving the approval of Princess Gyda in marriage.";
        }

        cameraController.AnimateToNewPosition(camPositon, camRotation);
    }
}
