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
    public static float prevX, prevY;
    float prevXd = 0, prevYd = 0;
    Vector3 worldpos;
    Vector3 pos;

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
        worldpos = Camera.main.ScreenToWorldPoint(e.Input.ScreenPosition);
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


        worldpos = Camera.main.ScreenToWorldPoint(e.Input.ScreenPosition);
        pos = manager.maincamera.transform.position;

        //Debug.Log(Math.Abs(prevX - worldpos.x));

        if (prevXd != Math.Abs(prevX - worldpos.x))
        {
            if (pos.x + (prevX - worldpos.x) > 1f && pos.x + (prevX - worldpos.x) < 44f)
            {
                //if (Math.Abs(prevX - worldpos.x) > 0.1f)
                //{
                pos.x += (prevX - worldpos.x);
                //}
            }
        }

        if (prevYd != Math.Abs(prevY - worldpos.y))
        {
            if (pos.y + (prevY - worldpos.y) > -22.5f && pos.y + (prevY - worldpos.y) < -2.5f)
            {
                //if (Math.Abs(prevY - worldpos.y) > 0.1f)
                //{
                pos.y += (prevY - worldpos.y);
                //}
            }
        }

        //manager.maincamera.transform.position = pos;
        manager.maincamera.GetComponent<Rigidbody2D>().MovePosition(pos);



        prevXd = Math.Abs(prevX - worldpos.x);
        prevYd = Math.Abs(prevY - worldpos.y);
        prevX = worldpos.x;
        prevY = worldpos.y;

        //pos.x = e.Input.DeltaPosition.x;
        //pos.y = e.Input.DeltaPosition.y;
        //manager.maincamera.GetComponent<Rigidbody>().MovePosition(pos);

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
