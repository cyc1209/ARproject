using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;
    // Start is called before the first frame update
    private Pose startPose;
    public bool firstFloor = false;

    public int bpm = 115;

    public Transform horizontalPos;
    public Transform verticalPos;

    private int stack = 0;
    private int level = 0;

    public float leftover = 0;
    internal int combo = 0;
    public int maxCom = 0;
    internal int TotalStack = 0;
    [SerializeField]
    private GameObject eye;

    public int Stack { get => stack; set => stack = value; }
    public Pose StartPose { get => startPose; set => startPose = value; }
    public int Level { get => level; set => level = value; }
    public int score { get;  set; }

    void Awake()
    {
        if (GameManager.instance == null)
            GameManager.instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(maxCom < combo)
        {
            maxCom = combo;
        }
     
    }
    public void eyeUP()
    {
        eye.transform.position += new Vector3(0, 0.03f, 0);
    }
    public void calcScore()
    {
        score += (level + 1) + combo;
    }

}
