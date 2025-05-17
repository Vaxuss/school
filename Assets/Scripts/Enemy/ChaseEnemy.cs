using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemy : MonoBehaviour
{
    GameObject _target;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] LayerMask _obstruction;
    bool _canSeePlayer = false;
    float _distanceToTarget;
    float _speed;

    void Start()
    {
       _target = FindFirstObjectByType<PlayerMovement>().gameObject; 
       _speed = _agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Sight();
        if (_canSeePlayer)
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

        _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);

        if (Physics.Raycast(transform.position, directionToTarget, _distanceToTarget, _obstruction))
            _canSeePlayer = false;
        else
            _canSeePlayer = true;
    }

    public void ChangeSpeed(float newSpeed, float stunTime)
    {
        _agent.speed = newSpeed;
        StartCoroutine(ResetStun(stunTime));
    }

    private IEnumerator ResetStun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        _agent.speed = _speed;
    }
}
