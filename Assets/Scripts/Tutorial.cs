using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorBlocks;
    public GameModeSwitcher gm;
    int currentPage = 0;

    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void NextPage()
    {
        tutorBlocks[currentPage].gameObject.SetActive(false);
        currentPage++;
        if (currentPage < tutorBlocks.Length)
        {
            tutorBlocks[currentPage].gameObject.SetActive(true);
        }
        else
        {
            gm.enabled = true;
            Time.timeScale = 1f;
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            NextPage();
        }
    }

}
