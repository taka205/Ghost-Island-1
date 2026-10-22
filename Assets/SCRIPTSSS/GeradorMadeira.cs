using UnityEngine;

public class GeradorMadeira : MonoBehaviour
{
    public GameObject madeiraPrefab;
    public float tempoEntreSpawns = 8f;
    public int maxMadeirasNoMapa = 10;
    public Vector2 limitesMapa = new Vector2(14f, 9f);
    private float cronometro = 0f;

    void Update()
    {
        cronometro += Time.deltaTime;
        if (cronometro >= tempoEntreSpawns)
        {
            if (GameObject.FindGameObjectsWithTag("Madeira").Length < maxMadeirasNoMapa)
            {
                GerarMadeira();
            }
            cronometro = 0f;
        }
    }

    void GerarMadeira()
    {
        if (madeiraPrefab == null) return;
        float randomX = Random.Range(-limitesMapa.x, limitesMapa.x);
        float randomY = Random.Range(-limitesMapa.y, limitesMapa.y);
        Vector2 posicaoSorteada = new Vector2(randomX, randomY);
        Instantiate(madeiraPrefab, posicaoSorteada, Quaternion.identity);
    }
}