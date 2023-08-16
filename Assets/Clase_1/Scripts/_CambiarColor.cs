using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CambiarColor : MonoBehaviour
{
    private Material mat;
    private Renderer _renderer;

    public Color nuevo_color;
    public bool colores_random;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        mat = _renderer.material;
    }

    private void Update()
    {
        if (colores_random)
        {
            mat.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1.0f);
        }
        else
            mat.color = nuevo_color;
    }
}
