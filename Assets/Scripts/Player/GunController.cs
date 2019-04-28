using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private int _bulletId;
    [SerializeField] private float bulletSpeed = 10;
 
    [SerializeField] private Transform firePoint = null;

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
    
    void Start()
    {
        ObjectPool.InitPool(bullet, 50);
        _bulletId = bullet.GetInstanceID();
    }

    private void Fire() {
        GameObject go = ObjectPool.GetInstance(_bulletId, firePoint.position, firePoint.rotation);
		go.GetComponent<BulletController>().speed = bulletSpeed;
    }
}
