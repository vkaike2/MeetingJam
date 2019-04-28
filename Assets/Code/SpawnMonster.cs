using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField]
    private float coolDown;
    [SerializeField]
    private GameObject monstro;

    // Update is called once per frame
    void Update()
    {
        SpawnaMonstro();
        coolDown -= Time.deltaTime;

    }
    private void SpawnaMonstro()
    {
        if (coolDown <= 0)
        {
            var monstroSpawnado = Instantiate(monstro, transform.position, transform.rotation);
            coolDown = 5f;
            int numeroAleatorio = Random.Range(1, 2);
            monstro.GetComponent<Monstro>().IniciaComADirecao(numeroAleatorio == 1 ? 1 : -1);
        }

    }

}
