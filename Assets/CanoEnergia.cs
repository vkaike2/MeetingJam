using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoEnergia : MonoBehaviour
{
    private bool keyPressed = false;
    public int comida = 1;



    private void OnTriggerStay2D(Collider2D collision)
    {
        float click = Input.GetAxisRaw("Fire1");
        if (click == 1 && !keyPressed && collision.gameObject.layer == 8&&comida>0)
        {
            var player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                player.ObterComida(comida);
                comida -= 1;
            }
            keyPressed = true;
        }

        if (click == 0 && keyPressed)
            keyPressed = false;

    }
}
