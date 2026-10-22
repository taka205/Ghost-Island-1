using UnityEngine;

public class IAFantasma : MonoBehaviour
{
    public float velocidadePatrulha = 1f;
    public float velocidadePerseguicao = 2.5f;
    public float raioDeVisao = 4f;
    public float raioBarreiraFogueira = 3f;

    private Transform player;
    private Transform fogueira;
    private Vector2 destinoPatrulha;
    private bool perseguindo = false;

    void Start()
    {
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null) player = objPlayer.transform;

        GameObject objFogo = GameObject.FindGameObjectWithTag("Fogueira");
        if (objFogo != null) fogueira = objFogo.transform;

        DefinirNovoDestino();
    }

    void Update()
    {
        // 1. Foge da Fogueira
        if (fogueira != null && Vector2.Distance(transform.position, fogueira.position) < raioBarreiraFogueira)
        {
            Vector2 direcaoFuga = (transform.position - fogueira.position).normalized;
            transform.position += (Vector3)direcaoFuga * velocidadePerseguicao * Time.deltaTime;
            DefinirNovoDestino();
            return;
        }

        // 2. Procura o Jogador
        if (player != null)
        {
            float distanciaPlayer = Vector2.Distance(transform.position, player.position);
            perseguindo = (distanciaPlayer <= raioDeVisao);
        }

        // 3. Movimenta-se
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (fogueira != null)
            {
                Fogueira fogueiraScript = fogueira.GetComponent<Fogueira>();
                if (fogueiraScript != null) fogueiraScript.tempoLuz -= 5f; // Perde 5 segundos
            }
            Destroy(gameObject);
        }
    }
}