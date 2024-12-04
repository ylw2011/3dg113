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
    Animator animator;
    void Start()
    {
        walkeffect = GetComponent<AudioSource>(); //走路音效
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        MovePlayer(); //移動主角，主要由鍵盤控制
        RotatePlayer(); //左右旋轉主角，主要由滑鼠控制
        ControlCamera(); //上下旋轉視角，主要由滑鼠控制
    }

    void MovePlayer()
    {        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);        
        transform.Translate(movement * Time.deltaTime * moveSpeed); //移動主角

        if (Mathf.Abs(moveVertical)+ Mathf.Abs(moveHorizontal) > 0.1f) //如果有位移
        {
            animator.SetBool("walk", true);
            if(walkeffect!=null)
            {
                if(!walkeffect.isPlaying) walkeffect.Play(); else walkeffect.Pause(); //播放走路聲音
            }
            //animator.SetFloat("forward", moveVertical);
            //animator.SetFloat("leftright", moveHorizontal);            
        }
        else animator.SetBool("walk", false);
        if (Input.GetButtonDown("Fire1")) animator.SetTrigger("attack1");
        if (Input.GetButtonDown("Fire2")) animator.SetTrigger("attack2");
    }

    void RotatePlayer()
    {
        float rotationAmount = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount);
    }

    void ControlCamera()
    {
        float mouseVertical = -Input.GetAxis("Mouse Y");
        
        currentRotation.x += mouseVertical * rotateSpeed * Time.deltaTime;        
        currentRotation.x = Mathf.Clamp(currentRotation.x, -45f, 45f);

        playerCamera.transform.localEulerAngles = currentRotation;        
    }
}
