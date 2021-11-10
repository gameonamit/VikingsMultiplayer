using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class FPSAndPingUpdater : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI pingTxt;
    [SerializeField] TextMeshProUGUI fpsTxt;

    private void Update()
    {
        pingTxt.text = "Ping " + PhotonNetwork.GetPing().ToString();
        fpsTxt.text = (Time.frameCount / Time.time).ToString("F2");
    }
}
