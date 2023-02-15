using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject Player;
    public GameObject Idle;
    public GameObject Stomp;

    float horizontalScale;
    // Start is called before the first frame update
    void Start()
    {
        horizontalScale = transform.localScale.x;

        Stomp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
        }
    }

    void LeftStomp()
    {
        Idle.SetActive(false);
        Stomp.SetActive(true);

        Player.transform.position = new Vector2(-1.1f, Player.transform.position.y);
        Player.transform.localScale = new Vector2(horizontalScale, Player.transform.localScale.y);

        Invoke("IdleTrue",0.25f);

    }
    void RigthStomp()
    {
        Idle.SetActive(false);
        Stomp.SetActive(true);

        Player.transform.position = new Vector2(1.1f, Player.transform.position.y);
        Player.transform.localScale = new Vector2(-horizontalScale, Player.transform.localScale.y);

        Invoke("IdleTrue", 0.25f);
    }
    void IdleTrue()
    {
        Stomp.SetActive(false);
        Idle.SetActive(true);
    }
}
