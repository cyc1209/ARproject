using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;

    public CubeGenerator cubeGenerator;
    public Text fF;
    public Text stack;
    public Text score;
    public Text combo;
    public Text maxCombo;
    public GameObject floorButton;
    public GameObject actionButton;
    public GameObject gameOver;


    //싱글톤
    void Awake()
    {
        if (UIManager.instance == null)
            UIManager.instance = this;

        cubeGenerator = GameObject.Find("CubeGenerator").GetComponent<CubeGenerator>();
    }

    void Start()
    {
        floorButton.SetActive(true);
        actionButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        fF.text = GameManager.instance.firstFloor.ToString();
        stack.text = GameManager.instance.TotalStack.ToString();
        score.text = "Total Score:" + GameManager.instance.score.ToString();
    }


    //바닥 생성 버튼
    public void OnFloorButtonTouched()
    {
        GameObject target = GameObject.Find("Example");
        target.SendMessage("OnPlaneDetected");
    }

    //액션(게임 진행 버튼)
    public void OnActionButtonTouched()
    {
        Cube.CurrentCube.Stop();
        cubeGenerator.GenerateCube();
        GameManager.instance.calcScore();
        if (GameManager.instance.Stack >= 20)
        {
            GameManager.instance.eyeUP();
        }
    }
 
    public void MaxCombo()
    {
        maxCombo.text = GameManager.instance.maxCom.ToString()+" Combo!";
    }

    internal void GameOver()
    {
        gameOver.SetActive(true);
        actionButton.SetActive(false);
        MaxCombo();
    }

    public void OnReButtonTouched()
    {
        gameOver.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void StartCombo()
    {
        combo.gameObject.SetActive(true);
        combo.text = GameManager.instance.combo.ToString() + " Combo!";
    }
    public void EndCombo()
    {
        combo.gameObject.SetActive(false);
    }
}
