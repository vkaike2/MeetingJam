using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoracaoUI : MonoBehaviour
{
    private Animator anim;

    private int ANIM_EXPLODIR;

    void Start()
    {
        anim = GetComponent<Animator>();
        ANIM_EXPLODIR = Animator.StringToHash("Explodir");
    }

    public void Explodir()
    {
        anim.SetBool(ANIM_EXPLODIR, true);
    }

    public void DestruirObjeto()
    {
        gameObject.SetActive(false);
    }
}
