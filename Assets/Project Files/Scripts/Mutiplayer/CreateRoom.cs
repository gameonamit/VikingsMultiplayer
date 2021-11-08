using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInput;

    public void CreateNewRoom()
    {
        if (createInput.text != string.Empty)
            PhotonNetwork.CreateRoom(createInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
