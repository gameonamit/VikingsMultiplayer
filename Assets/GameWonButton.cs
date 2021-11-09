using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameWonButton : MonoBehaviourPunCallbacks
{
    public void OnMenuButtonClick()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Loading");
    }
}
