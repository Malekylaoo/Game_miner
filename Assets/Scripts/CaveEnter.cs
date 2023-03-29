using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Заходим в шахту...");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Выходим из шахты...");
    }
}
