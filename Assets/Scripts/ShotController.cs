using UnityEngine;

public class ShotController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ChaseEnemy enemy))
        {
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }
}
