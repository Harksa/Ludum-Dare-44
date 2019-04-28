using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private float flashLenght = 0;

    private Renderer _renderer;
    private Color storedColor;

    void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        storedColor = _renderer.material.GetColor("_Color");

        GameManager.HealthChanged += delegate(int damages) {
            StartCoroutine(ColorChange());
        };
    }

    private IEnumerator ColorChange() {
        _renderer.material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(flashLenght);
        _renderer.material.SetColor("_Color", storedColor);
    }
}
