using UnityEngine;

public class Flutuar : MonoBehaviour
{
    public float velocidade = 2f;
    public float altura = 0.1f;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        // Faz a madeira subir e descer suavemente
        float novoY = posicaoInicial.y + Mathf.Sin(Time.time * velocidade) * altura;
        transform.position = new Vector3(transform.position.x, novoY, transform.position.z);
    }
}