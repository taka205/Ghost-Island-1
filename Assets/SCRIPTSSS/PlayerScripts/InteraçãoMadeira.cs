using UnityEngine;

public class InteracaoMadeira : MonoBehaviour
{
    public bool temMadeira = false;
    public float raioDeAcao = 1.5f;
    public LayerMask layerMadeira;
    public GameObject imagemMadeiraNasCostas;

    // Novo: Espaço para colocar o arquivo de som
    public AudioClip somPegarMadeira;

    void Update()
    {
        if (imagemMadeiraNasCostas != null)
        {
            imagemMadeiraNasCostas.SetActive(temMadeira);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            TentarInteragir();
        }
    }

    void TentarInteragir()
    {
        if (temMadeira)
        {
            Collider2D fogueira = Physics2D.OverlapCircle(transform.position, raioDeAcao);
            if (fogueira != null && fogueira.CompareTag("Fogueira"))
            {
                fogueira.GetComponent<Fogueira>().ColocarMadeira();
                temMadeira = false;
            }
        }
        else
        {
            Collider2D madeiraNoChao = Physics2D.OverlapCircle(transform.position, raioDeAcao, layerMadeira);
            if (madeiraNoChao != null && madeiraNoChao.CompareTag("Madeira"))
            {
                temMadeira = true;

                // Toca o som exatamente onde o jogador está, antes de destruir a madeira
                if (somPegarMadeira != null)
                {
                    AudioSource.PlayClipAtPoint(somPegarMadeira, transform.position);
                }

                Destroy(madeiraNoChao.gameObject);
            }
        }
    }
}