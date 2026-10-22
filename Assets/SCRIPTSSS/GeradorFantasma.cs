using UnityEngine;

public class GeradorFantasmas : MonoBehaviour
{
    public GameObject fantasmaPrefab;
    public Transform[] pontosDeSpawn;
    public float tempoEntreSpawns = 5f;
    public float raioSeguroFogueira = 8f;
    public Vector2 limitesMapa = new Vector2(15f, 10f);
    private float cronometro = 0f;

    void Start()
    {
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

        while (!posicaoValida && tentativas < 10)
        {
            float randomX = Random.Range(-limitesMapa.x, limitesMapa.x);
            float randomY = Random.Range(-limitesMapa.y, limitesMapa.y);
            posicaoSorteada = new Vector2(randomX, randomY);

            if (fogueira == null || Vector2.Distance(posicaoSorteada, fogueira.position) > raioSeguroFogueira)
            {
                posicaoValida = true;
            }
            tentativas++;
        }

        if (posicaoValida) Instantiate(fantasmaPrefab, posicaoSorteada, Quaternion.identity);
    }
}