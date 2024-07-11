using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PumpkinController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent Agent;

    [SerializeField]
    [Range(0, 50)]
    private int MaxPlayerDistance = 10;

    [SerializeField]
    [Range(0, 10)]
    private int IdleTime = 3;

    [SerializeField]
    private Animator PumpkinAnimator;

    private Vector3 randomDestination = Vector3.zero;

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
        if (Agent.remainingDistance == 0 && canMove)
        {
            canMove = false;
            MovePumpkin();
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
        PumpkinAnimator.Play("PumpkinWalk");
        Agent.SetDestination(randomDestination);
        canMove = true;
    }

    private void MovePumpkin()
    {
        PumpkinAnimator.Play("Idle");
        Invoke(nameof(SetRandomDestination), IdleTime);
    }
}
