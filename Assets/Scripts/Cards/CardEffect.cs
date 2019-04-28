using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{

    public enum CARD_EFFECT {
        INCREASE_DAMAGES,
        INCREASE_FIRERATE,
        INCREASE_SPEED,
        REGAIN_HEALTH
    }


    public void ApplyEffectAndStartWave(int card) {
        CARD_EFFECT effect = (CARD_EFFECT) card;
 
        if(effect != CARD_EFFECT.REGAIN_HEALTH)
            GameManager.PlayerHealth -= 10;

        switch(effect) {
            case CARD_EFFECT.INCREASE_DAMAGES:
                GameManager.PlayerDamages += GameManager.PlayerIncreaseDamages;
                break;
            case CARD_EFFECT.INCREASE_FIRERATE:
                GameManager.PlayerFireRate -= GameManager.PlayerIncreaseFireRate;
                break;
            case CARD_EFFECT.INCREASE_SPEED:
                GameManager.PlayerSpeed += GameManager.PlayerIncreaseSpeed;
                break;
            case CARD_EFFECT.REGAIN_HEALTH:
                GameManager.PlayerHealth += GameManager.PlayerRegainLife;
                break;
        }

        GameManager.StartNextWave();
    }

}
