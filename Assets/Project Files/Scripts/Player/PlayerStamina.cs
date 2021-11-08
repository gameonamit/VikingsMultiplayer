using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerStamina : MonoBehaviourPunCallbacks, IPunObservable
{
    public float CurrentStamina = 100;
    [SerializeField] private float autoFillSpeed = 5f;
    [SerializeField] private Slider staminaSlider;
    private Slider globalStaminaSlider;

    private void Start()
    {
        if (photonView.IsMine == true)
        {
            staminaSlider.gameObject.SetActive(false);
            globalStaminaSlider = GameObject.FindGameObjectWithTag("StaminaSlider").GetComponent<Slider>();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine == true)
        {
            if (CurrentStamina < 100)
            {
                CurrentStamina += autoFillSpeed * Time.deltaTime;
                if (CurrentStamina >= 100) CurrentStamina = 100;

                photonView.RPC("UpdateUI", RpcTarget.All, CurrentStamina);
            }
        }
    }

    public bool HasEnoughStamina(int staminaCost)
    {
        if (staminaCost < CurrentStamina)
            return true;
        else
            return false;
    }

    public void IncreaseStamina(int value)
    {
        CurrentStamina += value;
        if (CurrentStamina >= 100)
        {
            CurrentStamina = 100;
        }

        if (photonView.IsMine == true)
        {
            photonView.RPC("UpdateUI", RpcTarget.All, CurrentStamina);
        }
    }

    public void DecreaseStamina(int value)
    {
        CurrentStamina -= value;
        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0;
        }

        if (photonView.IsMine == true)
        {
            photonView.RPC("UpdateUI", RpcTarget.All, CurrentStamina);
        }
    }

    [PunRPC]
    public void UpdateUI(float cStamina)
    {
        staminaSlider.value = cStamina;
        if (globalStaminaSlider != null)
        {
            globalStaminaSlider.value = cStamina;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentStamina);
        }
        else
        {
            CurrentStamina = (float)stream.ReceiveNext();
        }
    }
}
