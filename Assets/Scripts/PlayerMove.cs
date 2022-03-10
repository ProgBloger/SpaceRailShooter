using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    [Header("General")]
    // Start is called before the first frame update
    [Tooltip("mps")][SerializeField] float Speed = 15f;
    [SerializeField] float XClamp = 15f;
    [SerializeField] float YClamp = 15f;
    [SerializeField] GameObject[] lasers;
    [Header("RotationFactor")]
    [SerializeField] float xRotFactor = -5;
    [SerializeField] float yRotFactor = 5;
    [SerializeField] float zRotFactor = 4;
    [Header("RotationMove")]
    [SerializeField] float xMoveRotation = -15f;
    [SerializeField] float yMoveRotation = 15f;
    [SerializeField] float zMoveRotation = 15f;
    bool controlEnabled = true;
    float horizontalMove;
    float verticalMove;

    void Start()
    {
        
    }

    void OnPlayerDeath()
    {
        print("Control Off");
        controlEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlEnabled)
        {
            MoveShip();
            RotateShip();
            FireLasers();
        }
    }

    void MoveShip()
    {
        horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = horizontalMove * Speed * Time.deltaTime;
        float newXPosition = transform.localPosition.x + xOffset;
        float clampXPos = Mathf.Clamp(newXPosition, -XClamp, XClamp);
                        
        verticalMove = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = verticalMove * Speed * Time.deltaTime;
        float newYPosition = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(newYPosition, -YClamp, YClamp);
        
        transform.localPosition = new Vector3(
                        clampXPos,
                        clampYPos,
                        transform.localPosition.z);
    }

    void RotateShip()
    {
        float xRot = transform.localRotation.y * xRotFactor + verticalMove * xMoveRotation;
        float yRot = transform.localRotation.x * yRotFactor + horizontalMove * yMoveRotation;
        float zRot = transform.localRotation.x * yRotFactor + horizontalMove * zMoveRotation;
        var newQuat = Quaternion.Euler(xRot, yRot , zRot);
        transform.localRotation = newQuat;
    }

    void FireLasers()
    {
        var enableGuns = CrossPlatformInputManager.GetButton("Fire");
        ActivateLasers(enableGuns);
    }

    void ActivateLasers(bool status)
    {
            foreach(var laser in lasers)
            {
                laser.SetActive(status);
            }
    }
}
