using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Swipe : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private List<string> logs = new List<string>();
    const int MAX_LOGS = 30;
    float prevX, prevY;

    void OnEnable()
    {
        TouchManager.Instance.Drag += OnSwipe;
        TouchManager.Instance.TouchStart += OnTouchStart;
        TouchManager.Instance.TouchEnd += OnTouchEnd;
        TouchManager.Instance.FlickStart += OnFlickStart;
        TouchManager.Instance.FlickComplete += OnFlickComplete;
    }

    void OnDisable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.Drag -= OnSwipe;
            TouchManager.Instance.TouchStart -= OnTouchStart;
            TouchManager.Instance.TouchEnd -= OnTouchEnd;
            TouchManager.Instance.FlickStart -= OnFlickStart;
            TouchManager.Instance.FlickComplete -= OnFlickComplete;
        }
    }

    void OnTouchStart(object sender, CustomInputEventArgs e)
    {
        string text = string.Format("OnTouchStart X={0} Y={1}", e.Input.ScreenPosition.x, e.Input.ScreenPosition.y);
        //Debug.Log(text);
        Vector3 worldpos = Camera.main.ScreenToWorldPoint(e.Input.ScreenPosition);
        prevX = worldpos.x;
        prevY = worldpos.y;
    }

    void OnTouchEnd(object sender, CustomInputEventArgs e)
    {
        string text = string.Format("OnTouchEnd X={0} Y={1}", e.Input.ScreenPosition.x, e.Input.ScreenPosition.y);
        //Debug.Log(text);
        
    }

    void OnSwipe(object sender, CustomInputEventArgs e)
    {
        string text = string.Format("OnSwipe Pos[{0},{1}] Move[{2},{3}]", new object[] {
                        e.Input.ScreenPosition.x.ToString ("0"),
                        e.Input.ScreenPosition.y.ToString ("0"),
                        e.Input.DeltaPosition.x.ToString ("0"),
                        e.Input.DeltaPosition.y.ToString ("0")
                });
        //Debug.Log(text);

        Vector3 worldpos = Camera.main.ScreenToWorldPoint(e.Input.ScreenPosition);
        Vector3 pos = manager.maincamera.transform.position;
        if(pos.x + (prevX - worldpos.x) > -1 && pos.x + (prevX - worldpos.x) < 46) pos.x += (prevX - worldpos.x);
        if (pos.y + (prevY - worldpos.y) > -23 && pos.y + (prevY - worldpos.y) < -2) pos.y += (prevY - worldpos.y);
        manager.maincamera.transform.position = pos;
        

        prevX = worldpos.x;
        prevY = worldpos.y;
    }

    void OnFlickStart(object sender, FlickEventArgs e)
    {
        string text = string.Format("OnFlickStart [{0}] Speed[{1}] Accel[{2}] ElapseTime[{3}]", new object[] {
                        e.Direction.ToString (),
                        e.Speed.ToString ("0.000"),
                        e.Acceleration.ToString ("0.000"),
                        e.ElapsedTime.ToString ("0.000"),
                });
        //Debug.Log(text);
    }

    void OnFlickComplete(object sender, FlickEventArgs e)
    {
        string text = string.Format("OnFlickComplete [{0}] Speed[{1}] Accel[{2}] ElapseTime[{3}]", new object[] {
                        e.Direction.ToString (),
                        e.Speed.ToString ("0.000"),
                        e.Acceleration.ToString ("0.000"),
                        e.ElapsedTime.ToString ("0.000")
                });
        //Debug.Log(text);
        

    }
}
