using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPicareta : MonoBehaviour
{
    [SerializeField]
    private float dano = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            var inimigo = collision.gameObject.GetComponent<Monstro>();

            inimigo.TomarDano(dano);

        }
    }
}
