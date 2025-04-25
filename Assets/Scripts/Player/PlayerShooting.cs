using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ShotPrefab;   
    [SerializeField] Transform ShotOrigin;
    [SerializeField] float ShotDealy = 0.2f;
    [SerializeField] float ShotForce = 20f;

    [Space]

    [SerializeField] GameObject SpecialShotPrefab;
    [SerializeField] Transform SpecialShotOrigin;
    [SerializeField] float SpecialCooldown = 3f;

    [Space]

    [SerializeField] Transform Camera;

    bool _canShoot = true;
    bool _canShootSpecial = true;
    float dealyCountdown = 0f;
    float dealySpecialCountdown = 0f;

    void Update()
    {
        if (!_canShoot)
        {
            dealyCountdown -= Time.deltaTime;
            if (dealyCountdown < 0f ) _canShoot = true;
        }

        if (!_canShootSpecial)
        {
            dealySpecialCountdown -= Time.deltaTime;
            if (dealySpecialCountdown < 0f) _canShootSpecial = true;
        }

        if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
        {
            Vector3 direction = ShotOrigin.position;

            GameObject currentBullet = Instantiate(ShotPrefab, ShotOrigin.position, Camera.rotation);
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * ShotForce, ForceMode.Impulse);

            _canShoot = false;
            dealyCountdown = ShotDealy;
        }

        if (Input.GetKey(KeyCode.LeftControl) && _canShootSpecial)
        {
            Vector3 direction = ShotOrigin.position;

            GameObject currentBullet = Instantiate(SpecialShotPrefab, SpecialShotOrigin.position, Camera.rotation);

            _canShootSpecial = false;
            dealySpecialCountdown = SpecialCooldown;
        }
    }
}
