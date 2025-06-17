using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject NewGameObject;

    public void OnPauseMenu()
    {
        NewGameObject.SetActive(true);
    }
}
