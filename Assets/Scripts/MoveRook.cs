using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRook : MonoBehaviour {

    const float timeLimit = 1;
    float passedTime;
    float fracTime;
    bool areMoving = false;
    Vector3 startPosition;
    Vector3 endPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (areMoving)
        {
            passedTime += Time.deltaTime;

            if (passedTime >= timeLimit)
            {
                fracTime = 1;
            }
            else
            {
                fracTime = passedTime / timeLimit;
            }
            transform.position = Vector3.Lerp(startPosition, endPosition, fracTime);
            if (transform.position == endPosition)
            {
                areMoving = false;
            }
        }

	}
    void startMove()
    {
        areMoving = true;
        passedTime = 0f;
    }
}
