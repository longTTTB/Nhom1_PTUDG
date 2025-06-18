using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitboxwin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = currentScene + 1;
            if (nextScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextScene);
            }
            else { Debug.Log("Het"); }
        }
    }
}
