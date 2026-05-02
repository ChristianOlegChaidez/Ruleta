using UnityEngine;
using TMPro;

public class GestorApuestas : MonoBehaviour
{
    public static GestorApuestas instancia;

    public int saldo = 1000;
    public int apuestaActual = 10;
    public TextMeshProUGUI textoSaldo;
    public TextMeshProUGUI textoApuesta;

    string tipoApuesta = "";
    int numeroApuestado = -1;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        instancia = this;
    }

    void Start()
    {
        ActualizarUI();
    }

    public void ApostarNumero(int numero)
    {
        if (saldo < apuestaActual) return;
        tipoApuesta = "numero";
        numeroApuestado = numero;
        Debug.Log("Apostando al numero: " + numero);
    }

    public void ApostarTipo(string tipo)
    {
        if (saldo < apuestaActual) return;
        tipoApuesta = tipo.ToLower();
        Debug.Log("Apostando a: " + tipo);
    }

    public void ResolverApuesta(int resultado)
    {
        if (tipoApuesta == "") return;

        Debug.Log("Tipo: " + tipoApuesta + " | Resultado: " + resultado + " | EsRojo: " + EsRojo(resultado) + " | EsNegro: " + EsNegro(resultado));

        saldo -= apuestaActual;
        bool gano = false;

        if (tipoApuesta == "numero" && numeroApuestado == resultado)
        {
            saldo += apuestaActual * 36;
            gano = true;
        }
        else if (tipoApuesta == "rojo" && EsRojo(resultado))
        {
            saldo += apuestaActual * 2;
            gano = true;
        }
        else if (tipoApuesta == "negro" && EsNegro(resultado))
        {
            saldo += apuestaActual * 2;
            gano = true;
        }
        else if (tipoApuesta == "par" && resultado != 0 && resultado % 2 == 0)
        {
            saldo += apuestaActual * 2;
            gano = true;
        }
        else if (tipoApuesta == "impar" && resultado != 0 && resultado % 2 != 0)
        {
            saldo += apuestaActual * 2;
            gano = true;
        }
        else if (tipoApuesta == "1a18" && resultado >= 1 && resultado <= 18)
        {
            saldo += apuestaActual * 2;
            gano = true;
        }
        else if (tipoApuesta == "19a36" && resultado >= 19 && resultado <= 36)
        {
            saldo += apuestaActual * 2;
            gano = true;
        }
        else if (tipoApuesta == "1a12" && resultado >= 1 && resultado <= 12)
        {
            saldo += apuestaActual * 3;
            gano = true;
        }
        else if (tipoApuesta == "13a24" && resultado >= 13 && resultado <= 24)
        {
            saldo += apuestaActual * 3;
            gano = true;
        }
        else if (tipoApuesta == "25a36" && resultado >= 25 && resultado <= 36)
        {
            saldo += apuestaActual * 3;
            gano = true;
        }
        else if (tipoApuesta == "col1" && resultado % 3 == 1)
        {
            saldo += apuestaActual * 3;
            gano = true;
        }
        else if (tipoApuesta == "col2" && resultado % 3 == 2)
        {
            saldo += apuestaActual * 3;
            gano = true;
        }
        else if (tipoApuesta == "col3" && resultado % 3 == 0 && resultado != 0)
        {
            saldo += apuestaActual * 3;
            gano = true;
        }

        Debug.Log(gano ? "GANASTE! Saldo: " + saldo : "PERDISTE. Saldo: " + saldo);
        ActualizarUI();
        tipoApuesta = "";
        numeroApuestado = -1;
    }

    bool EsRojo(int n)
    {
        int[] rojos = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        foreach (int r in rojos)
            if (r == n) return true;
        return false;
    }

    bool EsNegro(int n)
    {
        return n != 0 && !EsRojo(n);
    }

    void ActualizarUI()
    {
        if (textoSaldo != null) textoSaldo.text = "Saldo: $" + saldo;
        if (textoApuesta != null) textoApuesta.text = "Apuesta: $" + apuestaActual;
    }
}