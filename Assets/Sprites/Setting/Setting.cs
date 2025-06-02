
using UnityEngine;

public class Setting : MonoBehaviour
{
    [SerializeField] GameObject setting;

    public void SettingPause()
    {
       setting.SetActive(true);
        Time.timeScale = 0;

    }
    public void Quit()
    {
        Application.Quit();
    }
}
