using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoParedes : MonoBehaviour
{
    public float fuerzaSalto, x, y, velocidadBajarsePared;
    public KeyCode salto, izquierda, derecha;

    Muros pared;
    Rigidbody2D rb;
    Vector2 direccion;
    float gravedadPorDefecto;
    bool puedeSaltarParedes = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("SetGravedadPorDefecto", 0.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(salto) && puedeSaltarParedes)
        {
            puedeSaltarParedes = false;
            SaltoPared();
        }

        if (Input.GetKeyDown(derecha) && puedeSaltarParedes && pared == Muros.izquierda)
            rb.velocity = new Vector2(velocidadBajarsePared, 0);

        if (Input.GetKeyDown(izquierda) && puedeSaltarParedes && pared == Muros.derecha)
            rb.velocity = new Vector2(-velocidadBajarsePared, 0);
    }

    public void SetSalto(bool puede, Muros lado)
    {
        if (puede) rb.gravityScale = 0.1f;
        else rb.gravityScale = gravedadPorDefecto;

        pared = lado;
        puedeSaltarParedes = puede;
    }

    public void SaltoPared()
    {
        if (pared == Muros.izquierda) direccion = new Vector2(x, y);
        else direccion = new Vector2(-x, y);
        rb.AddForce(direccion * fuerzaSalto, ForceMode2D.Impulse);
    }

    void SetGravedadPorDefecto()
    {
        gravedadPorDefecto = rb.gravityScale;
    }
}
