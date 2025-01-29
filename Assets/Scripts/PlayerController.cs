using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;
    public GameObject map;
    public DeathScreen deathScreen;
    public NarrowManager narrowManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetFloat("Vertical", moveInput.y);
        }
        anim.SetFloat("Speed", moveInput.sqrMagnitude);

        if(Input.GetKeyDown(KeyCode.M))
        {
            map.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            map.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void Death()
    {
        speed = 0;
        anim.SetTrigger("Death");
    }
    public void CreateDeathScreen()
    {
        deathScreen.Appear();
        //Страшно. Не смотрите. Мне уже лень думоть
        deathScreen.SetInfo(narrowManager.secPassed, GameManager.current.resourceCollected[ResourceType.Wood],
            GameManager.current.resourceCollected[ResourceType.Rock], GameManager.current.resourceCollected[ResourceType.Planks],
            GameManager.current.resourceCollected[ResourceType.Money], GameManager.current.resourceCollected[ResourceType.Ore],
            GameManager.current.resourceCollected[ResourceType.Nails], GameManager.current.buildingCreated);
        GameManager.current.toolSelectSystem.SelectTool(ToolType.None);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
