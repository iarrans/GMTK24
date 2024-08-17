using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    public int nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("LevelFinished");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(nextScene);
        }
    }
}
