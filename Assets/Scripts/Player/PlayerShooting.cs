using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject _shotPrefab;   
    [SerializeField] Transform _shotOrigin;
    [SerializeField] float _shotDealy = 0.2f;
    [SerializeField] float _shotForce = 20f;
    [SerializeField] int _ammo = 10;
    [SerializeField] TextMeshProUGUI _ammoDisplay;

    [Space]

    [SerializeField] GameObject _specialShotPrefab;
    [SerializeField] Transform _specialShotOrigin;
    [SerializeField] float _specialCooldown = 3f;

    [Space]

    [SerializeField] Transform Camera;

    bool _canShoot = true;
    bool _canShootSpecial = true;
    float _dealyCountdown = 0f;
    float _dealySpecialCountdown = 0f;

    private void Start()
    {
        _ammoDisplay.text = _ammo.ToString();
    }

    void Update()
    {
        if (!_canShoot)
        {
            _dealyCountdown -= Time.deltaTime;
            if (_dealyCountdown < 0f ) _canShoot = true;
        }

        if (!_canShootSpecial)
        {
            _dealySpecialCountdown -= Time.deltaTime;
            if (_dealySpecialCountdown < 0f) _canShootSpecial = true;
        }

        if (Input.GetKey(KeyCode.Mouse0) && _canShoot && _ammo > 0)
        {
            _ammo--;
            _ammoDisplay.text = _ammo.ToString();
            Vector3 direction = _shotOrigin.position;

            GameObject currentBullet = Instantiate(_shotPrefab, _shotOrigin.position, Camera.rotation);
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * _shotForce, ForceMode.Impulse);

            _canShoot = false;
            _dealyCountdown = _shotDealy;
        }

        if (Input.GetKey(KeyCode.Mouse1) && _canShootSpecial)
        {
            Vector3 direction = _shotOrigin.position;

            GameObject specialShot = Instantiate(_specialShotPrefab, _specialShotOrigin.position, _specialShotOrigin.rotation);
            specialShot.GetComponent<Special>()._playerPos = transform;

            _canShootSpecial = false;
            _dealySpecialCountdown = _specialCooldown;
        }
    }

    public void IncreaseAmmo(int amount)
    {
        if(_ammo < 100)
            _ammo += amount;

        if(_ammo > 100) 
            _ammo = 100;

        _ammoDisplay.text = _ammo.ToString();
    }
}
