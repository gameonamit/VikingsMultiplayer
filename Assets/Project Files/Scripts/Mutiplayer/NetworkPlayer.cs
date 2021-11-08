using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    [SerializeField] private float smoothing = 20.0f;

    private void Awake()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            tag = "OtherPlayer";
            AudioListener audioListener = GetComponent<AudioListener>();
            Destroy(audioListener);
        }
        networkPosition = transform.position;
        networkRotation = transform.rotation;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * smoothing);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
