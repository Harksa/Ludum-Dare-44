using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private int _bulletId;
    [SerializeField] private float bulletSpeed = 10;
 
    [SerializeField] private Transform firePoint = null;

    [SerializeField] private Light _gunFireLigt = null;
    private WaitForSeconds _wait = new WaitForSeconds(0.03f);
    private AudioSource _audioSource = null;

    private bool _firing;
    public bool Firing
    {
        get { return _firing;}
        set { 
            _firing = value;
            if(_firing) {
                InvokeRepeating("Fire", 0, GameManager.PlayerFireRate);
            } else {
                CancelInvoke();
            }
        }
    }
    
    void Awake()
    {
        ObjectPool.InitPool(bullet, 50);
        _bulletId = bullet.GetInstanceID();

        _gunFireLigt.enabled = false;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Fire() {
        GameObject go = ObjectPool.GetInstance(_bulletId, firePoint.position, firePoint.rotation);
		go.GetComponent<BulletController>().speed = bulletSpeed;
        StartCoroutine(FireLight());
        _audioSource.Play();
    }

    IEnumerator FireLight() {
        _gunFireLigt.enabled = true;
        yield return _wait;
        _gunFireLigt.enabled = false;
    }

}
