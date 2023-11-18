using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LSLevelEntry : MonoBehaviour
{
    public string levelName, levelToCheck,displayName;
    private bool canLoadLevel,levelUnlocked;
    public GameObject mapPointActive, mapPointInactive;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(levelToCheck+"_unlocked")==1 || levelToCheck==""){
            mapPointActive.SetActive(true);
            mapPointInactive.SetActive(false);
            levelUnlocked = true;
        }else{
            mapPointActive.SetActive(false);
            mapPointInactive.SetActive(true);
            levelUnlocked = false;
        }

        if(PlayerPrefs.GetString("CurrentLevel")==levelName){
            PlayerController.instance.transform.position = transform.position;
            GameManager.instance.SetSpawnPoint(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && canLoadLevel && levelUnlocked){
            StartCoroutine(LevelLoadCo());
        }   
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            canLoadLevel = true;
            LSUIManager.instance.lNamePanel.SetActive(true);
            LSUIManager.instance.lNameText.text = displayName;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            canLoadLevel = false;
            LSUIManager.instance.lNamePanel.SetActive(false);
        }
    }

    public IEnumerator LevelLoadCo(){
        UIManager.instance.fadeToBlack = true;
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel",levelName);
    }
}
