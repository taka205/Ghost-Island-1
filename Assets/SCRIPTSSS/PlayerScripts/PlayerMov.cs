using UnityEngine;

public class PlayerMov : MonoBehaviour

{
    public float speed = 5f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;
    }
}