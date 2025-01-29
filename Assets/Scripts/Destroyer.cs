using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]private float lifetime;
    private void Start()
    {
        Invoke("Destroy", lifetime);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
