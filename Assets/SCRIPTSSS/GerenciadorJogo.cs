using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Fogueira fogueiraPrincipal;
    public float tempoDeJogo = 0f;
    public float tempoParaVencer = 180f; // 3 minutos em segundos
    private bool jogoTerminou = false;

    void Update()
    {
        if (jogoTerminou) return;

        tempoDeJogo += Time.deltaTime;

        if (tempoDeJogo >= tempoParaVencer)
        {
            VencerJogo();
        }
        else if (fogueiraPrincipal != null && fogueiraPrincipal.tempoLuz <= 0)
        {
            PerderJogo();
        }
    }

    void VencerJogo()
    {
        jogoTerminou = true;
        Debug.Log("VITÓRIA! Sobreviveste aos 3 minutos na Ilha de Anhatomirim!");
    }

    void PerderJogo()
    {
        jogoTerminou = true;
        Debug.Log("GAME OVER! A fogueira apagou e os fantasmas apanharam-te.");
    }
}