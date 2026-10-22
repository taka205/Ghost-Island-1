using UnityEngine;

public class InteracaoMadeira : MonoBehaviour
{
    public bool temMadeira = false;
    public float raioInteragirFogueira = 1.5f;
    public float raioPegarMadeira = 0.5f;
    public LayerMask layerMadeira;
    public GameObject imagemMadeiraNasCostas;

    // Novo: Espa�o para colocar o arquivo de som
    public AudioClip somPegarMadeira;

    void Update()
    {
        if (imagemMadeiraNasCostas != null) imagemMadeiraNasCostas.SetActive(temMadeira);

        if (Input.GetKeyDown(KeyCode.E)) TentarInteragir();
    }

    void TentarInteragir()
    {
        if (temMadeira)
        {
            Collider2D fogueira = Physics2D.OverlapCircle(transform.position, raioInteragirFogueira);
            if (fogueira != null && fogueira.CompareTag("Fogueira"))
            {
                fogueira.GetComponent<Fogueira>().ColocarMadeira();
                temMadeira = false;
            }
        }
        else
        {
            Collider2D madeiraNoChao = Physics2D.OverlapCircle(transform.position, raioPegarMadeira, layerMadeira);
            if (madeiraNoChao != null && madeiraNoChao.CompareTag("Madeira"))
            {
                temMadeira = true;
                if (somPegarMadeira != null) AudioSource.PlayClipAtPoint(somPegarMadeira, transform.position);
                Destroy(madeiraNoChao.gameObject);
            }
        }
    }
}