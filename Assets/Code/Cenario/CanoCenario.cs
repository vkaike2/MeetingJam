using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoCenario : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    public void AtivarCanvas()
    {
        canvas.SetActive(true);
    }
    public void DesativarCanvas()
    {
        canvas.SetActive(false);
    }
}
