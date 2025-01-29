using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public PlayerController player;
    public void Restart()
    {
        player.Death();
    }
}
