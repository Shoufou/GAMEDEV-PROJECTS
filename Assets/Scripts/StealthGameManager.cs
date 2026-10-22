using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class StealthGameManagerBehavior : MonoBehaviour
{
    private const int TIME_TO_COMPLETE_GAME = 61;
    private const int TIME_TO_ESCAPE_DETECTION = 5;

    private const int initial_value = 1;

    public UnityEvent<int> timerChanged;
    public UnityEvent<int> stealthStatusChanged;
    public UnityEvent stealthStatusReset;
    public UnityEvent playerHit;
    public UnityEvent hpReset;
    public UnityEvent timerUIReset;
    public UnityEvent resetBuffs;
    public int time;

    [SerializeField] private Transform PlayerSpawnPoint;
    [SerializeField] private GameObject Player;

    private Coroutine timer;

    //stealth game mechanics
    public static StealthGameManagerBehavior instance;
    public int detectionTimer;
    public int guardsWhoSeePlayer;
    public Coroutine detectionCoroutine;


    private void Start()
    {
        this.guardsWhoSeePlayer = 0;
        this.time = initial_value;
        this.detectionTimer = initial_value;
    }

    void Awake()
    {
        instance = this;
    }

    //listener to playerHp to know when to trigger gameover event
    private void OnEnable()
    {
        MovementController.GameOver.AddListener(GameOver);
    }

    private void Update()
    {
        if (this.time <= 0)
        {
            ResetGame();
            this.time = TIME_TO_COMPLETE_GAME;
        }

        if(guardsWhoSeePlayer > 0 && detectionCoroutine == null)
        {
            detectionCoroutine = StartCoroutine(detectionTime());
        }



        if(guardsWhoSeePlayer == 0 && detectionCoroutine != null)
        {
            detectionTimer = TIME_TO_ESCAPE_DETECTION;
            StopCoroutine(detectionCoroutine);
            detectionCoroutine = null;
            stealthStatusReset?.Invoke();
        }

        if(detectionTimer <= 0)
        {
            ResetGame();
        }
    }

    private void GameOver(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            ResetGame();
            print("Game over! Resetting Game.");
        }
    }

    public void Win()
    {
        ResetGame();
        print("You won! Resetting Game.");
    }


    public void ResetGame()
    {
        //teleports the player back to the spawnpoint
        CharacterController controller = Player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;
        Player.transform.position = PlayerSpawnPoint.position;
        if (controller != null) controller.enabled = true;

        //stops relevant coroutines and sets them to null
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }


        timer = null;
        //calls relevant functions to reset player stats
        hpReset?.Invoke();
        resetBuffs?.Invoke();

        //resets timer
        timerUIReset?.Invoke();

        //resets stealth
        stealthStatusReset?.Invoke();

        //brings back buff objects
        GameObject[] buffs = GameObject.FindGameObjectsWithTag("Buffs");
        foreach (GameObject buff in buffs)
        {
            buff.GetComponent<Renderer>().enabled = false;
            buff.GetComponent<Collider>().enabled = true;
        }


        print("Game Over! Game Restarting!");
    }

    public void testStartEvent()
    {
        if (timer == null)
        {
            timer = StartCoroutine(StartTimer());
        }
    }


    IEnumerator StartTimer()
    {
        time = TIME_TO_COMPLETE_GAME; 

        while (true)
        {
            time--;
            timerChanged?.Invoke(time);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator detectionTime()
    {
        this.detectionTimer = TIME_TO_ESCAPE_DETECTION; 
        while (true)
        {
            this.detectionTimer--;
            stealthStatusChanged?.Invoke(detectionTimer);
            yield return new WaitForSeconds(1f);
        }
    }
}

   
