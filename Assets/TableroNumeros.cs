using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TableroNumeros : MonoBehaviour
{
    public GameObject botonPrefab;
    public Transform contenedor;

    int[] rojos = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

    void Start()
    {
        int[,] layout = {
            { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 },
            { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 },
            { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 }
        };

        for (int fila = 0; fila < 3; fila++)
        {
            for (int col = 0; col < 12; col++)
            {
                int numero = layout[fila, col];
                CrearBoton(numero, fila, col);
            }
        }

        // Botón 0
        GameObject btn0 = Instantiate(botonPrefab, contenedor);
        RectTransform rt0 = btn0.GetComponent<RectTransform>();
        rt0.sizeDelta = new Vector2(50, 135);
        rt0.anchoredPosition = new Vector2(-355, 15);

        Image img0 = btn0.GetComponent<Image>();
        img0.color = new Color(0.1f, 0.5f, 0.1f);

        TextMeshProUGUI texto0 = btn0.GetComponentInChildren<TextMeshProUGUI>();
        if (texto0 != null) texto0.text = "0";

        btn0.GetComponent<Button>().onClick.AddListener(() => {
            GestorApuestas.instancia?.ApostarNumero(0);
        });
    }

    void CrearBoton(int numero, int fila, int col)
    {
        GameObject btn = Instantiate(botonPrefab, contenedor);
        RectTransform rt = btn.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 40);
        rt.anchoredPosition = new Vector2(col * 55 - 300, -fila * 45 + 60);

        Image img = btn.GetComponent<Image>();
        if (numero == 0)
            img.color = new Color(0.1f, 0.5f, 0.1f);
        else if (EsRojo(numero))
            img.color = new Color(0.8f, 0.1f, 0.1f);
        else
            img.color = new Color(0.1f, 0.1f, 0.1f);

        TextMeshProUGUI texto = btn.GetComponentInChildren<TextMeshProUGUI>();
        if (texto != null)
            texto.text = numero.ToString();

        int n = numero;
        btn.GetComponent<Button>().onClick.AddListener(() => {
            GestorApuestas.instancia?.ApostarNumero(n);
            Debug.Log("Apostando al numero: " + n);
        });
    }

    bool EsRojo(int n)
    {
        foreach (int r in rojos)
            if (r == n) return true;
        return false;
    }
}