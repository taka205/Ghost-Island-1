using UnityEngine;

public class Fogueira : MonoBehaviour
{
    public float tempoLuz = 30f; // Tempo inicial de luz (ajusta como preferires)
    public float tempoMaximoLuz = 60f; // Limite máximo para a fogueira não ficar infinita

    void Update()
    {
        // A luz apaga 1 segundo por cada segundo real
        tempoLuz -= Time.deltaTime;

        // Garante que a luz não passa do limite máximo nem desce de zero
        tempoLuz = Mathf.Clamp(tempoLuz, 0, tempoMaximoLuz);
    }

    public void ColocarMadeira()
    {
        tempoLuz += 10f; // Adiciona 10 segundos
        Debug.Log("Madeira adicionada! Tempo atual: " + tempoLuz);
    }
}