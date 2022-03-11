using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fov : MonoBehaviour
{
    public float radius;
    private float baseRadius;


    [Header("Velocidad")]
    [SerializeField] private float vPatrolling = 2.5f;
    [SerializeField] private float vRun = 5f;
    [Range(0, 360)] public float angle;
    private float originalAngle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    private bool canSeePlayer;
    private NavMeshAgent agent;
    private Transform target;

    public Transform[] destinations;
    private int currentPoint;
    private float distanceToPatrollObjective = 1f;
    private bool seen = false;
    private Animator myAnimator = default;

    [Header("Sonidos")]
    [SerializeField] private AudioSource audioSource = default;
    [SerializeField] private AudioClip walkingClip = default;
    [SerializeField] private AudioClip runningClip = default;


    private void Start()
    {
        originalAngle = angle;
        baseRadius = radius;
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        currentPoint = Random.Range(0, destinations.Length);
        StartCoroutine(fovRoutine());
        backToPatroll();
    }

    private void FixedUpdate()
    {
        if (canSeePlayer && target != null)
        {
            myAnimator.SetBool("canSeePlayer", true);
            agent.speed = vRun;
            angle = 360f;
            radius = 20;
            agent.SetDestination(target.position);
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkingClip);
            }
            myAnimator.SetBool("canSeePlayer", false);
            angle = originalAngle;
            agent.speed = vPatrolling;
            radius = baseRadius;
            seen = false;
            backToPatroll();
        }
    }

    private void backToPatroll()
    {
        Transform destination = destinations[currentPoint];
        float distToObjective = Vector3.Distance(transform.position, destination.position);
        if (distToObjective > distanceToPatrollObjective)
        {
            agent.SetDestination(destination.position);  // Aun no llega a su destino
        }
        else
        {
            currentPoint = Random.Range(0, destinations.Length);  //Llega al destino, cambia de objetivo
        }

    }

    /*
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            PERDISTE EL JUEGO METODO
        }
    }
    */

    // Es un loop infinito donde el enemy revisa cada 0.2 segundos si a visto al jugador  
    public IEnumerator fovRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            fovCheck();
        }
    }

    private void fovCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    //Le vio al jugador que hace?
                    if (seen == false)
                    {
                        seen = true;
                        canSeePlayer = true;
                        StartCoroutine(playerSeenActions());
                    }
                    // canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            target = null;
        }
    }

    private IEnumerator playerSeenActions()
    {
        transform.LookAt(target);
        audioSource.Stop();
        Debug.Log("Te vi");
        myAnimator.SetBool("seen", true);
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        Debug.Log("Te sigo ahora");
        agent.isStopped = false;
        myAnimator.SetBool("seen", false);
        if (target != null)
        {
            audioSource.PlayOneShot(runningClip);
            audioSource.PlayOneShot(walkingClip);
            agent.SetDestination(target.position);
        }
    }





}
