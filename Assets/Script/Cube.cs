using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update

    public static Cube CurrentCube { get; private set; }
    public static Cube LastCube { get; private set; }
    public Direction Direction { get; set; }

    [SerializeField]
    private float speed = 0.8625f;

    private void OnEnable()
    {
        speed += GameManager.instance.Level * 0.3f;
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Floor(Clone)").GetComponent<Cube>();
            LastCube.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0,0.5f), UnityEngine.Random.Range(0, 0.5f), UnityEngine.Random.Range(0, 0.5f));
        }

        CurrentCube = this;
        GetComponent<Renderer>().material.color = LastCube.GetComponent<Renderer>().material.color * 1.1f;
        if(LastCube.GetComponent<Renderer>().material.color.r > 0.8f || LastCube.GetComponent<Renderer>().material.color.g > 0.8f|| LastCube.GetComponent<Renderer>().material.color.b > 0.8f)
            GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0, 0.5f), UnityEngine.Random.Range(0, 0.5f), UnityEngine.Random.Range(0, 0.5f));

        transform.localScale = LastCube.transform.localScale;

    }

    internal void Stop()
    {

        speed = 0;
        float hangover;
        if (Direction == Direction.Z)
            hangover = transform.position.z - LastCube.transform.position.z;
        else if(Direction == Direction.X)
            hangover = transform.position.x - LastCube.transform.position.x;
        else if (Direction == Direction.MX)
            hangover = (transform.position.x - LastCube.transform.position.x);
        else
            hangover = (transform.position.z - LastCube.transform.position.z);
        GameManager.instance.leftover = hangover;

        if (Mathf.Abs(hangover) <= (Direction == Direction.Z || Direction == Direction.MZ ? LastCube.transform.localScale.z : LastCube.transform.localScale.x)/6.0f)
        {
            hangover = 0.0f;
            GameManager.instance.combo++;
            if(GameManager.instance.combo > 1)
              UIManager.instance.StartCombo();
        }
        else
        {
            GameManager.instance.combo = 0;
            UIManager.instance.
                EndCombo();
        }

        if(Mathf.Abs(hangover) >= (Direction == Direction.Z || Direction == Direction.MZ ? LastCube.transform.localScale.z: LastCube.transform.localScale.x))
        {
            LastCube = null;
            CurrentCube = null;
            UIManager.instance.GameOver();

        }


        if (Direction == Direction.Z || Direction == Direction.MZ)
            SplitCubeZ(hangover);
        else
            SplitCubeX(hangover);

        LastCube = this;
    }

    private void SplitCubeZ(float hangover)
    {
        if (LastCube == null)
            return;
        float newZ = LastCube.transform.localScale.z - Mathf.Abs(hangover);

        float newPosZ = LastCube.transform.position.z + (hangover/2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosZ); 
        Pose pose = GameManager.instance.StartPose;
        pose.position.z = newPosZ;
        GameManager.instance.StartPose = pose;
    }

    private void SplitCubeX(float hangover)
    {
        if (LastCube == null)
            return;
        float newX = LastCube.transform.localScale.x - Mathf.Abs(hangover);

        float newPosX = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newX, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
        Pose pose = GameManager.instance.StartPose;
        pose.position.x = newPosX;
        GameManager.instance.StartPose = pose; 
    }

    private void Update()
    {
        if(Direction == Direction.Z)
            transform.position += transform.forward * Time.deltaTime * speed;
        else if (Direction == Direction.X)
            transform.position += transform.right * Time.deltaTime * speed;
        else if (Direction == Direction.MX)
            transform.position -= transform.right * Time.deltaTime * speed;
        else
            transform.position -= transform.forward * Time.deltaTime * speed;
    }

}
