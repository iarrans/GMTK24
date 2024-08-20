using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    public int nextScene;

    public ChangeSceneBehaviour changeSceneBehaviour;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeSceneBehaviour.ChangeScene(nextScene);
        }
    }
}
