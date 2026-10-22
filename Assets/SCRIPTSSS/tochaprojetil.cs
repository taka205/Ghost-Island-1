using UnityEngine;

public class TochaProjetil : MonoBehaviour
{
    public float velocidade = 7f;
    public float tempoDeVida = 2f;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fantasma"))
        {
            Destroy(collision.gameObject); // Destrói o inimigo
            Destroy(gameObject); // Destrói a tocha
        }
    }
}