using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class _ControladorClaseOcho : MonoBehaviour
{
    [SerializeField] private GameObject jugador;
    [SerializeField] private Image fondo_negro;

    [SerializeField] private Transform posicion_inicial;

    public bool reiniciar_nivel;
    public bool reiniciando;

    private void Start()
    {
        if(jugador == null)
        {
            try
            {
                jugador = GameObject.Find("Jugador");
            }
            catch
            {
                Debug.LogError("NO SE ENCONTRO JUGADOR");
            }
        }
    }

    private void Update()
    {
        if(jugador.transform.position.y < -4f && !reiniciar_nivel)
        {
            if(!reiniciando)
                reiniciar_nivel = true;
        }

        if(reiniciar_nivel)
        {
            StartCoroutine(ReiniciarNivel());
        }
    }

    private IEnumerator ReiniciarNivel()
    {
        reiniciar_nivel = false;
        reiniciando = true;

        //CREAMOS UNA VARIABLE LOCAL PARA ALMACENAR TIEMPO
        float time = 0;
        //Y OTRA PARA LA DURACION DE LA TRANSICION
        float duration = 1f;

        //ABRIMOS UN WHILE, EN UNITY, LOS WHILE NO SE USAN EN CASI NINGUNA OCASION, EXCEPTO DENTRO DE CORRUTINAS :D 
        //BASICAMENTE ABRIMOS UN BUCLE QUE SE REPITE HASTA QUE NO SE CUMPLA LA CONDICION QUE ESTÁ ENTRE PARÉNTESIS
        //EN ESTE CASO: QUE EL TIEMPO QUE TRANSCURRE SEA MENOR A LA DURACION (QUE EN ESTE CASO VALOR 1)
        while (time < duration)
        {
            //ACA MOVEMOS LA POSICION EN X DE LA PANTALLA EN NEGRO Y USAMOS UN LERP QUE ES UNA FUNCION MATEMATICA PARA TRANSPOLAR DOS VALORES DE FORMA GRADUAL
            //ESO LE DA EL EFECTO DE DESLIZAMIENTO A LA PANTALLA
            fondo_negro.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(800, 0, time / duration), 0, 0);
            //CADA FRAME LE SUMAMOS UN DELTATIME A NUESTRA VARIABLE DE TIEMPO PARA PODER ROMPER EL LOOP EN ALGUN MOMENTO
            time += Time.deltaTime;

            yield return null;
        }

  
        //Y MOVEMOS EL AUTO A SU POSICION INICIAL
        jugador.transform.position = posicion_inicial.position;
        //REINICIAMOS LA VARIABLE TIME PARA REUTILIZARLA
        time = 0;
        //HACEMOS LO MISMO QUE ANTES PERO A LA INVERSA PARA VOLVER LA PANTALLA A LA NORMALIDAD
        while (time < duration)
        {
            fondo_negro.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(0, 800, time / duration), 0, 0);
            time += Time.deltaTime;

            yield return null;
        }

        reiniciando = false;
    }
}
