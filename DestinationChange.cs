using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DestinationChange : MonoBehaviour
{

    public int xPos_MIN;
    public int xPos_MAX;

    public int zPos_MIN;
    public int zPos_MAX;

    int xPos;
    int zPos;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            //Debug.Log("Collision");
            Monster.next -= 1;
            Debug.Log("next value decremented");

            xPos = Random.Range(xPos_MIN, xPos_MAX);
            zPos = Random.Range(zPos_MIN, zPos_MAX);
            this.gameObject.transform.position = new Vector3(xPos, 2.3f, zPos);
        }

        if (other.GetComponent<Light>() != null)
        {
            if (other.GetComponent<Light>().isActiveAndEnabled)
            {
                xPos = Random.Range(xPos_MIN, xPos_MAX);
                zPos = Random.Range(zPos_MIN, zPos_MAX);
                this.gameObject.transform.position = new Vector3(xPos, 2.3f, zPos);
            }
        }
    }
}
