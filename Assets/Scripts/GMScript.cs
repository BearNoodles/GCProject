using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GMScript : MonoBehaviour
{
    public RigidbodyFirstPersonController player;
    private AudioSource playerDeathSound;
    int died;
    public Canvas canvas;
    public static int retries;
    private Text retryText;

    private Vector3 playerPos;
    private static Vector3 resetPos;

    private float pTimer;
    private static float gameTimer;
    private int timeInt;
    private Text gameTimeText;

    public GameObject bonusLift;

    DynamicMusic music;
    

    // Use this for initialization
    void Awake()
    {
        Physics.gravity = new Vector3(0, -30f, 0);
        playerPos.x = PlayerPrefs.GetFloat("resetPosx");
        playerPos.y = PlayerPrefs.GetFloat("resetPosy");
        playerPos.z = PlayerPrefs.GetFloat("resetPosz");
        if (playerPos == Vector3.zero)
        {
            playerPos = Checkpoint.SetResetPosition();
        }

        died = PlayerPrefs.GetInt("died");

        player.transform.position = playerPos;
        retryText = GameObject.Find("/Canvas/RetryText").GetComponent<Text>();
        retries = PlayerPrefs.GetInt("retries");

        gameTimeText = GameObject.Find("/Canvas/TimeText").GetComponent<Text>();
        gameTimer = PlayerPrefs.GetFloat("time");

        foreach(AudioSource a in player.GetComponents<AudioSource>())
        {
            if(a.clip.name == "aah")
            {
                playerDeathSound = a;
                break;
            }
        }

        if(died == 1)
        {
            playerDeathSound.Play();
        }

        PlayerPrefs.SetInt("died", 0);
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        timeInt = (int)gameTimer;

        retryText.text = "Retries : " + retries.ToString();
        gameTimeText.text = "Time : " + timeInt.ToString();

        if (retries > 5 && bonusLift != null)
            Destroy(bonusLift);

        if (player.transform.position.y < -5)
        {
            resetPos = Checkpoint.SetResetPosition();
            Reset(resetPos.x, resetPos.y, resetPos.z, retries + 1, gameTimer, 1);
        }

        pTimer = player.GetComponent<RigidbodyFirstPersonController>().Timer;
        if (pTimer <= 0)
        {
            canvas.GetComponentInChildren<Text>().text = "";
        }

        else if (pTimer < 10)
        {
            canvas.GetComponentInChildren<Text>().text = "PowerUp: 0" + (int)pTimer;
        }

        else
        {
            canvas.GetComponentInChildren<Text>().text = "PowerUp: " + (int)pTimer;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetPos = Checkpoint.SetResetPosition();
            Reset(resetPos.x, resetPos.y, resetPos.z, retries + 1, gameTimer, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            resetPos = Checkpoint.NextCheckpoint();
            Reset(resetPos.x, resetPos.y, resetPos.z, retries, gameTimer, 0);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Reset(-35, 10, -152, 0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private static void Reset(float x, float y, float z, int retries, float time, int died)
    {
        PlayerPrefs.SetInt("retries", retries);
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.SetFloat("resetPosx", x - 5);
        PlayerPrefs.SetFloat("resetPosy", y);
        PlayerPrefs.SetFloat("resetPosz", z);
        PlayerPrefs.SetInt("died", died);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);

        DynamicMusic m = GameObject.FindGameObjectWithTag("Music").GetComponent<DynamicMusic>();
        m.ResetSpeakers();
    }

}
