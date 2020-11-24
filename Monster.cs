using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

//NOTE SPPED-EK VÁLTOZTATÁSA MAP MÉRETTŐL FÜGGŐEN
public class Monster : MonoBehaviour
{
    public GameObject destination;

    UnityEngine.AI.NavMeshAgent agent;

    int xPos;
    int zPos;

    int next;

    public static System.Random ran;

    Animator anim;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ran = new System.Random();
        next = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (next == 0)
        {
            //TODO THIS WILL BE THE PLAYER'S COORDINATES
            agent.SetDestination(destination.transform.position);
        }
        else
        {
            //RANDOM COORDINATES ON MAP
            agent.SetDestination(destination.transform.position);
            next -= 1;
        }

        Vector3 dir = destination.transform.position - transform.position;

        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir);

        rot = Quaternion.AngleAxis(+90, Vector3.up) * rot;

        float rotationspeed = 1f;
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationspeed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        //NOTE COLLIDER AND LIGHT REACH MUST HAVE THE SAME RADIUS/SIZE
        if (other.GetComponent<Light>() != null)
        {
            if (other.GetComponent<Light>().isActiveAndEnabled)
            {
               // Debug.Log("Collision with Light");

                xPos = Random.Range(-58, 30);
                zPos = Random.Range(-51, 37);

                agent.speed = 36;

                agent.acceleration = 36;

                anim.SetTrigger("Fast");

                //TODO MONSTER SCREAM.PLAY

                next = ran.Next(1, 5);

                return;
            }
        }
        
        if(other.gameObject == player)
        {
            anim.SetTrigger("Attack");
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (agent.speed == 36)
        {
            anim.SetTrigger("Slow");
            agent.speed = 12;
            agent.acceleration = 12;

            //MESH COLLIDER REENABLE
        }
    }
}
