using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class FunWithLaserGameManager : MonoBehaviour
{

    public UnityEvent<int> timerChanged;
    public UnityEvent playerHit;
    public UnityEvent hpReset;
    public UnityEvent timerUIReset;
    public UnityEvent resetBuffs;
    public int time;
    
    [SerializeField] private GameObject VerticalLaser;
    [SerializeField] private GameObject HorizontalLaser;
    [SerializeField] private GameObject DiagonalLaser;
    [SerializeField] private Transform  LaserSpawnPoint;
    [SerializeField] private Transform PlayerSpawnPoint;
    [SerializeField] private GameObject Player;

    private Coroutine timer;
    private Coroutine laserSpawner;
    private GameObject[] lasers;

    private void Start()
    {
        lasers = new GameObject[] { VerticalLaser, HorizontalLaser, DiagonalLaser };
        this.time = 1;
    }

    //listener to playerHp to know when to trigger gameover event
    private void OnEnable()
    {
        MovementController.GameOver.AddListener(GameOver);
    }

    private void Update()
    {
        if(this.time <= 0)
        {
            ResetGame();
            this.time = 1;
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


    private void ResetGame()
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
        if (laserSpawner != null)
        {
            StopCoroutine(laserSpawner);
            laserSpawner = null;
        }


        timer = null;
        laserSpawner = null;

        //calls relevant functions to reset player stats
        hpReset?.Invoke();
        resetBuffs?.Invoke();

        //destroys all existing lasers
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject laser in lasers)
        {
            Destroy(laser);
        }

        //resets timer
        timerUIReset?.Invoke();

        //brings back buff objects
        GameObject[] buffs = GameObject.FindGameObjectsWithTag("Buffs");
        foreach (GameObject buff in buffs)
        {
            buff.GetComponent<Renderer>().enabled = true;
            buff.GetComponent<Collider>().enabled = true;
        }


        print("Game Over! Game Restarting!");
    }

    public void testStartEvent()
    {
        if (timer == null && laserSpawner == null)
        {
            timer = StartCoroutine(StartTimer());
            laserSpawner = StartCoroutine(SpawnLasers());
        }
    }

    //communicates with the player object to take damage, and with the UI manager to actually update the GUI
    public void playerHitEvent()
    {
        playerHit?.Invoke();
    }

     IEnumerator StartTimer()
    {
        time = 61;

        while(true)
        {
            time--;
            timerChanged?.Invoke(time);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpawnLasers()
    {
        int index;

        while(true)
        {
            index = UnityEngine.Random.Range(0, lasers.Length);
            GameObject laser = Instantiate(lasers[index], LaserSpawnPoint.position, lasers[index].transform.rotation);
            LaserScript[] laserComponents = laser.GetComponentsInChildren<LaserScript>();

            foreach (LaserScript laserComponent in laserComponents)
            {
                if (laserComponent != null)
                {
                    laserComponent.playerHit.AddListener(playerHitEvent);
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
   
}
