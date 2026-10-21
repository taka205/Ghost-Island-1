using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fogueira : MonoBehaviour
{
    public Light2D luz;
    public CircleCollider2D areaSegura;

    public float tempoLuz = 90f;
    public float tempoMaximo = 120f;
    public float raioMaximo = 8f;

    void Update()
    {
        if (tempoLuz > 0)
        {
            tempoLuz -= Time.deltaTime;

            float tamanhoDaLuz = (tempoLuz / 90f) * raioMaximo;

            luz.pointLightOuterRadius = tamanhoDaLuz;
            areaSegura.radius = tamanhoDaLuz;
        }
        else
        {
            tempoLuz = 0;
            luz.pointLightOuterRadius = 0f;
            areaSegura.radius = 0f;
        }
    }

    public void ColocarMadeira()
    {
        tempoLuz += 15f;

        if (tempoLuz > tempoMaximo)
        {
            tempoLuz = tempoMaximo;
        }
    }
}