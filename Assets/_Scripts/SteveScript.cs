using UnityEngine;
using System.Collections;

//https://www.youtube.com/watch?v=s67AYDD3j1E

public class SteveScript : MonoBehaviour {

    public int attackDistance = 2;

    private float distance;

    public GameObject[] waypoints;
    private int waypointInd;
    private GameObject target;

    private NavMeshAgent agent;

    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 2.5f;

    private Animator animator;

    public State state;

    private Vector3 investigateSpot;
    private float invetigateTimer = 0;
    public float investigateWait = 10;

    public float heightMultiplier;
    public float sightDist = 15;
    public float lookSpeed = .03f;

    private bool chasing = false;
    private bool seePlayer = false;
    private float seeTimer = 0;
    private float seeWait = 3;

    public enum State
    {
        PATROL,
        CHASE,
        INVESTIGATE
    }

    void Start()
    {
        
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

        state = SteveScript.State.PATROL;
        target = waypoints[waypointInd];

        heightMultiplier = .6f;
    }

    void Update()
    {
        switch (state)
        {
            case State.PATROL:
                Patrolling();
                break;
            case State.CHASE:
                Chasing();
                break;
            case State.INVESTIGATE:
                Investigating();
                break;
        }

    }

    void Patrolling()
    {
        animator.SetBool("Looking", false);
        agent.speed = patrolSpeed;
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >= 2)
        {
            agent.SetDestination(target.transform.position);
        }
        else if (distance <= 2)
        {
            waypointInd = Random.Range(0, waypoints.Length);

            target = waypoints[waypointInd];
        }
    }

    void Chasing()
    {
        invetigateTimer = 0;
        if (seePlayer)
        {
            seeTimer = 0;
        }
        else
        {
            seeTimer += Time.deltaTime;
        }
        if(seeTimer <= seeWait)
        { 
            chasing = true;
            animator.SetBool("Looking", false);
            agent.speed = chaseSpeed;
            distance = Vector3.Distance(target.transform.position, transform.position);
            agent.SetDestination(target.transform.position);

            if (distance <= attackDistance)
            {
                StartCoroutine(Attack()); 
            }
        }
        else
        {
            state = SteveScript.State.INVESTIGATE;
            chasing = false;
        }
    }

    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(.75f);
        state = SteveScript.State.PATROL;
        target.GetComponentInChildren<SanityMeter>().Kill();
    }

    void Investigating()
    {
        animator.SetBool("Looking", true);
        invetigateTimer += Time.deltaTime;
        agent.SetDestination(this.transform.position);
        Quaternion rotate = Quaternion.LookRotation(investigateSpot - transform.position);
        if(Quaternion.Angle(transform.rotation, rotate) > 15)
        {
            animator.SetBool("Turn", true);
        }
        else
        {
            animator.SetBool("Turn", false);
        }
        //Debug.Log(Quaternion.Angle(transform.rotation, rotate));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, lookSpeed);
        if (invetigateTimer >= investigateWait)
        {
            state = SteveScript.State.PATROL;
            target = waypoints[waypointInd];
            invetigateTimer = 0;
        }
    }

    void FixedUpdate()
    {
        seePlayer = false;
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);
        if(Physics.Raycast (transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                state = SteveScript.State.CHASE;
                target = hit.collider.gameObject;
                investigateSpot = hit.collider.gameObject.transform.position;
                seePlayer = true;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = SteveScript.State.CHASE;
                target = hit.collider.gameObject;
                investigateSpot = hit.collider.gameObject.transform.position;
                seePlayer = true;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = SteveScript.State.CHASE;
                target = hit.collider.gameObject;
                investigateSpot = hit.collider.gameObject.transform.position;
                seePlayer = true;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    { 
        if (coll.tag == "Player" && !chasing)
        {
            state = SteveScript.State.INVESTIGATE;
            investigateSpot = coll.gameObject.transform.position;
        }
        
    }

}
