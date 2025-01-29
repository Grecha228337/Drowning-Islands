using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI timeSurvived;
    public TextMeshProUGUI wood;
    public TextMeshProUGUI rocks;
    public TextMeshProUGUI planks;
    public TextMeshProUGUI money;
    public TextMeshProUGUI ore;
    public TextMeshProUGUI nails;
    public TextMeshProUGUI builded;

    public GameObject[] objToView;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Appear()
    {
        anim.SetTrigger("Appear");
    }
    public void SetInfo(int time, int w, int r, int p, int m, int o, int n, int buildings)
    {
        int min = time / 60;
        string minStr = min > 9 ? min.ToString() : "0" + min;
        int sec = time % 60;
        string secStr = sec > 9 ? sec.ToString() : "0" + sec;
        timeSurvived.text = "Время: " + minStr + ":" + secStr;

        wood.text = w.ToString();
        rocks.text = r.ToString();
        planks.text = p.ToString();
        money.text = m.ToString();
        ore.text = o.ToString();
        nails.text = n.ToString();

        builded.text = "Построено зданий: " + buildings.ToString();

        //StartCoroutine(ViewInfo())
    }

    public IEnumerator ViewInfo()
    {
        for (int i = 0; i < objToView.Length; i++)
        {
            yield return new WaitForSeconds(0.4f);
            objToView[i].gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Destroy");
        }
    }
}
