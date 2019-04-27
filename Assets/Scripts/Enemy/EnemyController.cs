using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float speed;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(playerController.transform.position.x, transform.position.y, playerController.transform.position.z));
    }

    private void FixedUpdate() {
        _rigidbody.velocity = transform.forward * speed;
    }
}
