using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour {

    public bool superPacdot = false;
    private void Awake()
    {
       
    }

    void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (superPacdot)
            {
               
                super.insance.ison = true;
                StartCoroutine(huifu());
                super.insance.OnsuperEatPacdot(gameObject);
                Destroy(gameObject);
            }
            else
            {
                super.insance.OnEatPacdot(gameObject);
                Destroy(gameObject);
            }

        }
    }

    IEnumerator huifu()
    {
        yield return new WaitForSeconds(5f);
        super.insance.ison = false;
        Debug.Log("zzz");
    }
}

   
