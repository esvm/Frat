using UnityEngine;
using System.Collections;

public class DestroyObjetos : MonoBehaviour {

	public static float speed = 1f;
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.left * speed;

        if (transform.position.x < -70)
        {
			 Destroy(transform.gameObject);
        }
	}
}
