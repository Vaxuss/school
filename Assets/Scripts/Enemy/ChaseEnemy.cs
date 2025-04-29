using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemy : MonoBehaviour
{
    GameObject _target;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] LayerMask _obstruction;
    [SerializeField] GameObject Player;
    bool canSeePlayer = false;
    float distanceToTarget;

    void Start()
    {
       _target = FindFirstObjectByType<PlayerMovement>().gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        Sight();
        if (canSeePlayer)
        {
            if (Vector3.Distance(_agent.transform.position, _target.transform.position) > 3f)
                _agent.SetDestination(_target.transform.position);
            else
                _agent.ResetPath();
        }
    }

    void Sight()
    {
        Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;

        distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);

        if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstruction))
            canSeePlayer = false;
        else
            canSeePlayer = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = (Player.transform.position - transform.position).normalized * distanceToTarget;
        Gizmos.DrawRay(transform.position, direction);
    }
}
