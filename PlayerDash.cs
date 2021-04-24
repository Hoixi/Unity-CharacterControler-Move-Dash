using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{

    public Vector3 moveDirection;

    public const float maxDashTime = 1.0f;
    public float dashDistance = 1;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 6;
    CharacterController controller;
    public bool dash = false;
    public int enemy = 1;
    public GameObject final;
    public Text eT;
    public Text dashText;
    public float playerSpeed = 20;


    Vector3 initialMousePos;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    


    float zDelta = 0;
    float xDelta = 0;
    

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentDashTime);
        if (Input.GetMouseButtonDown(0))
        {           
            initialMousePos = Input.mousePosition;
            dashDistance = 2;
        }

        if (Input.GetMouseButton(0))
        {
            if (dashDistance <= 10)
            {
                
                dashDistance = dashDistance + 3 * Time.deltaTime;
                dashText.text = dashDistance.ToString("0");
            }
            Vector3 touchDelta = initialMousePos - Input.mousePosition;
            float input = touchDelta.x / Screen.width;
            float inputz = touchDelta.y / Screen.width;
            xDelta = -input * 10 * Time.deltaTime;
            zDelta = -inputz * 10 * Time.deltaTime;
            Vector3 direc = new Vector3(-input, 0, -inputz);
            controller.Move(direc * playerSpeed * Time.deltaTime);
        }
            if (Input.GetMouseButtonUp(0))
            {
                dash = true;
                currentDashTime = 0;
                dashText.text = "2";
            }
            if (currentDashTime < maxDashTime)
            {
                moveDirection = transform.forward * dashDistance;
                currentDashTime += dashStoppingSpeed;

            }
            else
            {
                moveDirection = Vector3.zero;
            }
            controller.Move(moveDirection * Time.deltaTime * dashSpeed);

            if (currentDashTime > 1 && currentDashTime < 0)
            {
                dash = false;
            }

            if (enemy == 0) {
                final.active = true;
            }

            if (xDelta != 0 && zDelta != 0)
            {
                transform.forward = new Vector3(xDelta, 0, zDelta);
            }
    }


        private void OnTriggerEnter(Collider other)
        {
            if (dash == true)
            {
                Destroy(other.gameObject);
                enemy = enemy - 1;
                eT.text = (0 + 1) + "/1";
                dash = false;
            }


        }
    }
