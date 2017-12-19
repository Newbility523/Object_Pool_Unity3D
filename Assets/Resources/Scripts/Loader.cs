using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	private ObjectPool objectPool;

	// Use this for initialization
	void Start () {
		objectPool = ObjectPool.GetObjectPool;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.U)){
			GameObject go = objectPool.GetGameObject("Cube");
			go.transform.position = transform.position;
			go.AddComponent<Rigidbody>();
		}
		if(Input.GetKeyDown(KeyCode.I)){
			GameObject go = objectPool.GetGameObject("Sphere");
			go.transform.position = transform.position;
			go.AddComponent<Rigidbody>();
		}
		if(Input.GetKeyDown(KeyCode.O)){
			GameObject go = objectPool.GetGameObject("Capsule");
			go.transform.position = transform.position;
			go.AddComponent<Rigidbody>();
		}
	}
}
