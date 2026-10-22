using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para trocar de cenas

public class GameManager : MonoBehaviour
{
    public Fogueira fogueiraPrincipal;
    public float tempoDeJogo = 0f;
    public float tempoParaVencer = 180f;

    [Header("Nomes das Cenas")]
    public string cenaVitoria = "Vitoria";
    public string cenaDerrota = "Derrota";

    private bool jogoTerminou = false;

    void Update()
    {
        if (jogoTerminou) return;

        tempoDeJogo += Time.deltaTime;

        // Condição de Vitória
        if (tempoDeJogo >= tempoParaVencer)
        {
            VencerJogo();
        }
        // Condição de Derrota
        else if (fogueiraPrincipal != null && fogueiraPrincipal.tempoLuz <= 0)
        {
            PerderJogo();
        }
    }

    void VencerJogo()
    {
        jogoTerminou = true;
        SceneManager.LoadScene(cenaVitoria);
    }

    void PerderJogo()
    {
        jogoTerminou = true;
        SceneManager.LoadScene(cenaDerrota);
    }
}