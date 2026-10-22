using UnityEngine;
using TMPro;

public class AtirarTocha : MonoBehaviour
{
    public GameObject tochaPrefab;
    public Transform pontoDeTiro;
    public float tempoEntreTiros = 1f;

    [Header("Sistema de Munição")]
    public int tochasDisponiveis = 5;
    public TextMeshProUGUI textoContadorTochas;

    private float cronometroTiro;

    void Start()
    {
        AtualizarTextoTochas();
    }

    void Update()
    {
        cronometroTiro -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cronometroTiro <= 0f && tochasDisponiveis > 0)
        {
            Atirar();
            cronometroTiro = tempoEntreTiros;
        }
    }

    void Atirar()
    {
        if (tochaPrefab == null || pontoDeTiro == null) return;

        tochasDisponiveis--;
        AtualizarTextoTochas();

        Vector3 posicaoRato = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoRato.z = 0f;

        Vector2 direcao = (posicaoRato - pontoDeTiro.position).normalized;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        Quaternion rotacao = Quaternion.Euler(0, 0, angulo);

        Instantiate(tochaPrefab, pontoDeTiro.position, rotacao);
    }

    void AtualizarTextoTochas()
    {
        if (textoContadorTochas != null)
        {
            textoContadorTochas.text = "Tochas: " + tochasDisponiveis;
        }
    }
}