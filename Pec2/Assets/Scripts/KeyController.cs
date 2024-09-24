using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{

    private bool keyPicked = false;

    public bool IsKeyPicked() => keyPicked;

    public void PickKey()
    {
        keyPicked = true;
    }
}
