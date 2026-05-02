using UnityEngine;

public class CalibradorRuleta : MonoBehaviour
{
    void Start()
    {
        // Rotacion inicial para que el 0 quede arriba
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
}