using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabDestroy : MonoBehaviour {

	public float liveTime = 5.0f;
	private float currentTime;
	private ObjectPool objectPool;

	// Use this for initialization
	void Start () {
		currentTime = 0.0f;	
		objectPool = ObjectPool.GetObjectPool;
		liveTime = 5.0f;
		Debug.Log(liveTime);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime ;
		if (currentTime > liveTime) {
			currentTime = 0;
			objectPool.SaveGameObject(name.Replace("(Clone)", ""), this.gameObject);	
		}
	}
}
