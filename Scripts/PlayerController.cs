using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public float rotateSpeed;
    private Vector3 moveDirection;

    public CharacterController characterController;
    private Camera theCam;
    public GameObject playerModel;

    public Animator anim;

    public bool isKocking;
    public float knockBackLength = 0.5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;
    public GameObject[] playerPieces;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKocking)
        {

            float yStore = moveDirection.y;
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (characterController.isGrounded)
            {
                moveDirection.y = -1f;
                if (Input.GetButtonDown("Jump") && jumpForce > 0)
                {
                    AudioManager.instance.PlaySFX(12);
                    moveDirection.y = jumpForce;
                }
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            //transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed);
            characterController.Move(moveDirection * Time.deltaTime);
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRoration = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                //playerModel.transform.rotation = newRoration;
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRoration, rotateSpeed * Time.deltaTime);

            }
        }
        else
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            if (characterController.isGrounded)
            { 
                moveDirection.y = -1f;
            } 

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            characterController.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                isKocking = false;
            }
        }

        
        anim.SetFloat("RunSpeed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", characterController.isGrounded);
    }

    public void Knockback()
    {
        isKocking = true;
        knockbackCounter = knockBackLength;
        moveDirection.y = knockbackPower.y;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
