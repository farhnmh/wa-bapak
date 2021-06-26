using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tax Office")
        {
            GameManager.instance.RandomHouse();
        }

        if (collision.gameObject.tag == "House")
        {
            var house = collision.gameObject.GetComponent<HouseManager>();
            if(house.onGoing && !house.penyidikan)
            {
                house.onGoing = false;
                house.penyidikan = true;
                MissionManager.instance.Penyidikan();
            }
            
        }
    }
}
