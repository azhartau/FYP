using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Controller : MonoBehaviour
{
    public GameObject menu_panel;
    public GameObject exit_panel;
    void Start()
    {
        exit_panel.gameObject.SetActive(false);
        menu_panel.gameObject.SetActive(true);
    }
    public void exit()
    {
        exit_panel.gameObject.SetActive(true);
        menu_panel.gameObject.SetActive(false);
    }
    public void play()
    {
        SceneManager.LoadScene("level");
    } 
    public void pp()
    {
        Application.OpenURL("");
    }
    public void more_game()
    {
        Application.OpenURL("");
    }
    public void rate_us()
    {
        Application.OpenURL("");
    }
    public void exit_yes()
    {
        Application.Quit();
    }
    public void exit_no()
    {
        menu_panel.gameObject.SetActive(true);
        exit_panel.gameObject.SetActive(false);
    }

}
