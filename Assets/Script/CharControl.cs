using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public float moveSpeed = 5.0f; // ���鲾�ʳt��
    public float rotateSpeed = 20.0f; // �������t��
    public Camera playerCamera; // ���a�۾�
    Vector3 currentRotation;
    AudioSource walkeffect;
    void Start()
    {
        walkeffect = GetComponent<AudioSource>();
    }
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        ControlCamera();
    }

    void MovePlayer()
    {
        // ���W�U���k�䱱��鲾��
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(movement.x+movement.z>0.1f && !walkeffect.isPlaying) walkeffect.Play(); else walkeffect.Pause();
        transform.Translate(movement * Time.deltaTime * moveSpeed);
    }

    void RotatePlayer()
    {
        // �ƹ����k���ʱ�������
        float rotationAmount = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount);
    }

    void ControlCamera()
    {
        // �ƹ��W�U���ʱ���۾�����
        float mouseVertical = -Input.GetAxis("Mouse Y");
        //float mouseX = Input.GetAxis("Mouse X");

        //Vector3 currentRotation = playerCamera.transform.localEulerAngles;
        currentRotation.x += mouseVertical * rotateSpeed * Time.deltaTime;
        //currentRotation.y += mouseX * rotateSpeed * Time.deltaTime;

        // ����۾����W�U���צb90�פ�
        currentRotation.x = Mathf.Clamp(currentRotation.x, -45f, 45f);

        playerCamera.transform.localEulerAngles = currentRotation;
        //playerCamera.transform.localRotation = Quaternion.Euler(currentRotation.x,0, 0);
    }
}
