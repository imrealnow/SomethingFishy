using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPositioner : MonoBehaviour {

	public float offset;
    public SharedFloat xValue;


	void FixedUpdate () {
		transform.position = new Vector3(xValue.Value + offset, transform.position.y, 0);
	}
}
