using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string firstlevel, levelselect;
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Continue")){
            continueButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  NewGame(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(firstlevel);
        PlayerPrefs.SetInt("Continue",0);
        PlayerPrefs.SetString("CurrentLevel",firstlevel);
    }

    public void Continue(){
        SceneManager.LoadScene(levelselect);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
