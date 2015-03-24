using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Transform playerCam;

    private int cameraState;
    int camVertical = 0,
        camHorizontal = 1;

    private float targetRotationAmount = 90f;
    private float currentRotAmount = 0f;
    private float cameraSpeed = 50f;

    // Use this for initialization
    void Start()
    {
        cameraState = camVertical;

        playerCam = transform.GetChild(0);
        Debug.Log(playerCam.name);
    }

    // Update is called once per frame
    void Update()
    {
        playerCam.transform.LookAt(transform);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.Translate(x, 0.0f, z);

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine("CameraRotate");
        }
    }

    #region CameraRotate
    IEnumerator CameraRotate()
    {
        if (cameraState == camVertical)
        {
            while (currentRotAmount < targetRotationAmount)
            {
                float rotAmount = 1.0f * Time.deltaTime * cameraSpeed;
                playerCam.transform.RotateAround(transform.position, Vector3.right, -rotAmount);
                currentRotAmount += rotAmount;

                if (currentRotAmount > targetRotationAmount)
                {
                    currentRotAmount = 0f;
                    cameraState = camHorizontal;
                    StopCoroutine("CameraRotate");
                }

                yield return null;
            }
        }

        if (cameraState == camHorizontal)
        {
            while (currentRotAmount < targetRotationAmount)
            {
                float rotAmount = 1.0f * Time.deltaTime * cameraSpeed;
                playerCam.transform.RotateAround(transform.position, Vector3.right, rotAmount);
                currentRotAmount += rotAmount;

                if (currentRotAmount > targetRotationAmount)
                {
                    currentRotAmount = 0f;
                    cameraState = camHorizontal;
                    StopCoroutine("CameraRotate");
                }

                yield return null;
            }
        }
    }
    #endregion
}
