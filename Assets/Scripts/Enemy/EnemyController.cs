using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private PlayerController playerController;

    private AudioSource _audioSource;

    void Start()  {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnEnable() {
        StartCoroutine(PlayRandomSound());
    }

    void Update() {
        transform.LookAt(new Vector3(playerController.transform.position.x, transform.position.y, playerController.transform.position.z));
    }

    private void FixedUpdate() {
        _rigidbody.velocity = transform.forward * GameManager.EnemySpeed;
    }

    IEnumerator PlayRandomSound() {
        int lenght = MonsterSoundsHolder.Main._monsterSounds.Count - 1;
        while(true) {
            yield return new WaitForSeconds(Random.Range(3, 10));
            _audioSource.PlayOneShot( MonsterSoundsHolder.Main._monsterSounds[Random.Range(0, lenght)]);
        };
    }

    private void OnDisable() {
        StopAllCoroutines();
    }
}
