using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public GameObject destination;

    UnityEngine.AI.NavMeshAgent agent;

    public int xPos_MIN;
    public int xPos_MAX;

    public int zPos_MIN;
    public int zPos_MAX;

    public static int next;

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
            // PLAYER'S COORDINATES
            agent.SetDestination(player.transform.position);

            Vector3 dir = player.transform.position - transform.position;

            dir.y = 0;

            Quaternion rot = Quaternion.LookRotation(dir);

            rot = Quaternion.AngleAxis(+90, Vector3.up) * rot;

            float rotationspeed = 1f;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationspeed * Time.deltaTime);
        }
        else
        {
            //RANDOM COORDINATES ON MAP
            Debug.Log("next: " + next);
            Debug.Log("ENTERED TO RANDOM DEST");
            agent.SetDestination(destination.transform.position);
            // next -= 1;

            anim.SetTrigger("Slow");


            Vector3 dir = destination.transform.position - transform.position;

            dir.y = 0;

            Quaternion rot = Quaternion.LookRotation(dir);

            rot = Quaternion.AngleAxis(+90, Vector3.up) * rot;

            float rotationspeed = 1f;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationspeed * Time.deltaTime);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //NOTE COLLIDER AND LIGHT REACH MUST HAVE THE SAME RADIUS/SIZE
        if (other.GetComponent<Light>() != null)
        {
            if (other.GetComponent<Light>().isActiveAndEnabled && next == 0)
            {
                // Debug.Log("Collision with Light");

                 

                Debug.Log("LIGHT COLLISION");

                agent.speed = 36;

                agent.acceleration = 36;

                anim.SetTrigger("Fast");

                agent.SetDestination(destination.transform.position);

                //TODO MONSTER SCREAM.PLAY

                next = ran.Next(1, 5);

                return;
            }
        }

        if (other.gameObject == player && next == 0)
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
