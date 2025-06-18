using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cong : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite buttonOnSprite;
    public GameObject hitboxwin;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        SetButtonSprite();
        GameManager.Instance.Win();
    }
    public void SetButtonSprite()
    {
        if (GameManager.Instance.iswin == true)
        {
            spriteRenderer.sprite = buttonOnSprite;
            hitboxwin.SetActive(true);  
        }
    }
}
