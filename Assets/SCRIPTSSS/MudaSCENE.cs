using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleDeCenas : MonoBehaviour
{
    public void IrParaCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }

    public void SairDoJogo()
    {
        Application.Quit();
        Debug.Log("O jogo foi fechado.");
    }
}