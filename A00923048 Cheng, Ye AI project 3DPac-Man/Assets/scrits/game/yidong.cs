using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yidong : MonoBehaviour {

    private float zz = 0;
    private Rigidbody rig;
    public static float Pspeed = 10;

	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	
	void Update () {
        Debug.Log("abc" + yidong.Pspeed);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, 0, y) * Time.deltaTime * Pspeed);
        rig.velocity = new Vector3(0, 0, 0);
    }
}
