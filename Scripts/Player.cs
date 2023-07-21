using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    public GameObject animObj;
    Animator anim;

    public float speed = 3.0f;
    public float rotSpeed = 10f;

    float dirX;
    bool wKeyDown;


    public float rotCamXAxisSpeed = 3f; // ī�޶� x�� ȸ���ӵ�
    public float rotCamYAxisSpeed = 1f; // ī�޶� y�� ȸ���ӵ�

    public float limitMinX = -15; // ī�޶� x�� ȸ�� ���� (�ּ�)
    public float limitMaxX = 15; // ī�޶� x�� ȸ�� ���� (�ִ�)

    private float eulerAngleX; // ���콺 �� / �� �̵����� ī�޶� y�� ȸ��
    private float eulerAngleY; // ���콺 �� / �Ʒ� �̵����� ī�޶� x�� ȸ��

    void Awake()
    {
        anim = animObj.GetComponent<Animator>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        CalculateRotation(mouseX, mouseY);
        Move();
    }

    void Move()
    {
        dirX = Input.GetAxisRaw("Vertical");
        wKeyDown = Input.GetButton("Walk");

        var dir = cam.transform.forward;
        dir.y = 0;

        //transform.LookAt(transform.position + dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);


        Vector3 move = transform.forward * dirX /*+ transform.right * Input.GetAxisRaw("Horizontal")*/;

        transform.position += move * speed * (wKeyDown ? 2f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", wKeyDown);
        anim.SetBool("isWalk", move != Vector3.zero);
    }

    public void CalculateRotation(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        eulerAngleX -= mouseY * rotCamYAxisSpeed;
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        cam.transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) { angle += 360; }
        if (angle > 360) { angle -= 360; }

        return Mathf.Clamp(angle, min, max);
    }
}
