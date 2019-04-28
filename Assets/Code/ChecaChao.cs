using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecaChao : MonoBehaviour
{
    public bool EstaNoChao = false;
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player.PlayAndando();
        EstaNoChao = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EstaNoChao = false;
    }

}
