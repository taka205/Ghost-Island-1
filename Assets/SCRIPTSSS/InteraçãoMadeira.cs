using UnityEngine;

public class InteracaoMadeira : MonoBehaviour
{
    public bool temMadeira = false;

    // Distâncias independentes
    public float raioInteragirFogueira = 1.5f; // Pode jogar a lenha de uma distância segura
    public float raioPegarMadeira = 0.5f;      // Precisa estar bem em cima da lenha para pegar

    public LayerMask layerMadeira;
    public GameObject imagemMadeiraNasCostas;
    public AudioClip somPegarMadeira;

    void Update()
    {
        if (imagemMadeiraNasCostas != null)
        {
            imagemMadeiraNasCostas.SetActive(temMadeira);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TentarInteragir();
        }
    }

    void TentarInteragir()
    {
        if (temMadeira)
        {
            // Tenta achar a fogueira num raio maior
            Collider2D fogueira = Physics2D.OverlapCircle(transform.position, raioInteragirFogueira);
            if (fogueira != null && fogueira.CompareTag("Fogueira"))
            {
                fogueira.GetComponent<Fogueira>().ColocarMadeira();
                temMadeira = false;
            }
        }
        else
        {
            // Tenta achar a madeira num raio bem pequeno (bem próximo)
            Collider2D madeiraNoChao = Physics2D.OverlapCircle(transform.position, raioPegarMadeira, layerMadeira);
            if (madeiraNoChao != null && madeiraNoChao.CompareTag("Madeira"))
            {
                temMadeira = true;

                if (somPegarMadeira != null)
                {
                    AudioSource.PlayClipAtPoint(somPegarMadeira, transform.position);
                }

                Destroy(madeiraNoChao.gameObject);
            }
        }
    }

    // Desenha os círculos no Unity para você ver exatamente a distância
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // Círculo amarelo para a fogueira
        Gizmos.DrawWireSphere(transform.position, raioInteragirFogueira);

        Gizmos.color = Color.green; // Círculo verde pequeno para pegar a lenha
        Gizmos.DrawWireSphere(transform.position, raioPegarMadeira);
    }
}