using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnClickEvents : MonoBehaviour
{
    public TextMeshProUGUI soundsText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.mute)
            soundsText.text = "/";
        else
            soundsText.text = "";
    }

    public void ToggleMute()
    {
        if (GameManager.mute) //if its true
        {
            GameManager.mute = false;
            soundsText.text = "";
        }
        else
        {
            GameManager.mute = true;
            soundsText.text = "/";
        }
    }
  
    
    public void QuitGame()
    {
        //only works when you build your game
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
