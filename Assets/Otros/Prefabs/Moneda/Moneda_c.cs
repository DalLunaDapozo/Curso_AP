using UnityEngine;

public class Moneda_c : MonoBehaviour
{
    public GameObject particulas;
    public float velocidad_rotacion;

    void Update()
    {
        transform.Rotate(velocidad_rotacion, 0, 0);    
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(particulas, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
