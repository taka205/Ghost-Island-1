using UnityEngine;

public class GeradorMadeira : MonoBehaviour
{
    public GameObject madeiraPrefab;
    public float tempoEntreSpawns = 8f;
    public int maxMadeirasNoMapa = 10; // Limite de madeiras simultâneas no chão

    [Header("Limites do Mapa (X e Y)")]
    public Vector2 limitesMapa = new Vector2(14f, 9f);

    private float cronometro = 0f;

    void Update()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= tempoEntreSpawns)
        {
            // Só cria nova madeira se não tiver atingido o limite
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f); // Verde transparente
        Gizmos.DrawCube(transform.position, new Vector3(limitesMapa.x * 2, limitesMapa.y * 2, 0));
    }
}