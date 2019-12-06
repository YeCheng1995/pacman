using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class super : MonoBehaviour {
    public List<GameObject> douzi=new List<GameObject>();


    public GameObject StartPanel;
    public GameObject GamePanel;
    public GameObject startCountDownPrefab;
    public GameObject gameoverPrefab;
    public GameObject WinPrefab;
    public AudioClip startClip;
    public Text remaintext;
    public Text nowtext;
    public Text scoretext;

    private int pacdotNum = 0;
    private int nowEat = 0;
    public int score = 0;

    

    public GameObject[] startplayer;
    public bool ison = false;
    public static super insance;
    private void Awake()
    {
        SetGameState(false);
        insance = this;

        foreach (Transform  z in GameObject.Find("douzi").transform)
        {
            douzi.Add(z.gameObject);
        }
        pacdotNum = gameObject.transform.childCount;
       
    }

    // Update is called once per frame
    void Update () {
        if (nowEat == pacdotNum && startplayer[4].GetComponent<yidong>().enabled != false)
        {
            GamePanel.SetActive(false);
            Instantiate(WinPrefab);
            StopAllCoroutines();
            SetGameState(false);
        }
        if (nowEat == pacdotNum)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (GamePanel.activeInHierarchy)
        {
            remaintext.text = "Remain:\n\n"+(pacdotNum - nowEat);
            nowtext.text = "Eaten:\n\n" +  nowEat;
            scoretext.text = "Score:\n\n" + score;
        }
    }
    
    public void OnEatPacdot(GameObject zz)
    {
        nowEat++;
        score += 100;
        douzi.Remove(zz);
    }
    public void OnsuperEatPacdot(GameObject zz)
    {
        nowEat++;
        score += 200;
        douzi.Remove(zz);
    }
    public void OnEatSuperPacdot()
    {
        
        Invoke("CreateSuperPacdot", 10f);
    }
    void CreateSuperPacdot()
    {
       
        if (douzi.Count < 5)
        {
            return;
        }
        int tempIndex = Random.Range(0, douzi.Count);
        douzi[tempIndex].transform.localScale = new Vector3(1f, 1f, 1f);
        douzi[tempIndex].GetComponent<Pacdot>().superPacdot = true;
        OnEatSuperPacdot();
      

    }

    private void SetGameState(bool state)
    {
        startplayer[0].GetComponent<EnemyMove>().enabled = state;
        startplayer[1].GetComponent<EnemyMove>().enabled = state;
        startplayer[2].GetComponent<EnemyMove>().enabled = state;
        startplayer[3].GetComponent<EnemyMove>().enabled = state;
        startplayer[4].GetComponent<yidong>().enabled = state;
        startplayer[5].GetComponent<guardmove>().enabled = state;
        startplayer[6].GetComponent<bullet>().enabled = state;


    }

    public void OnStartButton ()
    {
        StartCoroutine(PlayStartCountDown());
        AudioSource.PlayClipAtPoint(startClip,new Vector3(5,13,0));
        StartPanel.SetActive(false);
       
    }
    public void OnExitButton()
    {

        Application.Quit();
    }
    IEnumerator PlayStartCountDown()
    {
        GameObject go = Instantiate(startCountDownPrefab);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);
        Invoke("CreateSuperPacdot", 10f);
        GamePanel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
}
