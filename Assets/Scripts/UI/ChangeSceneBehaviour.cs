using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneBehaviour : MonoBehaviour
{
    public Image image;
    public float animSpeed = 1.5f;
    public float timeToChange = 0.5f;
    private int sceneToChange;
    private bool onChangeScene = false;
    private bool onLoadScene = false;

    private void Start()
    {
        Color color = image.color;
        color.a = 1;
        image.color = color;
        StartCoroutine(LoadSceneCoroutine());
    }

    void Update()
    {
        if (onChangeScene)
        {
            if (image.color.a == 1)
            {
                StartCoroutine(ChangeSceneCoroutine());
                onChangeScene = false;
            }
            Color nextColor = image.color;
            nextColor.a += Time.deltaTime * animSpeed;
            if (nextColor.a > 1) nextColor.a = 1;
            image.color = nextColor;
        } else if (onLoadScene)
        {
            if (image.color.a == 0)
            {                
                gameObject.SetActive(false);
                onLoadScene = false;
            }
            Color nextColor = image.color;
            nextColor.a -= Time.deltaTime * animSpeed;
            if (nextColor.a < 0) nextColor.a = 0;
            image.color = nextColor;
        }
    }

    public void ChangeScene(int scene)
    {
        gameObject.SetActive(true);
        AudioManager.instance.PlayDoorClip();
        onChangeScene = true;
        sceneToChange = scene;
        EventSystem.current.enabled = false;
        Color color = image.color;
        color.a = 0;
        image.color = color;
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(timeToChange);
        SceneManager.LoadScene(sceneToChange);
    }

    private IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(timeToChange);
        onLoadScene = true;
    }
}
