using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerEscenas : MonoBehaviour
{
    public void CambiarEscena(string nombre_de_escena)
    {
        SceneManager.LoadScene(nombre_de_escena);
    }
}
