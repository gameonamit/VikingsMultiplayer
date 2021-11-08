using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks, IPunObservable
{
    public int CurrentHealth = 100;
    [SerializeField] private Slider healthSlider;
    private Slider globalHealthSlider;

    private void Start()
    {
        if (photonView.IsMine == true)
        {
            healthSlider.gameObject.SetActive(false);
            globalHealthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        }
    }

    public void IncreaseHealth(int value)
    {
        CurrentHealth += value;
        if (CurrentHealth >= 100)
        {
            CurrentHealth = 100;
        }
        if (photonView.IsMine == true)
        {
            photonView.RPC("UpdateUI", RpcTarget.All, CurrentHealth);
        }
    }

    public void DecreaseHealth(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }
        if (photonView.IsMine == true)
        {
            photonView.RPC("UpdateUI", RpcTarget.All, CurrentHealth);
        }
    }

    [PunRPC]
    private void UpdateUI(int cHealth)
    {
        healthSlider.value = cHealth;
        if (globalHealthSlider != null)
            globalHealthSlider.value = cHealth;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentHealth);
        }
        else
        {
            CurrentHealth = (int)stream.ReceiveNext();
        }
    }
}
