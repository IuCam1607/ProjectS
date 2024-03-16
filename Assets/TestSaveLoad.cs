using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveLoad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            PlayerManager player = FindAnyObjectByType<PlayerManager>();

            string sceneName = ES3.Load<string>("PlayerScene");
            Vector3 playerPosition = ES3.Load<Vector3>("PlayerPosition");
            LoadingScreenManager.instance.LoadScene(sceneName);
            player.transform.position = playerPosition;
            Debug.Log("Loading Scene: " + sceneName);
        }
    }
}
