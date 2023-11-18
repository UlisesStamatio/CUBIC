using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{

    public int healAmount;
    public bool isFullHealth;
    public GameObject pickUpEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Destroy(gameObject);
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(7);
            if (isFullHealth)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                //HealthManager.instance.AddHealth(healAmount);
                HealthManager.instance.ResetHealth();
            }
        }
    }
}
