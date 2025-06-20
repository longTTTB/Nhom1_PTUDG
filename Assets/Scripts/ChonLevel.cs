using UnityEngine;
using UnityEngine.SceneManagement;


public class ChonLevel : MonoBehaviour
{
    public void ChonLv1()
    {
        SceneManager.LoadScene("Lv3");
    }
    public void ChonLv2()
    {
        SceneManager.LoadScene("Lv2");
    }
    public void ChonLv3()
    {
        SceneManager.LoadScene("Lv1");
    }
    public void ChonLv4()
    {
        SceneManager.LoadScene("Boss");
    }
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}
