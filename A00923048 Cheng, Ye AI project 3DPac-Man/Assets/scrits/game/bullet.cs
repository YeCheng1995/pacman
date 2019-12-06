using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    public float speed = 5f;

    protected Transform m_transform;

    GameObject player;
    private float time;
    private bool ok;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void Start()
    {
        m_transform = this.transform;
        Destroy(this.gameObject, 2f);
    }

    void Update()
    {
        if (player)
        {
            m_transform.LookAt(player.transform);
            m_transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }

        if (!ok)
        {
            time += Time.deltaTime;
            while (time >= 2)
            {
                time = 0;
                yidong.Pspeed = 10;
                ok = true;
            }
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
           yidong.Pspeed = 7;
           Debug.Log("abc" + yidong.Pspeed);
           Destroy(this.gameObject);
           ok = false;
        }
    }
    
}
