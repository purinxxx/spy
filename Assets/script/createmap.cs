using UnityEngine;
using System.Collections;

public class createmap : MonoBehaviour {
    GameObject masu;
    GameObject m;

    // Use this for initialization
    
    void Start () {
        /*
        masu = (GameObject)Resources.Load ("masu");
        for (int i = 0; i < data.map1.Length; i++) {
            string[] l = new string[48];
            for (int j = 0; j < data.map1[i].Length; j++){
                l[j] = data.map1[i].Substring(j, 1);
                if (l[j] == "0"){
                    float x = (j*100 + 50)/100;
                    float y = -1*(i*100 + 50)/100;
                    m = (GameObject)Instantiate(masu, new Vector3(x, y, 0), Quaternion.identity);
                    m.name = masu.name;
                }
                //Debug.Log(l[j]);
                // i=0~26 j=0~47
            }
        }
        */

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
