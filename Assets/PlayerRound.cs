using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerRound : MonoBehaviourPunCallbacks, IPunObservable
{
    Image[] roundUIGroupOne;
    Image[] roundUIGroupTwo;

    [SerializeField] int RoundsToWinGame = 4;
    public int RoundsWon = 0;

    [SerializeField] Color disabledColor, leftColor, rightColor;

    private void Start()
    {
        roundUIGroupOne = GameObject.FindGameObjectWithTag("LeftRoundUI").GetComponentsInChildren<Image>();
        roundUIGroupTwo = GameObject.FindGameObjectWithTag("RightRoundUI").GetComponentsInChildren<Image>();

        DisableRoundGroup(roundUIGroupOne);
        DisableRoundGroup(roundUIGroupTwo);
        // Disable all round UI on start
    }

    #region Round Increase & UI
    public void IncreaseRounds()
    {
        RoundsWon++;
        if (RoundsWon >= RoundsToWinGame)
        {
            //Game Won
        }

        if (photonView.IsMine == true)
        {
            photonView.RPC("UpdateUI", RpcTarget.All, RoundsWon);
        }
    }

    [PunRPC]
    public void UpdateUI(int rounds)
    {
        if(photonView.IsMine == true)
        {
            UpdateRoundUI(roundUIGroupOne, rounds, leftColor);
        }
        else
        {
            UpdateRoundUI(roundUIGroupTwo, rounds, rightColor);
        }
    }
    #endregion

    private void DisableRoundGroup(Image[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = disabledColor;
        }
    }

    private void UpdateRoundUI(Image[] images, int cRound, Color col)
    {
        for(int i = 0; i < images.Length; i++)
        {
            if (i < cRound)
                images[i].color = col;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(RoundsWon);
        }
        else
        {
            RoundsWon = (int)stream.ReceiveNext();
        }
    }
}
