using UnityEngine;
using System.Collections;
using TMPro;

public class ruleta : MonoBehaviour
{
    public float velocidad = 800f;
    public float duracion = 4f;
    public TextMeshProUGUI textoResultado;
    private bool girando = false;

    int[] numeros = { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26 };

    void Start()
    {
        // El 0 empieza arriba
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void Girar()
    {
        if (!girando)
            StartCoroutine(GirarRuleta());
    }

    IEnumerator GirarRuleta()
    {
        girando = true;

        int indiceGanador = Random.Range(0, numeros.Length);
        int numeroGanador = numeros[indiceGanador];

        float gradosPorSeccion = 360f / numeros.Length;
        float anguloDestino = indiceGanador * gradosPorSeccion;
        float vueltasExtras = 360f * Random.Range(5, 10);
        float anguloTotal = vueltasExtras + anguloDestino;

        float anguloInicial = 0f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float t = tiempo / duracion;
            float curva = Mathf.SmoothStep(0f, 1f, t);
            float angulo = Mathf.Lerp(anguloInicial, anguloInicial + anguloTotal, curva);
            transform.eulerAngles = new Vector3(0f, 0f, angulo);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = new Vector3(0f, 0f, anguloInicial + anguloTotal);
        girando = false;

        if (textoResultado != null)
            textoResultado.text = "Número: " + numeroGanador;

        Debug.Log("Ganador: " + numeroGanador);
        GestorApuestas.instancia?.ResolverApuesta(numeroGanador);
    }
}