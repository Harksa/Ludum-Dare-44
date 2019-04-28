using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource = null;
    [SerializeField] private List<AudioClip> _musics = null;

    private int _currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        GameManager.WaveChanged += delegate(int wave) {
            if(wave == 3 || wave == 6) {
                Invoke("PlayNextTrack", _audioSource.clip.length - _audioSource.time + 0.5f);
            }
        };

    }

   private void PlayNextTrack() {
       _audioSource.Stop();
       _audioSource.clip = _musics[++_currentIndex];
       _audioSource.Play();
   }
}
