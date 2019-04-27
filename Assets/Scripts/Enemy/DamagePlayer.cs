using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public float damages = 10;

    [SerializeField] private float damageTicks = 0.75f;

    private float currentDamageTick = 0;

    private void Update() {
        if(currentDamageTick > 0) {
            currentDamageTick -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
       if(other.gameObject.CompareTag("Player")) {
           GameManager.Health -= damages;
           currentDamageTick = damageTicks;
       } 
    }
    
    private void OnTriggerStay(Collider other) {
        if(currentDamageTick <= 0) {
            if(other.gameObject.CompareTag("Player")) {
                GameManager.Health -= damages;
                currentDamageTick = damageTicks;
            } 
        }
    }

}
