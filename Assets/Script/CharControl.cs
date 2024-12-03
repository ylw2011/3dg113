using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public float moveSpeed = 5.0f; 
    public float rotateSpeed = 20.0f;
    public Camera playerCamera;
    Vector3 currentRotation;
    AudioSource walkeffect;
    Animator an;
    void Start()
    {
        walkeffect = GetComponent<AudioSource>();
        an = GetComponent<Animator>();

    }
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        ControlCamera();
    }

    void MovePlayer()
    {        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(Mathf.Abs(movement.x+movement.z)>0.1f && !walkeffect.isPlaying) walkeffect.Play(); else walkeffect.Pause();        
        transform.Translate(movement * Time.deltaTime * moveSpeed);

        if (Mathf.Abs(moveVertical)+ Mathf.Abs(moveHorizontal) > 0.1f)
        {
            an.SetBool("walk", true);
            an.SetFloat("forward", moveVertical);
            an.SetFloat("leftright", moveHorizontal);            
        }
        else an.SetBool("walk", false);
        if (Input.GetButtonDown("Fire1")) an.SetTrigger("attack1");
        if (Input.GetButtonDown("Fire2")) an.SetTrigger("attack2");
    }

    void RotatePlayer()
    {
        float rotationAmount = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount);
    }

    void ControlCamera()
    {
        float mouseVertical = -Input.GetAxis("Mouse Y");
        //float mouseX = Input.GetAxis("Mouse X");

        //Vector3 currentRotation = playerCamera.transform.localEulerAngles;
        currentRotation.x += mouseVertical * rotateSpeed * Time.deltaTime;
        //currentRotation.y += mouseX * rotateSpeed * Time.deltaTime;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -45f, 45f);

        playerCamera.transform.localEulerAngles = currentRotation;
        //playerCamera.transform.localRotation = Quaternion.Euler(currentRotation.x,0, 0);
    }
}
