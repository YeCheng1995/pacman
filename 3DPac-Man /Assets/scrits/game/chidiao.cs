using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chidiao : MonoBehaviour {

    private Vector3 pos;
	void Start () {
       
	}
    private void Awake()
    {
        pos= new Vector3(0.5f, 0.49f, 1f);
    }
    // Update is called once per frame
    void Update () {
		
	}
   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "gui")
        {
            if (super.insance.ison)
            {
                other.gameObject.transform.position = pos;
                super.insance.score += 500;
                other.GetComponent<EnemyMove>().z = 0;
            }
            else
            {
                gameObject.SetActive(false);
                super.insance.GamePanel.SetActive(false);
                Instantiate(super.insance.gameoverPrefab);
                Invoke("ReStart", 3f);
            }

        }
    }

    void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
