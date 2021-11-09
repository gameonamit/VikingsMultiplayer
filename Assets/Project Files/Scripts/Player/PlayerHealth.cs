using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks, IPunObservable
{
    public int CurrentHealth = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private RectTransform healthBackBar;

    private Slider globalHealthSlider;
    private RectTransform globalHealthBackBar;

    private void Awake()
    {
        if (photonView.IsMine == false)
        {
            this.gameObject.tag = "OtherPlayer";
            AudioListener audioListener = GetComponent<AudioListener>();
            Destroy(audioListener);
        }
    }

    private void Start()
    {
        if (photonView.IsMine == true)
        {
            healthSlider.gameObject.SetActive(false);
            globalHealthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
            globalHealthBackBar = GameObject.FindGameObjectWithTag("HealthFillBack").GetComponent<RectTransform>();

            globalHealthBackBar.anchorMax = globalHealthSlider.fillRect.anchorMax;
            globalHealthBackBar.position = globalHealthSlider.fillRect.position;

            healthBackBar.anchorMax = healthSlider.fillRect.anchorMax;
            healthBackBar.position = healthSlider.fillRect.position;
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
            photonView.RPC("UpdateHealthUI", RpcTarget.All, CurrentHealth);
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
            photonView.RPC("UpdateHealthUI", RpcTarget.All, CurrentHealth);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = 100;
        if (photonView.IsMine == true)
        {
            photonView.RPC("UpdateHealthUI", RpcTarget.All, CurrentHealth);
        }
    }

    [PunRPC]
    private void UpdateHealthUI(int cHealth)
    {
        healthSlider.value = cHealth;
        StartCoroutine(ResetHealthBackBar());

        if (globalHealthSlider != null)
        {
            globalHealthSlider.value = cHealth;
            StartCoroutine(ResetGlobalHealthBackBar());
        }
    }

    IEnumerator ResetHealthBackBar()
    {
        yield return new WaitForSeconds(1f);
        healthBackBar.anchorMax = healthSlider.fillRect.anchorMax;
        healthBackBar.position = healthSlider.fillRect.position;
    }

    IEnumerator ResetGlobalHealthBackBar()
    {
        yield return new WaitForSeconds(1f);
        globalHealthBackBar.anchorMax = globalHealthSlider.fillRect.anchorMax;
        globalHealthBackBar.position = globalHealthSlider.fillRect.position;
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
