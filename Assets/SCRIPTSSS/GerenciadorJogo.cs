using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Biblioteca necess�ria para usar o TextMeshPro

public class GameManager : MonoBehaviour
{
    public Fogueira fogueiraPrincipal;
    public float tempoDeJogo = 0f;
    public float tempoParaVencer = 90f; // 1 minuto e 30 segundos

    [Header("Nomes das Cenas")]
    public string cenaVitoria = "Vitoria";
    public string cenaDerrota = "Derrota";

    [Header("Textos da Interface (UI)")]
    public TextMeshProUGUI textoTempoRestante; // Usa TextMeshPro
    public TextMeshProUGUI textoLuzFogueira;   // Usa TextMeshPro

    private bool jogoTerminou = false;

    void Update()
    {
        if (jogoTerminou) return;

        tempoDeJogo += Time.deltaTime;

        // Atualiza os textos na tela a cada frame
        AtualizarTextosNaTela();

        if (tempoDeJogo >= tempoParaVencer)
        {
            VencerJogo();
        }
        else if (fogueiraPrincipal != null && fogueiraPrincipal.tempoLuz <= 0)
        {
            PerderJogo();
        }
    }

    void AtualizarTextosNaTela()
    {
        // 1. Rel�gio de Sobreviv�ncia (Contagem Decrescente)
        float tempoRestante = tempoParaVencer - tempoDeJogo;
        if (tempoRestante < 0) tempoRestante = 0;

        int minutos = Mathf.FloorToInt(tempoRestante / 60);
        int segundos = Mathf.FloorToInt(tempoRestante % 60);

        if (textoTempoRestante != null)
        {
            // Formata no estilo rel�gio: 01:30
            textoTempoRestante.text = string.Format("Sobreviver: {0:00}:{1:00}", minutos, segundos);
        }

        // 2. Contador da Fogueira
        if (fogueiraPrincipal != null && textoLuzFogueira != null)
        {
            int luz = Mathf.CeilToInt(fogueiraPrincipal.tempoLuz);
            textoLuzFogueira.text = "Luz da Fogueira: " + luz + "s";
        }
    }

    void VencerJogo()
    {
        jogoTerminou = true;
        Debug.Log("VIT�RIA! Sobreviveste aos 3 minutos na Ilha de Anhatomirim!");
    }

    void PerderJogo()
    {
        jogoTerminou = true;
        Debug.Log("GAME OVER! A fogueira apagou e os fantasmas apanharam-te.");
    }
}