using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 物體移動速度
    public float rotateSpeed = 20.0f; // 物體旋轉速度
    public Camera playerCamera; // 玩家相機
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
        // 按上下左右鍵控制物體移動
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(movement.x+movement.z>0.1f && !walkeffect.isPlaying) walkeffect.Play(); else walkeffect.Pause();
        transform.Translate(movement * Time.deltaTime * moveSpeed);
    }

    void RotatePlayer()
    {
        // 滑鼠左右移動控制物體旋轉
        float rotationAmount = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount);
    }

    void ControlCamera()
    {
        // 滑鼠上下移動控制相機視角
        float mouseVertical = -Input.GetAxis("Mouse Y");
        //float mouseX = Input.GetAxis("Mouse X");

        //Vector3 currentRotation = playerCamera.transform.localEulerAngles;
        currentRotation.x += mouseVertical * rotateSpeed * Time.deltaTime;
        //currentRotation.y += mouseX * rotateSpeed * Time.deltaTime;

        // 限制相機的上下角度在90度內
        currentRotation.x = Mathf.Clamp(currentRotation.x, -45f, 45f);

        playerCamera.transform.localEulerAngles = currentRotation;
        //playerCamera.transform.localRotation = Quaternion.Euler(currentRotation.x,0, 0);
    }
}
