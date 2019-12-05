using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
public class EnemyMove : MonoBehaviour
{
    public int z = 0;
    public GameObject playerer;

    public List<Transform> targetPos;
    private Seeker seeker;

    Vector3 targetPosition;

    CharacterController characterController;

    //计算出来的路线;
    Path path;

    //移动速度;
    [SerializeField]
    private float playerMoveSpeed = 5f;

    //当前点
    private int currentWayPoint = 0;

    private bool stopMove = true;

    //Player中心点;
    float playerCenterY = 1.0f;


    private float timer = 0;

    private float ambushtime = 0;

    private float num;
    [SerializeField]
    private float numtimer = 0;
    [SerializeField]
    private bool numOn = false;

    private Transform ambushPos;
    private bool ambushison = false;
    public float ambushtimevalue = 20;
    private bool Ason = false;

    public Transform cornerPos;

    private double chaseSecond;
    private double fastspeedPercent;
    private double slowsPercent;
    private int chasetimes;
    private int fastspeed;
    private int slowspeed;
    private float Bayetime;

    public enum state
    {
        chase,
        patrol,
        bank,
        ambush

    }
    private state states;
    void Start()
    {
        states = state.patrol;
    }

    public void Changestatus(state states)
    {
        switch (states)
        {
            case state.chase:
                Chase();
                break;
            case state.patrol:
                Patrol();
                break;
            case state.bank:
                Bank();
                break;
            case state.ambush:
                Ambush();
                break;


        }
    }
    private void Awake()
    {
        num = 5;
        playerCenterY = transform.localPosition.y;
        seeker = GetComponent<Seeker>();

    }
    // Update is called once per frame
    void Update()
    {
        numtimer += Time.deltaTime;
        if (numtimer >= 10)
        {
            numOn = true;
            numtimer = 0;
        }
        float i = Vector3.Distance(gameObject.transform.position, playerer.transform.position);
        if (i <= num)
        {
            if (super.insance.ison == false)
            {
                states = state.chase;
                Changestatus(states);
                if (numOn == true)
                {
                    StartCoroutine(gushi());
                    numOn = false;
                }
            }
            else
            {
                dada_1(corner());
                dada_2();
                StartCoroutine(guanbi());
            }
        }
        else
        {
            if (super.insance.ison == false)
            {
                ambushtime += Time.deltaTime;
            }
            if (ambushtime >= ambushtimevalue)
            {
                if (super.insance.ison == false)
                {
                    dada_1(dd());
                    dada_2();
                }
            }
            if (ambushtime < ambushtimevalue)
            {
                if (Ason)
                {
                    states = state.bank;
                    Changestatus(states);

                }
                else
                {
                    states = state.patrol;
                    Changestatus(states);

                }
            }

            if (ambushtime >= ambushtimevalue + 3f)
            {
                ambushtime = 0;
            }

        }
        Baye();
        Bayetime += Time.deltaTime;
        while (Bayetime >= 5)
        {
            presentBaye();
            Bayetime = 0;
        }
    }

    public void Baye()
    {
        if (states == state.chase)
        {
            chaseSecond += Time.deltaTime;
        }
        if (chaseSecond <= 2)
        {
            if (playerMoveSpeed > 7)
            {
                chasetimes += 1;
                fastspeed += 1;
                chaseSecond = 0;
                Debug.Log("fastspeed");
            }
            else
            {
                chasetimes += 1;
                slowspeed += 1;
                chaseSecond = 0;
                Debug.Log("slowspeed");
            }
        }
    }
    public void presentBaye()
    {
        if (fastspeed != 0 || slowspeed != 0)
        {
            if (fastspeed > slowspeed)
            {
                fastspeedPercent = fastspeed / chasetimes;
            }
            else
            {
                slowsPercent = slowspeed / chasetimes;
            }

            if (fastspeedPercent > slowsPercent)
            {
                playerMoveSpeed -= 0.5f;
                Debug.Log("playerMoveSpeed" + playerMoveSpeed);

            }
            else
            {
                playerMoveSpeed += 0.5f;
                Debug.Log("playerMoveSpeed" + playerMoveSpeed);
            }
            if (playerMoveSpeed <= 4)
            {
                playerMoveSpeed = 4;
            }
        }
    }
    IEnumerator gushi()
    {
        //增加决策的范围
        if (num < 8)
        {
            num += 0.5f;

        }
        else
        {
            num = 8;
            Debug.Log("dadadada");
        }
        //包围的速率
        if (ambushtimevalue > 10)
        {
            ambushtimevalue -= 1;
        }
        else
        {
            ambushtimevalue = 10;

        }
        //速度
        if (playerMoveSpeed < 8)
        {
            playerMoveSpeed += 0.5f;
            Debug.Log("dadadada");
        }
        else
        {
            playerMoveSpeed = 8;
        }

        yield return new WaitForSeconds(0.1f);
    }

