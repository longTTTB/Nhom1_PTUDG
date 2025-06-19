using UnityEngine;
using UnityEngine.UI;

public class ChangeImageButton : MonoBehaviour
{
    public Sprite offImage;
    public Sprite onImage;
    public Button button;
    private bool isOn = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeButtonImage()
    {
        if(isOn)
        {
            button.image.sprite = offImage;
            isOn = false;
        }
        else
        {
            button.image.sprite = onImage;
            isOn=true;
        }
        
    }
}
