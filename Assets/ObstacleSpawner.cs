using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Player2D player;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float lane01, lane02, lane03;
    [SerializeField] float spawnInterval = 1.5f;
    [SerializeField] float spawnDistance = 30f;
    [SerializeField] float horizonY;
    public float minSpeed, maxSpeed;

    float timer;
    void Start()
    {
        player = FindAnyObjectByType<Player2D>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    /* void SpawnObstacles()
    {
        GameObject obstacle = Instantiate(obstaclePrefab);

        // Put the obstacle in one of the lanes
        obstacle.transform.position = new Vector3(GetRandomLane(), player.groundY, 0);

        Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
        obstacleScript.horizonY = horizonY;

        obstacleScript.zPos = spawnDistance;
        obstacleScript.speed = GetRandomSpeed();
    } */
    void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab);

        Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();

        SetLane(obstacleScript);

        obstacleScript.horizonY = horizonY;
        obstacleScript.zPos = spawnDistance;
        obstacleScript.speed = GetRandomSpeed();
    }

    void SetLane(Obstacle obstacle)
    {
        int lane = Random.Range(0, 3);

        obstacle.lane = lane;

        switch (lane)
        {
            case 0:
                obstacle.transform.position =
                    new Vector3(lane01, player.groundY, 0);
                break;

            case 1:
                obstacle.transform.position =
                    new Vector3(lane02, player.groundY, 0);
                break;

            case 2:
                obstacle.transform.position =
                    new Vector3(lane03, player.groundY, 0);
                break;
        }
    }


    float GetRandomLane()
    {
        switch (Random.Range(0, 3))
        {
            case 0: return lane01;
            case 1: return lane02;
            default: return lane03;
        }
    }
    float GetRandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }
}



/* void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab);
        Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
        // obstacleScript.spawnZ = spawnZ;
        obstacleScript.horizonY = horizonY;
        obstacleScript.groundY = player.groundY;
        obstacleScript.laneWidth = player.laneOffset;

        obstacleScript.speed = GetRandomSpeed();

        obstacleScript.lane = Random.Range(0, 3);
    } */