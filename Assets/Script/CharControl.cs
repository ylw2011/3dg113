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
        walkeffect = GetComponent<AudioSource>(); //��������
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        MovePlayer(); //���ʥD���A�D�n����L����
        RotatePlayer(); //���k����D���A�D�n�ѷƹ�����
        ControlCamera(); //�W�U��������A�D�n�ѷƹ�����
    }

    void MovePlayer()
    {        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);        
        transform.Translate(movement * Time.deltaTime * moveSpeed); //���ʥD��

        if (Mathf.Abs(moveVertical)+ Mathf.Abs(moveHorizontal) > 0.1f) //�p�G���첾
        {
            animator.SetBool("walk", true);
            if(walkeffect!=null)
            {
                if(!walkeffect.isPlaying) walkeffect.Play(); else walkeffect.Pause(); //���񨫸��n��
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
