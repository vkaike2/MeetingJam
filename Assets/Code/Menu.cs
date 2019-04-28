using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Controles;
    public GameObject Inicial;
    public GameObject Caverna;
    public GameObject Intro;

    private AudioSource somclick;

    private void Awake()
    {
        somclick = GetComponent<AudioSource>();

        if (somclick == null)
            SceneManager.LoadScene("Cave");
    }

    public void ReiniciarScene()
    {
        SceneManager.LoadScene("Cave");
        somclick.Play(0);
    }

    public void Comecar()
    {
        Inicial.SetActive(false);
        Caverna.SetActive(false);
        Intro.SetActive(true);
        //SceneManager.LoadScene("Cave");
        somclick.Play(0);
    }

    public void IrParaControles()
    {
        somclick.Play(0);
        Inicial.SetActive(false);
        Controles.SetActive(true);
    }

    public void Voltar()
    {
        somclick.Play(0);
        Inicial.SetActive(true);
        Controles.SetActive(false);
    }

}
