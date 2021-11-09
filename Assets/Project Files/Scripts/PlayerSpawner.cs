using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;

    [SerializeField] GameObject PlayerPrefab;

    [SerializeField] Transform spawnPointOne;
    [SerializeField] Transform spawnPointTwo;

    private void Start()
    {
        StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject[] players = GameObject.FindGameObjectsWithTag("OtherPlayer");
        GameObject player;
        if (players.Length <= 0)
        {
            player = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPointOne.position, Quaternion.identity);
            cameraController.transform.position = spawnPointOne.position;
        }
        else
        {
            player = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPointTwo.position, Quaternion.identity);
            cameraController.transform.position = spawnPointTwo.position;
        }
        UpdateCameraTarget(player);
    }

    private void UpdateCameraTarget(GameObject player)
    {
        cameraController.target = player.transform.GetChild(0).transform;
    }
}
