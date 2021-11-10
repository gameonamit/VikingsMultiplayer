using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerRound : MonoBehaviourPunCallbacks, IPunObservable
{
    LevelManager levelManager;
    Animator anim;

    Image[] roundUIGroupOne;
    Image[] roundUIGroupTwo;

    TextMeshProUGUI roundTxt;

    [SerializeField] int RoundsToWinGame = 4;
    public int RoundsWon = 0;


    [SerializeField] Color disabledColor, leftColor, rightColor;

    public bool isLeft = true;

    [SerializeField] GameObject BjornSkin, KindHeraldSkin;
    [SerializeField] Avatar BjornAvatar, KindHeraldAvatar;

    [SerializeField] public Transform cinematicCameraTranform;

    private void Awake()
    {
        roundUIGroupOne = GameObject.FindGameObjectWithTag("LeftRoundUI").GetComponentsInChildren<Image>();
        roundUIGroupTwo = GameObject.FindGameObjectWithTag("RightRoundUI").GetComponentsInChildren<Image>();
        roundTxt = GameObject.FindGameObjectWithTag("RoundTxt").GetComponent<TextMeshProUGUI>();
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        DisableRoundGroup(roundUIGroupOne);
        DisableRoundGroup(roundUIGroupTwo);
        // Disable all round UI on start

        if (this.gameObject.CompareTag("Player"))
        {
            GameObject otherPlayer = GameObject.FindGameObjectWithTag("OtherPlayer");
            if(otherPlayer == null)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
        }
        else
        {
            GameObject otherPlayer = GameObject.FindGameObjectWithTag("Player");
            if (otherPlayer == null)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
        }

        if (isLeft)
        {
            BjornSkin.SetActive(true);
            KindHeraldSkin.SetActive(false);
            anim.avatar = BjornAvatar;
        }
        else
        {
            BjornSkin.SetActive(false);
            KindHeraldSkin.SetActive(true);
            anim.avatar = KindHeraldAvatar;
        }
    }

    #region Round Increase & UI
    public void IncreaseRounds()
    {
        RoundsWon++;
        levelManager.AddRounds();
        if (RoundsWon >= RoundsToWinGame)
        {
            //Game Won
            photonView.RPC("GameWonRPC", RpcTarget.All, isLeft);
        }

        photonView.RPC("UpdateRoundsUI", RpcTarget.All, RoundsWon);
        photonView.RPC("UpdateRoundsTxt", RpcTarget.All, levelManager.CurrentRound);
    }

    [PunRPC]
    public void GameWonRPC(bool isLeft)
    {
        Transform otherPlayer;
        Transform cinematicCamera;
        if (isLeft)
        {
            otherPlayer = this.transform;
            cinematicCamera = this.cinematicCameraTranform;
        }
        else
        {
            if (this.CompareTag("Player"))
            {
                otherPlayer = GameObject.FindGameObjectWithTag("OtherPlayer").transform;
                cinematicCamera = otherPlayer.GetComponent<PlayerRound>().cinematicCameraTranform;
            }
            else
            {
                otherPlayer = GameObject.FindGameObjectWithTag("Player").transform;
                cinematicCamera = otherPlayer.GetComponent<PlayerRound>().cinematicCameraTranform;
            }
        }
        Vector3 position = otherPlayer.localPosition + cinematicCamera.localPosition;
        Quaternion rotation = Quaternion.Euler(otherPlayer.eulerAngles + cinematicCamera.localRotation.eulerAngles);
        levelManager.GameWon(isLeft, position, rotation);
    }

    [PunRPC]
    public void UpdateRoundsUI(int rounds)
    {
        if (isLeft == true)
        {
            UpdateRoundUI(roundUIGroupOne, rounds, leftColor);
        }
        else
        {
            UpdateRoundUI(roundUIGroupTwo, rounds, rightColor);
        }
    }

    [PunRPC]
    public void UpdateRoundsTxt(int rounds)
    {
        levelManager.UpdateRounds(rounds);
        roundTxt.text = levelManager.CurrentRound.ToString();
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
            {
                images[i].color = col;
            }
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
