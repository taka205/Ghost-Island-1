using UnityEngine;

public class IAFantasma : MonoBehaviour
{
    public float velocidadePatrulha = 1f;
    public float velocidadePerseguicao = 2.5f;
    public float raioDeVisao = 4f;
    public float raioBarreiraFogueira = 3f; // Distância mínima a que o fantasma chega do fogo

    private Transform player;
    private Transform fogueira;
    private Vector2 destinoPatrulha;
    private bool perseguindo = false;

    void Start()
    {
        // Procura o jogador e a fogueira ao nascer
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null) player = objPlayer.transform;

        GameObject objFogueira = GameObject.FindGameObjectWithTag("Fogueira");
        if (objFogueira != null) fogueira = objFogueira.transform;

        DefinirNovoDestino();
    }

    void Update()
    {
        // 1. A Barreira da Fogueira (Prioridade Máxima)
        if (fogueira != null && Vector2.Distance(transform.position, fogueira.position) < raioBarreiraFogueira)
        {
            // Calcula a direção contrária à fogueira e empurra o fantasma para trás
            Vector2 direcaoFuga = (transform.position - fogueira.position).normalized;
            transform.position += (Vector3)direcaoFuga * velocidadePerseguicao * Time.deltaTime;

            // Recalcula um novo destino para ele não tentar insistir em ir para o fogo
            DefinirNovoDestino();
            return; // Ignora o resto do movimento (patrulha/perseguição) enquanto estiver na luz
        }

        // 2. Lógica de Deteção do Jogador
        if (player != null)
        {
            float distanciaPlayer = Vector2.Distance(transform.position, player.position);
            perseguindo = (distanciaPlayer <= raioDeVisao);
        }

        // 3. Movimento Normal
        if (perseguindo && player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidadePerseguicao * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destinoPatrulha, velocidadePatrulha * Time.deltaTime);

            if (Vector2.Distance(transform.position, destinoPatrulha) < 0.1f)
            {
                DefinirNovoDestino();
            }
        }
    }

    void DefinirNovoDestino()
    {
        float randomX = Random.Range(-4f, 4f);
        float randomY = Random.Range(-4f, 4f);
        destinoPatrulha = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeVisao);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("O fantasma apanhou o jogador!");
            Destroy(gameObject);
        }
    }
}