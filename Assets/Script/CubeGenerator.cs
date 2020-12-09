using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField]
    private Cube cubePrefab;
    [SerializeField]
    private Direction direction;

    public void GenerateCube() 
    {
        float random = Random.Range(-2.0f, 2.0f);
        if (-1.0f <= random && random < 0)
            direction = Direction.X;
        else if (-2.0f <= random && random < -1.0f)
            direction = Direction.Z;
        else if (0 <= random && random < 1.0f)
            direction = Direction.MZ;
        else
            direction = Direction.MX;

        var go = Instantiate(cubePrefab); 
        if(direction == Direction.Z)
            go.transform.position = new Vector3(GameManager.instance.StartPose.position.x, GameManager.instance.StartPose.position.y+((GameManager.instance.Stack+1)*0.03f), GameManager.instance.StartPose.position.z - (0.9f+ GameManager.instance.leftover));
        else if(direction == Direction.X)
            go.transform.position = new Vector3(GameManager.instance.StartPose.position.x - (0.9f + GameManager.instance.leftover), GameManager.instance.StartPose.position.y + ((GameManager.instance.Stack + 1) * 0.03f), GameManager.instance.StartPose.position.z);
        else if (direction == Direction.MX)
            go.transform.position = new Vector3(GameManager.instance.StartPose.position.x + (0.9f + GameManager.instance.leftover), GameManager.instance.StartPose.position.y + ((GameManager.instance.Stack + 1) * 0.03f), GameManager.instance.StartPose.position.z);
        else
            go.transform.position = new Vector3(GameManager.instance.StartPose.position.x, GameManager.instance.StartPose.position.y + ((GameManager.instance.Stack + 1) * 0.03f), GameManager.instance.StartPose.position.z + (0.9f + GameManager.instance.leftover));

        GameManager.instance.Stack++;
        GameManager.instance.TotalStack++;
        go.Direction = direction;
    }

}
