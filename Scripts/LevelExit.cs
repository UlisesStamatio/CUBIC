using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{

    public Animator anim;
    private bool end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player" && end == false){
            anim.SetTrigger("Hit");
            end = true;
            StartCoroutine(GameManager.instance.LevelEndCo());
        }
    }
}
