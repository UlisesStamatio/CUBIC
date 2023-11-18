using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private Vector3 respawnPosition;
    public GameObject deathEffect;
    public int currentCoins;
    public int currentDeaths;
    public int levelEndMusic = 0;
    public string levelToLoad;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        HealthManager.instance.PlayerKilled();
    }

    //CO Rutina
    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        CameraController.instance.theCMBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        //Tiempo de espera de 2 segundos antes del respawn
        yield return new WaitForSeconds(2f);

        HealthManager.instance.ResetHealth();

        UIManager.instance.fadeFromBlack = true;

        PlayerController.instance.transform.position = respawnPosition;

        CameraController.instance.theCMBrain.enabled = true;

        PlayerController.instance.gameObject.SetActive(true);
    }


    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = currentCoins.ToString();
    }

    public void AddDeath()
    {
        currentDeaths += 1;
        UIManager.instance.deathText.text = currentDeaths.ToString();
    }


    public void PauseUnPause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    public IEnumerator LevelEndCo(){
         AudioManager.instance.PlayMusic(levelEndMusic);
         yield return new WaitForSeconds(4f);

         PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked",1);
         SceneManager.LoadScene(levelToLoad);
    }
}
