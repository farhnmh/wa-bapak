using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour
{
    public GameObject theCar;
    public Transform playerTransform;
    public float carX, carY, carZ;

    void Update()
    {
        carX = theCar.transform.eulerAngles.x;
        carY = theCar.transform.eulerAngles.y;
        carZ = theCar.transform.eulerAngles.z;
    }

    private void LateUpdate()
    {
        this.transform.position = playerTransform.position;
    }
}