    void Patrol()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos[z].position, Time.deltaTime * playerMoveSpeed);
        if (gameObject.transform.position == targetPos[z].position)
        {
            z++;
            if (targetPos.Count == z)
            {
                z = 0;

            }
        }
    }

    void Chase()
    {
        if (super.insance.ison == false)
        {
            dada_1(dd());
            if (playerer == null)
            {
                return;
            }
            dada_2();
            Ason = true;
        }
        else
        {
            StartCoroutine(guanbi());
        }
    }
    void Bank()
    {
        dada_1(zz());
        dada_2();
        if (gameObject.transform.position == targetPos[z].position)
        {
            Ason = false;
        }
    }
    void Ambush()
    {

    }
    IEnumerator guanbi()
    {
        yield return new WaitForSeconds(3f);
        super.insance.ison = false;

    }
    public void dada_1(IEnumerator da)
    {
        // Debug.Log(dis);
        timer += Time.deltaTime;

        if (timer >= 0.2)
        {
            StartCoroutine(da);
            timer = 0;
        }

    }


    public void OnPathComplete(Path p)
    {
        // Debug.Log("OnPathComplete error = " + p.error);

        if (!p.error)
        {
            currentWayPoint = 0;
            path = p;
            stopMove = false;
        }

        for (int index = 0; index < path.vectorPath.Count; index++)
        {
            //("path.vectorPath[" + index + "]=" + path.vectorPath[index]);
        }
    }
    IEnumerator dd()
    {
        yield return new WaitForSeconds(0.2f);
        if (playerer != null)
        {
            seeker.StartPath(transform.position, playerer.transform.position, OnPathComplete);
        }
    }
    IEnumerator zz()
    {
        yield return new WaitForSeconds(0.2f);
        if (playerer != null)
        {
            seeker.StartPath(transform.position, targetPos[z].transform.position, OnPathComplete);
        }
    }
    IEnumerator corner()
    {
        yield return new WaitForSeconds(0.2f);
        if (playerer != null)
        {
            seeker.StartPath(transform.position, cornerPos.position, OnPathComplete);
        }
    }
    public void dada_2()
    {
        if (path == null || stopMove)
        {
            return;
        }
        //根据Player当前位置和 下一个寻路点的位置，计算方向;
        Vector3 currentWayPointV = new Vector3(path.vectorPath[currentWayPoint].x, path.vectorPath[currentWayPoint].y + playerCenterY, path.vectorPath[currentWayPoint].z);
        Vector3 dir = (currentWayPointV - transform.position).normalized;
        //计算这一帧要朝着 dir方向 移动多少距离;
        dir *= playerMoveSpeed * Time.fixedDeltaTime;
        //计算加上这一帧的位移，是不是会超过下一个节点;
        float offset = Vector3.Distance(transform.localPosition, currentWayPointV);
        if (offset < 0.1f)
        {
            transform.localPosition = currentWayPointV;
            currentWayPoint++;
            if (currentWayPoint == path.vectorPath.Count)
            {
                stopMove = true;
                currentWayPoint = 0;
                path = null;
            }
        }
        else
        {
            if (dir.magnitude > offset)
            {
                Vector3 tmpV3 = dir * (offset / dir.magnitude);
                dir = tmpV3;
                currentWayPoint++;
                if (currentWayPoint == path.vectorPath.Count)
                {
                    stopMove = true;
                    currentWayPoint = 0;
                    path = null;
                }
            }
            transform.localPosition += dir;
        }
    }

}