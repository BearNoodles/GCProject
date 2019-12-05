using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float xDist, yDist, zDist;
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;
    public bool xPause;
    public float xPauseInterval;
    public bool yPause;
    public float yPauseInterval;
    public bool zPause;
    public float zPauseInterval;
    public float xOffset, yOffset, zOffset;

    private AudioSource sound;


    private Vector3 startPos;
    private float xDirection, yDirection, zDirection, xStart, yStart, zStart, xTimer, yTimer, zTimer;
    private bool isXPaused, isYPaused, isZPaused, xLeft, yLeft, zLeft;
    bool soundStopped = false;
    float maxVolume;

    // Use this for initialization
    void Start()
    {
        startPos = gameObject.transform.parent.position;
        xDirection = 1;
        yDirection = 1;
        zDirection = 1;
        xDist = Mathf.Abs(xDist);
        yDist = Mathf.Abs(yDist);
        zDist = Mathf.Abs(zDist);
        
        StartSpeed();

        sound = GetComponent<AudioSource>();

        maxVolume = sound.volume;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    Vector3 Move(Vector3 pos)
    {
        pos += new Vector3(xSpeed * xDirection * Time.deltaTime, ySpeed * yDirection * Time.deltaTime, zSpeed * zDirection * Time.deltaTime);
        return pos;
    }

    float SetDirection(float speed, bool left)
    {
        if (speed >= 0)
            return left ? 1 : -1;
        else
            return left ? -1 : 1;
    }

    void MovePlatform()
    {
        if (xSpeed == 0 && ySpeed == 0 && zSpeed == 0)
        {
            if (!soundStopped)
            {
                FadeOutSound();
            }
        }
        else if (!sound.isPlaying)
        {
            sound.Play();
            soundStopped = false;
        }

        Vector3 pos = gameObject.transform.parent.position;
        pos = Move(pos);
        gameObject.transform.parent.position = pos;

        if ((xDist != 0) && (pos.x < startPos.x - xDist + xOffset || pos.x > startPos.x + xDist + xOffset))
        {
            if (pos.x < startPos.x - xDist + xOffset)
                xLeft = true;
            else
                xLeft = false;

            if (isXPaused)
                PauseX();
            else if (xPause)
            {
                isXPaused = true;
                PauseX();
            }

            else
                xDirection = SetDirection(xSpeed, xLeft);
        }

        if ((yDist != 0) && (pos.y < startPos.y - yDist + yOffset || pos.y > startPos.y + yDist + yOffset))
        {
            if (pos.y < startPos.y - yDist + yOffset)
                yLeft = true;
            else
                yLeft = false;

            if (isYPaused)
                PauseY();
            else if (yPause)
            {
                isYPaused = true;
                PauseY();
            }

            else
                yDirection = SetDirection(ySpeed, yLeft);
        }

        if ((zDist != 0) && (pos.z < startPos.z - zDist + zOffset || pos.z > startPos.z + zDist + zOffset))
        {
            if (pos.z < startPos.z - zDist + zOffset)
                zLeft = true;
            else
                zLeft = false;

            if (isZPaused)
                PauseZ();
            else if (zPause)
            {
                isZPaused = true;
                PauseZ();
            }
            else
                zDirection = SetDirection(zSpeed, zLeft);
        }

        
    }

    void StartSpeed()
    {
        
        if (xDist == 0)
            xSpeed = 0;
        if (yDist == 0)
            ySpeed = 0;
        if (zDist == 0)
            zSpeed = 0;

        xStart = xSpeed;
        yStart = ySpeed;
        zStart = zSpeed;
    }

    void PauseX()
    {
        if (xTimer < xPauseInterval)
        {
            xTimer += Time.deltaTime;
            xSpeed = 0;
        }

        else
        {
            xTimer = 0;
            isXPaused = false;
            xSpeed = xStart;
            xDirection = SetDirection(xSpeed, xLeft);
        }
    }

    void PauseY()
    {
        if (yTimer < yPauseInterval)
        {
            yTimer += Time.deltaTime;
            ySpeed = 0;
        }

        else
        {
            yTimer = 0;
            isYPaused = false;
            ySpeed = yStart;
            yDirection = SetDirection(ySpeed, yLeft);
        }
    }

    void PauseZ()
    {
        if (zTimer < zPauseInterval)
        {
            zTimer += Time.deltaTime;
            zSpeed = 0;
        }
        
        else
        {
            zTimer = 0;
            isZPaused = false;
            zSpeed = zStart;
            zDirection = SetDirection(zSpeed, zLeft);
        }
    }

    private void FadeOutSound()
    {
        sound.volume -= 0.01f;
        if(sound.volume <= 0)
        {
            sound.Stop();
            soundStopped = true;
            sound.volume = maxVolume;
        }
    }
}


