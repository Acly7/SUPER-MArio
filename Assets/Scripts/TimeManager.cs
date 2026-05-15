using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI tiempoTXT;

    public float tiempo = 300f;

    void Update()
    {
        tiempo -= Time.deltaTime;

        tiempoTXT.text = "Tiempo: " + Mathf.Round(tiempo);
    }
}