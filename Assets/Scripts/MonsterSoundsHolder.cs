using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundsHolder : MonoBehaviour
{
    public List<AudioClip> _monsterSounds;

    private static MonsterSoundsHolder _monsterSoundsHolder = null;

    public static MonsterSoundsHolder Main {
        get {
            if(!_monsterSoundsHolder) {
                _monsterSoundsHolder = FindObjectOfType<MonsterSoundsHolder>();
            }

            return _monsterSoundsHolder;
        }
        set {_monsterSoundsHolder = value;}
    }
}
