using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerEntity player;

    public PlayerEntity Player => player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerEntity>();
    }
}
