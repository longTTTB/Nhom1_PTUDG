using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI soulText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI maxsoulText;
    public TextMeshProUGUI maxenemyText;
    public int currentsoul = 0;
    public int killEnemy = 0;
    public int soulNeed = 3;
    public int enemyNeed = 6;
    public bool iswin = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateMission();
    }
    public void GetSoul()
    {
        currentsoul++;
        UpdateMission();
    }
    public void KillEnemy()
    {
        killEnemy++;
        UpdateMission();
    }
    public void UpdateMission()
    {
        soulText.text = currentsoul.ToString();
        enemyText.text = killEnemy.ToString();
        maxsoulText.text = soulNeed.ToString();
        maxenemyText.text = enemyNeed.ToString();
    }
    public void Win()
    {
        if (currentsoul >= soulNeed && killEnemy >= enemyNeed)
        {
            iswin = true;
        }
    }
}
