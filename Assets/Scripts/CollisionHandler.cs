using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Per Second")][SerializeField] float LoadLevelDelay;
    [Tooltip("Explosion Prefab")][SerializeField] GameObject explosionFX;
    void OnTriggerEnter(Collider collider)
    {
        print("Hit Trigger");
        StartDeath();
        explosionFX.SetActive(true);
        Invoke("RestartLevel", LoadLevelDelay);
    }

    void StartDeath()
    {
        SendMessage("OnPlayerDeath");   
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
