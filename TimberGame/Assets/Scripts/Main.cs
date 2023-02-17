using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject Player;
    public GameObject Idle;
    public GameObject Stomp;

    public GameObject Barrel;
    public GameObject EnemyRigth;
    public GameObject EnemyLeft;

    public Text scoreboard;

    float horizontalScale;

    private List<GameObject> listOfBarrels;

    bool playerRightSide;

    int score;

    bool gameStart;
    bool gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        listOfBarrels = new List<GameObject>();

        horizontalScale = transform.localScale.x;

        Stomp.SetActive(false);

        InitialBarrel();

        scoreboard.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        scoreboard.text = "Toque para Iniciar";
        scoreboard.fontSize = 21;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    RigthStomp();
                }
                else
                {
                    LeftStomp();
                }
                listOfBarrels.RemoveAt(0);
                RepositionBarrels();
                CheckStomp();
            }
        }
    }

    void LeftStomp()
    {
        playerRightSide = false;
        Idle.SetActive(false);
        Stomp.SetActive(true);

        Player.transform.position = new Vector2(-1.1f, Player.transform.position.y);
        Player.transform.localScale = new Vector2(horizontalScale, Player.transform.localScale.y);

        Invoke("IdleTrue", 0.25f);
        listOfBarrels[0].SendMessage("AnimStompRight");


    }
    void RigthStomp()
    {
        playerRightSide = true;
        Idle.SetActive(false);
        Stomp.SetActive(true);

        Player.transform.position = new Vector2(1.1f, Player.transform.position.y);
        Player.transform.localScale = new Vector2(-horizontalScale, Player.transform.localScale.y);

        Invoke("IdleTrue", 0.25f);
        listOfBarrels[0].SendMessage("AnimStompLeft");

    }
    void IdleTrue()
    {
        Stomp.SetActive(false);
        Idle.SetActive(true);
    }

    GameObject CreateNewBarrel(Vector2 bPosition)
    {
        GameObject newbarrel;

        if (Random.value > 0.5f || (listOfBarrels.Count <= 2))
        {
            newbarrel = Instantiate(Barrel);
        }
        else
        {
            if (Random.value > 0.5f)
            {
                newbarrel = Instantiate(EnemyLeft);
            }
            else
            {
                newbarrel = Instantiate(EnemyRigth);
            }
        }

        newbarrel.transform.position = bPosition;
        return newbarrel;
    }
    void InitialBarrel()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject rBarrel = CreateNewBarrel(new Vector2(0, -1.85f + (i * 0.87f)));
            listOfBarrels.Add(rBarrel);
        }
    }

    void RepositionBarrels()
    {
        GameObject objBarrel = CreateNewBarrel(new Vector2(0, -1.85f + (7 * 0.87f)));
        listOfBarrels.Add(objBarrel);
        for (int i = 0; i < 7; i++)
        {
            listOfBarrels[i].transform.position = new Vector2(listOfBarrels[i].transform.position.x, listOfBarrels[i].transform.position.y - 0.87f);
        }
    }
    void CheckStomp()
    {
        if (listOfBarrels[0].gameObject.CompareTag("Enemy"))
        {
            if ((listOfBarrels[0].name == "inimigoDir(Clone)" && playerRightSide) || (listOfBarrels[0].name == "inimigoEsq(Clone)" && !playerRightSide))
            {
                Debug.Log("errou");
                GameOver();
            }
            else
            {
                AddScore();
                Debug.Log("acertou");
            }
        }
        else
        {
            Debug.Log("acertou");
            AddScore();
        }
    }

    void AddScore()
    {
        score++;
        scoreboard.text = score.ToString();
        scoreboard.fontSize = 50;

    }

    void GameOver()
    {
        gameEnd = true;
        Stomp.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);
        Idle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);

        Player.GetComponent<Rigidbody2D>().isKinematic = false;

        if (playerRightSide)
        {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 3.0f);
            Player.GetComponent<Rigidbody2D>().AddTorque(-100.0f);
        }
        else
        {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, 3.0f);
            Player.GetComponent<Rigidbody2D>().AddTorque(100.0f);
        }
        Invoke("ReloadScene", 2);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
