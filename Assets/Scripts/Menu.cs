using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool opne;
    public void Opne()
    {
        opne = true;
        gameObject.SetActive(true);

    }

    public void Close()
    {
        opne = false;
        gameObject.SetActive(false);

    }

}
