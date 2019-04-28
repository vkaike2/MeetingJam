using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    [SerializeField]
    private float velocidade = 3;
    Rigidbody2D rigibodyCol;
    [SerializeField]
    private float vida = 2;

   
    private void Start()
    {
        rigibodyCol = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rigibodyCol.velocity = new Vector2(velocidade, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer ==11)
        {
                     
            velocidade *= -1;
        }
    }


    public void IniciaComADirecao(int valor)
    {
        velocidade *= valor;
    }

    public void TomarDano(float dano)
    {
        vida -= dano;
    }
}
