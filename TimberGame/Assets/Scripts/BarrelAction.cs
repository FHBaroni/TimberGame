using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelAction : MonoBehaviour
{
 void AnimStompRight()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(10,2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(100.0f);
        Invoke("DestroyBarrel", 2.0f);
    }
    void AnimStompLeft()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(100.0f);
        Invoke("DestroyBarrel", 2.0f);
    }
    void DestroyBarrel()
    {
        Destroy(gameObject);
    }
}
