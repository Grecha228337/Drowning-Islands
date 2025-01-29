using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed;
    public float endY;
    public float startY;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if(transform.position.y <= endY)
        {
            Vector2 pos = new Vector2(0, startY);
            transform.position = pos;

        }
    }
}
