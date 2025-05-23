using UnityEngine;

public class DeathDelay : MonoBehaviour
{
    [SerializeField]
    float _deathDelay = .5f;

    void Start() => Invoke("Death", _deathDelay);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
            Death();
    }

    void Death() => Destroy(gameObject);
}
