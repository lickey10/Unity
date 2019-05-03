using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SgLib;
using System.Collections.Generic;

public enum GameState
{
    Prepare,
    Playing,
    Paused,
    PreGameOver,
    GameOver
}

public class GameManager : MonoBehaviour
{

    public static int GameCount
    { 
        get { return _gameCount; } 
        private set { _gameCount = value; } 
    }

    private static int _gameCount = 0;

    public static event System.Action<GameState, GameState> GameStateChanged = delegate {};

    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        private set
        {
            if (value != _gameState)
            {
                GameState oldState = _gameState;
                _gameState = value;

                GameStateChanged(_gameState, oldState);
            }
        }
    }

    private GameState _gameState = GameState.Prepare;

    [Header("Check to enable premium features (require EasyMobile plugin)")]
    public bool enablePremiumFeatures = true;

    [Header("Gameplay References")]
    public UIManager uIManager;
    public GameObject ballPrefab;
    public GameObject ballPoint;
    public GameObject obstacleManager;
    public GameObject targetPointManager;
    public GameObject leftFlipper;
    public GameObject rightFlipper;
    public GameObject targetPrefab;
    public GameObject ushape;
    public GameObject background;
    public GameObject fence;
    [HideInInspector]
    public GameObject currentTargetPoint;
    [HideInInspector]
    public GameObject currentTarget;
    public ParticleSystem die;
    public ParticleSystem hitGold;
    [HideInInspector]
    public bool gameOver;

    [Header("Gameplay Config")]
    public Color[] backgroundColor;
    public float torqueForce;
    public int scoreToIncreaseDifficulty = 10;
    public float targetAliveTime = 20;
    public float targetAliveTimeDecreaseValue = 2;
    public int minTargetAliveTime = 3;
    public int scoreToAddedBall = 15;
    public int numberOfBalls = 3;
    public GameObject[] GoalObjectsToComplete;

    private List<GameObject> listBall = new List<GameObject>();
    private Rigidbody2D leftFlipperRigid;
    private Rigidbody2D rightFlipperRigid;
    private int obstacleCounter = 0;
    private bool stopProcessing;
    GameObject[] backgroundObjects;
    GameObject[] lightGroups;
    private int numberOfBallsAtStart = 3;

    // Use this for initialization
    void Start()
    {
        GameState = GameState.Prepare;

        ScoreManager.Instance.Reset();
        currentTargetPoint = null;

        lightGroups = GameObject.FindGameObjectsWithTag("Light Group");

        //get the background objects
        backgroundObjects = GameObject.FindGameObjectsWithTag("Background");

        //Change color of backgorund, ushape, fence, flippers
        Color color = backgroundColor[Random.Range(0, backgroundColor.Length)];

        foreach(GameObject backgroundObj in backgroundObjects)
        {
            backgroundObj.GetComponent<SpriteRenderer>().color = color;
        }

        leftFlipperRigid = leftFlipper.GetComponent<Rigidbody2D>();
        rightFlipperRigid = rightFlipper.GetComponent<Rigidbody2D>();

        if (!UIManager.firstLoad)
        {
            numberOfBallsAtStart = numberOfBalls;

            StartGame();
            CreateBall();
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !UIManager.firstLoad)
        {
            //left flipper
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("LeftFlipper"))
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.flipping);

                foreach(GameObject lightGroup in lightGroups)
                {
                    LightGroupController controller = lightGroup.GetComponent<LightGroupController>();
                    controller.ShiftLightsLeft();
                }

                leftFlipperRigid.MoveRotation((leftFlipper.GetComponent<HingeJoint2D>().limits.max + 10000) * Time.deltaTime);
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("LeftFlipper"))
            {
                leftFlipperRigid.MoveRotation(leftFlipper.GetComponent<HingeJoint2D>().limits.min * 10);
            }
            else if (Input.GetMouseButton(0) || Input.GetButton("LeftFlipper"))
                leftFlipperRigid.MoveRotation(leftFlipper.GetComponent<HingeJoint2D>().limits.max * (Time.deltaTime + 50));
            
            //right flipper
            if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("RightFlipper"))
            {
                rightFlipperRigid.MoveRotation(rightFlipper.GetComponent<HingeJoint2D>().limits.max * 10);
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("RightFlipper"))
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.flipping);
                //Vector3 mouseHolding = Input.mousePosition;

                foreach (GameObject lightGroup in lightGroups)
                {
                    LightGroupController controller = lightGroup.GetComponent<LightGroupController>();
                    controller.ShiftLightsRight();
                }
                
                rightFlipperRigid.MoveRotation((rightFlipper.GetComponent<HingeJoint2D>().limits.min + 10000) * -Time.deltaTime);
            }
            else if (Input.GetMouseButton(1) || Input.GetButton("RightFlipper"))
                rightFlipperRigid.MoveRotation(rightFlipper.GetComponent<HingeJoint2D>().limits.min * (-Time.deltaTime + 50));
        }
    }

    /// <summary>
    /// Fire game event, create gold
    /// </summary>
    public void StartGame()
    {
        GameState = GameState.Playing;

        //Enable goldPoint, create gold at that position and start processing
        GameObject targetPoint = targetPointManager.transform.GetChild(Random.Range(0, targetPointManager.transform.childCount)).gameObject;
        targetPoint.SetActive(true);
        currentTargetPoint = targetPoint;
        Vector2 pos = Camera.main.ScreenToWorldPoint(currentTargetPoint.transform.position);
        currentTarget = Instantiate(targetPrefab, pos, Quaternion.identity) as GameObject;

        StartCoroutine(Processing());
    }

    void GameOver()
    {
        GameState = GameState.GameOver;

        numberOfBalls = numberOfBallsAtStart;
    }

    void AddTorque(Rigidbody2D rigid, float force)
    {
        rigid.AddTorque(force);
    }

    /// <summary>
    /// Create a ball
    /// </summary>
    public void CreateBall()
    {
        GameObject ball = Instantiate(ballPrefab, ballPoint.transform.position, Quaternion.identity) as GameObject;
        listBall.Add(ball);
    }

    /// <summary>
    /// Create gold 
    /// </summary>
    public void CreateTarget()
    {
        if (!gameOver)
        {
            //Stop all processing, disable current gold
            StopAllCoroutines();
            currentTargetPoint.SetActive(false);

            //Random new goldPoint and create new gold, then start processing
            GameObject goldPoint = targetPointManager.transform.GetChild(Random.Range(0, targetPointManager.transform.childCount)).gameObject;
            while (currentTargetPoint == goldPoint)
            {
                goldPoint = targetPointManager.transform.GetChild(Random.Range(0, targetPointManager.transform.childCount)).gameObject;
            }
            goldPoint.SetActive(true);
            currentTargetPoint = goldPoint;
            Vector2 goldPos = Camera.main.ScreenToWorldPoint(currentTargetPoint.transform.position);
            currentTarget = Instantiate(targetPrefab, goldPos, Quaternion.identity) as GameObject;
            StartCoroutine(Processing());
        }      
    }

    public void GoalComplete(GameObject ballLock)
    {
        for(int x = 0; x < GoalObjectsToComplete.Length; x++)
        {
            if (GoalObjectsToComplete[x] == ballLock)
            {
                ballLock.GetComponent<LockBallKickBack>().LockBall = false;

                if (GoalObjectsToComplete.Length > x + 1)
                    GoalObjectsToComplete[x + 1].GetComponent<LockBallKickBack>().LockBall = true;

                break;
            }
        }
    }

    /// <summary>
    /// Check game over
    /// </summary>
    /// <param name="the ball"></param>
    public void CheckGameOver(GameObject ball)
    {
        //remove the ball from the list
        listBall.Remove(ball);
        numberOfBalls--;

        if (numberOfBalls > 0)
            CreateBall();

        //No ball left -> game over
        if (listBall.Count == 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.gameOver);
            gameOver = true;

            currentTargetPoint.SetActive(false);

            ParticleSystem particle = Instantiate(hitGold, currentTarget.transform.position, Quaternion.identity) as ParticleSystem;
            particle.startColor = currentTarget.gameObject.GetComponent<SpriteRenderer>().color;
            particle.Play();
            Destroy(particle.gameObject, 1f);
            Destroy(currentTarget.gameObject);

            GameOver();           
        }
    }

    /// <summary>
    /// Change background element color, enable obstacles, update processing time
    /// </summary>
    public void CheckAndUpdateValue()
    {
        if (ScoreManager.Instance.Score % scoreToIncreaseDifficulty == 0)
        {
            //Change background element color
            Color color = backgroundColor[Random.Range(0, backgroundColor.Length)];

            foreach (GameObject backgroundObj in backgroundObjects)
            {
                backgroundObj.GetComponent<SpriteRenderer>().color = color;
            }

            //Enable obstacles
            if (obstacleCounter < obstacleManager.transform.childCount)
            {
                obstacleManager.transform.GetChild(obstacleCounter).gameObject.SetActive(true);
                obstacleCounter++;
            }

            //Update processing time
            if (targetAliveTime > minTargetAliveTime)
            {
                targetAliveTime -= targetAliveTimeDecreaseValue;
            }
            else
            {
                targetAliveTime = minTargetAliveTime;
            }
        }

        if (ScoreManager.Instance.Score % scoreToAddedBall == 0)
        {
            CreateBall();
        }
    }

    IEnumerator Processing()
    {
        Image img = currentTargetPoint.GetComponent<Image>();
        img.fillAmount = 0;
        float t = 0;
        while (t < targetAliveTime)
        {
            t += Time.deltaTime;
            float fraction = t / targetAliveTime;
            float newF = Mathf.Lerp(0, 1, fraction);
            img.fillAmount = newF;
            yield return null;
        }

        if (!gameOver)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.gameOver);
            //gameOver = true;
            for (int i = 0; i < listBall.Count; i++)
            {
                listBall[i].GetComponent<BallController>().Exploring();
            }

            currentTargetPoint.SetActive(false);

            ParticleSystem particle = Instantiate(hitGold, currentTarget.transform.position, Quaternion.identity) as ParticleSystem;
            particle.startColor = currentTarget.gameObject.GetComponent<SpriteRenderer>().color;
            particle.Play();
            Destroy(particle.gameObject, 1f);
            Destroy(currentTarget.gameObject);

            //GameOver();
        }      
    }
}
