using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 5;
    public float fuerzaDeSalto = 2;
    public int maxDobleSalto = 1; // Máximo número de dobles saltos permitidos.
    public LayerMask capaTerreno;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private int contadorDobleSalto = 0;
    private Animator animator;
    //private bool orientacion = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcesarMovimiento();
    }

    void ProcesarMovimiento()
    {
        if (EstaEnElTerreno())
        {
            contadorDobleSalto = 0; // Restablecer la cuenta de dobles saltos cuando está en el suelo.
        }

        float entradaMovimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(entradaMovimiento * velocidadMovimiento, rb.velocity.y);

        //Orientacion(entradaMovimiento);

        if (entradaMovimiento != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (EstaEnElTerreno() || contadorDobleSalto < maxDobleSalto)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); // Restablecer la velocidad vertical antes del salto.
                rb.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
                contadorDobleSalto++; // Incrementar la cuenta de dobles saltos.
            }
        }
    }

    /*void Orientacion(float entradaMovimiento)
    {
        if (entradaMovimiento > 0 && !orientacion || entradaMovimiento < 0 && orientacion)
        {
            orientacion = !orientacion;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }*/

    private bool EstaEnElTerreno()
    {
        RaycastHit2D centro = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.1f, capaTerreno);
        return centro.collider != null;
    }
}
