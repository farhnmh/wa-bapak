using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour
{
    public GameObject theCar;
    // Start is called before the first frame update

    public float carX, carY, carZ;
    // Update is called once per frame
    void Update()
    {
        carX = theCar.transform.eulerAngles.x;
        carY = theCar.transform.eulerAngles.y;
        carZ = theCar.transform.eulerAngles.z;
    }
}
