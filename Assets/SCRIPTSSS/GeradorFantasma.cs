using UnityEngine;

public class GeradorFantasmas : MonoBehaviour
{
    public GameObject fantasmaPrefab;
    public Transform fogueira;
    public float tempoEntreSpawns = 5f;
    public float raioSeguroFogueira = 8f; // Distância mínima que os fantasmas nascem longe do fogo

    [Header("Limites do Mapa (X e Y)")]
    public Vector2 limitesMapa = new Vector2(15f, 10f);

    private float cronometro = 0f;

    void Start()
    {
        // Acha a fogueira automaticamente se você esquecer de arrastar no Inspector
        if (fogueira == null)
        {
            GameObject objFogo = GameObject.FindGameObjectWithTag("Fogueira");
            if (objFogo != null) fogueira = objFogo.transform;
        }
    }

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
        if (fantasmaPrefab == null) return;

        Vector2 posicaoSorteada = Vector2.zero;
        bool posicaoValida = false;
        int tentativas = 0;

        // Tenta achar um ponto válido até 10 vezes por spawn para não travar o jogo
        while (!posicaoValida && tentativas < 10)
        {
            float randomX = Random.Range(-limitesMapa.x, limitesMapa.x);
            float randomY = Random.Range(-limitesMapa.y, limitesMapa.y);
            posicaoSorteada = new Vector2(randomX, randomY);

            // Verifica se está longe o suficiente da fogueira
            if (fogueira == null || Vector2.Distance(posicaoSorteada, fogueira.position) > raioSeguroFogueira)
            {
                posicaoValida = true;
            }
            tentativas++;
        }

        if (posicaoValida)
        {
            Instantiate(fantasmaPrefab, posicaoSorteada, Quaternion.identity);
        }
    }

    // Desenha um quadrado azul no Unity mostrando a área de spawn e um círculo vermelho da zona proibida
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f); // Azul transparente
        Gizmos.DrawCube(transform.position, new Vector3(limitesMapa.x * 2, limitesMapa.y * 2, 0));

        if (fogueira != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(fogueira.position, raioSeguroFogueira);
        }
    }
}