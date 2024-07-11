using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class FrankensteinController : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerTransform;

    [SerializeField]
    private NavMeshAgent Agent;

    [SerializeField]
    [Range(0, 50)]
    private int MaxPlayerDistance = 15;

    [SerializeField]
    [Range(0, 10)]
    private int IdleTime = 4;

    [SerializeField]
    private Animator FrankAnimator;

    private Vector3 lastDestination;

    private Vector3 randomDestination = Vector3.zero;

    private bool isFollowingPlayer = false;

    private bool canMove;
    private void OnEnable()
    {
        SetRandomDestination();
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(SetRandomDestination));
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, PlayerTransform.position) <= MaxPlayerDistance)
        {
            if(PlayerTransform.position != lastDestination)
            {
                FollowPlayer();
            }
        }
        else
        {
            if(canMove && (isFollowingPlayer || Agent.remainingDistance == 0))
            {
                canMove = false;
                MoveFrank();
            }
        }
    }


    private void SetRandomDestination()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-MaxPlayerDistance, MaxPlayerDistance), 0, Random.Range(-MaxPlayerDistance, MaxPlayerDistance));
        randomPosition.y = 0;
        randomPosition += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, MaxPlayerDistance, 1);
        randomDestination = hit.position;
        FrankAnimator.Play("FrankWalk");
        Agent.SetDestination(randomDestination);
        Agent.isStopped = false;
        isFollowingPlayer = false;
        canMove = true;
    }

    private void FollowPlayer()
    {
        Agent.SetDestination(PlayerTransform.position);
        Agent.isStopped = false;
        FrankAnimator.Play("FrankWalk");
        lastDestination = PlayerTransform.position;
        isFollowingPlayer = true;
    }

    private void MoveFrank()
    {
        FrankAnimator.Play("Idle");
        Agent.isStopped = true;
        Invoke(nameof(SetRandomDestination), IdleTime);
    }
}
