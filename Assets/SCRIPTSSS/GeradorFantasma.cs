using UnityEngine;

public class GeradorFantasmas : MonoBehaviour
{
    public GameObject fantasmaPrefab;
    public Transform[] pontosDeSpawn;
    public float tempoEntreSpawns = 5f;

    private float cronometro = 0f;

    void Update()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= tempoEntreSpawns)
        {
            GerarFantasma();
            cronometro = 0f;
        }
    }

    void GerarFantasma()
    {
        if (pontosDeSpawn.Length == 0 || fantasmaPrefab == null) return;

        // Sorteia um dos pontos disponíveis
        int indiceAleatorio = Random.Range(0, pontosDeSpawn.Length);
        Transform pontoEscolhido = pontosDeSpawn[indiceAleatorio];

        // Cria o fantasma nesse ponto
        Instantiate(fantasmaPrefab, pontoEscolhido.position, Quaternion.identity);
    }
}