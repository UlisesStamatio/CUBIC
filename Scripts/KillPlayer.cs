using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            AudioManager.instance.PlaySFX(11);
            GameManager.instance.AddDeath();
            GameManager.instance.Respawn();
        }
    }
}
