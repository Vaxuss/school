using UnityEngine;
using UnityEngine.UIElements;

public class Special : MonoBehaviour
{
    GameObject _target;

    [SerializeField]
    float _baseDistance = 100f;

    [SerializeField]
    float _stunTime = 2f;

    float _distance;

    internal Transform _playerPos;

    private void Start()
    {
        _distance = _baseDistance;

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit: " + hit);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                _target = hit.transform.gameObject;
            }
            else
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, 15, transform.forward, 15, 8);

                foreach (var enemy in hits)
                {
                    Debug.Log("area hit");
                    float distanceToPlayer = Vector3.Distance(enemy.transform.position, _playerPos.transform.position);
                    if (distanceToPlayer < _distance)
                    {
                        _distance = distanceToPlayer;
                        _target = enemy.transform.gameObject;
                    }
                }
                
                Debug.Log(_target);
            }

            if(_target == null)
                Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ChaseEnemy>().ChangeSpeed(0, _stunTime);
            Destroy(gameObject);
        }
    }
}
