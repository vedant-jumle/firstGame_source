using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    private AudioManager audio;
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float horizontalMove = 0f;
    public bool jump = false;
    
    void start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void GetMovementDetails()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        jump = Input.GetButtonDown("Jump");
    }
    void FixedUpdate()
    {
        GetMovementDetails();
        if(horizontalMove == 0)
        {
            controller.Move(0,false,jump);
        }
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime,false,jump);
        }
    }
}
