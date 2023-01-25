using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnner : MonoBehaviour
{
    public PowerupEnttity PowerGo;
   
    public float durations;
    private void Start()
    {
        switch (PowerGo)
        {
            case PowerupEnttity.None:
                
            break;

            case PowerupEnttity.Extralife:
                 
            break;


        }
    }

}

[System.Serializable]
public enum PowerupEnttity
{
    None,
    Extralife,
    SpeedBall,
    Magnetball,
    Slowball,
    Increasepaddlesize,
    ThruBreak,
    KillPaddle,
    SplitBall,
    Shootingpaddle,
    FireBall,
    LevelWarp,
    Shrinkpaddle,
    FallingBricks
}
