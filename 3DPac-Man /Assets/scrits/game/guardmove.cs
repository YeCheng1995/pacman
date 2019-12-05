using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class guardmove : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed = 4;
    public GameObject[] pos;
    int i = 0;
    float des;

    public Transform zidan;
    GameObject player;
    Rigidbody playerrigidbody;

    public GameObject playerer;
    public float distance;
    private float time;

    private int place1;
    private int place2;
    private int place3;
    private int place4;
    float[] places = new float[4];
    float[] present = new float[4];
    private int total;
    private double max;
    private int maxindex;
    private bool ok = false;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Start()
    {
        //playerrigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        distance = Vector3.Distance(gameObject.transform.position, playerer.transform.position);
        if(distance > 5)
        {
            move();
        }
        else
        {
            StartCoroutine(NGram());

        }

    }
    public void move()
    {
        this.transform.LookAt(pos[i].transform);
        des = Vector3.Distance(this.transform.position, pos[i].transform.position);
        transform.position = Vector3.MoveTowards(this.transform.position, pos[i].transform.position, Time.deltaTime * moveSpeed);
        if (des < 0.1f && i < 5)
        {
            i++;
            if (i == 4)
            {
                if (i == maxindex + 1 && ok)
                {
                    Instantiate(zidan, transform.position, zidan.transform.rotation);
                    Debug.Log("cycycycycymax" + maxindex);
                }
                i = 0;
            }
            if(i == maxindex+1 && ok)
            {
                Instantiate(zidan, transform.position, zidan.transform.rotation);
                Debug.Log("cycycycycymax"+ maxindex);
            }
        }
    }
    IEnumerator NGram()
    {
        yield return new WaitForSeconds(0.2f);
        while (time >= 1)
        {
            Instantiate(zidan, transform.position, zidan.transform.rotation);
            if (player.transform.position.x > 0 && player.transform.position.z > 1)
            {
                places[3]++;
                total++;
                for (int x = 0; x < 4; x++)
                {
                    //present[x] = (places[x] / total) * 100;
                    present[x] = Mathf.Round((places[x] / total) * 100.0f);
                    Debug.Log("present" + x + present[x]);
                }

            }
            if (player.transform.position.x > 0 && player.transform.position.z < 1)
            {
                places[2]++;
                total++;
                for (int x = 0; x < 4; x++)
                {
                    present[x] = Mathf.Round((places[x] / total) * 100.0f); 
                    Debug.Log("present" + x + present[x]);
                }

            }
            if (player.transform.position.x < 0 && player.transform.position.z < 1)
            {
                places[1]++;
                total++;
                for (int x = 0; x < 4; x++)
                {
                    present[x] = Mathf.Round((places[x] / total) * 100.0f);
                    Debug.Log("present" + x + present[x]);
                }

            }
            if (player.transform.position.x < 0 && player.transform.position.z > 1)
            {
                places[0]++;
                total++;
                for (int x = 0; x < 4; x++)
                {
                    present[x] = Mathf.Round((places[x] / total) * 100.0f);
                    Debug.Log("present" + x + present[x]);
                }

            }
            for (int index = 0; index < 4; index++)
            {
                if (present[index] >= present.Max())
                {
                    maxindex = index;
                    ok = true;
                }
            }

            time = 0;
        }

    }
}
