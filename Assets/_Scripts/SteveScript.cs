using UnityEngine;
using System.Collections;

//https://www.youtube.com/watch?v=s67AYDD3j1E

public class SteveScript : MonoBehaviour {

    public int attackDistance = 2;
    private float distance;
    public GameObject[] waypoints;
    private int waypointInd = 0;
    private GameObject target;
    private NavMeshAgent agent;
    private bool chasing = false;
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 2.5f;
    private Animator animator;

    void Start()
    {
        target = waypoints[waypointInd];
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!chasing)
        {
            Patrolling();
        }
        else
        {
            Chasing();
        }


    }

    void Patrolling()
    {
        agent.speed = patrolSpeed;
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >= 2)
        {
            agent.SetDestination(target.transform.position);
        }
        else if (distance <= 2)
        {
            waypointInd += 1;
            if (waypointInd >= waypoints.Length)
            {
                waypointInd = 0;
            }
            target = waypoints[waypointInd];
        }
    }

    void Chasing()
    {
        agent.speed = chaseSpeed;
        distance = Vector3.Distance(target.transform.position, transform.position);
        agent.SetDestination(target.transform.position);

        if (distance <= attackDistance)
        {
            StartCoroutine(Attack()); 
        }
    }

    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(.75f);
        chasing = false;
        target.GetComponentInChildren<SanityMeter>().Kill();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            target = coll.gameObject;
            chasing = true;
        }
    }

}
