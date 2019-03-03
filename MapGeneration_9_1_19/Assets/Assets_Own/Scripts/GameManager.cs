using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

     BoardManager boardScript;

    private int level = 3;

    private void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        Init();
    }
    void Init()
    {
        boardScript.setUpScene(level);
    }
    
}
