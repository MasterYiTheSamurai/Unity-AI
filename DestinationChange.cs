using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DestinationChange : MonoBehaviour
{

     int xPos;
     int zPos;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            //Debug.Log("Collision");

            xPos = Random.Range(-58, 30);
            zPos = Random.Range(-51, 37);
            this.gameObject.transform.position = new Vector3(xPos, -2.3f, zPos);
        }

        if (other.GetComponent<Light>() != null)
        {
            if (other.GetComponent<Light>().isActiveAndEnabled)
            {
                xPos = Random.Range(-58, 30);
                zPos = Random.Range(-51, 37);
                this.gameObject.transform.position = new Vector3(xPos, -2.3f, zPos);
            }
        }
    }
}
