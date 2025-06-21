using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENUHOME : MonoBehaviour
{
    public void NewGAme()
    {
        SceneManager.LoadScene("Lv1");
    }
    public void Level()
    {
        SceneManager.LoadScene("ChonLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
