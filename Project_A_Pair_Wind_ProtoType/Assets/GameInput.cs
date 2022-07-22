using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public bool right;
    public bool left;
    public bool up;
    public bool down;
    public bool keyA;
    public bool keyS;
    public bool keyD;
    public bool keyW;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) { up = true;}
        else { up = false;}

        if(Input.GetKey(KeyCode.DownArrow)) {down = true;}
        else {down = false;}

        if(Input.GetKey(KeyCode.LeftArrow)) {left = true;}
        else {left = false;}

        if(Input.GetKey(KeyCode.RightArrow)) {right = true;}
        else {right = false;}


        if(Input.GetKey(KeyCode.A)) {keyA = true;}
        else {keyA = false;}

        if(Input.GetKey(KeyCode.S)) {keyS = true;}
        else {keyS = false;}

        if(Input.GetKey(KeyCode.D)) {keyD = true;}
        else {keyD = false;}

        if(Input.GetKey(KeyCode.W)) {keyW = true;}
        else {keyW = false;}
    }
}
