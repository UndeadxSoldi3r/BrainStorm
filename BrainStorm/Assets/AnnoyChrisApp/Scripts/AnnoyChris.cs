using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnnoyChris : MonoBehaviour {


    public float Sanity;
    public float currentSanity;
    float Score;
    int State;
    float Level;
    float clicks;
    float timer;
    float levelTimer;
    public Sprite happy;
    public Sprite normal;
    public Sprite angry;
    public Sprite fartface;
    private AudioSource audioSource;
    public AudioClip[] sounds;
    private Image sanityImage;
    private Text Clicks;
    private Text Levels;
    private Animator animatorController;
    private GameObject NextButton;
    private GameObject TimerObject;
    private string lvlText;
    bool timing;


	void Start () {
        TimerObject = GameObject.Find("Timer");
        timing = false;
        timer = 6;
        levelTimer = timer;
        Level = 1;
        animatorController = GetComponent<Animator>();
        Sanity = 5;
        currentSanity = Sanity;
        State = 0;
        audioSource = GetComponent<AudioSource>();
        sanityImage = GameObject.Find("Sanity").GetComponent<Image>();
        Clicks = GameObject.Find("Clicks").GetComponent<Text>();
        sanityImage.color = Color.green;
        NextButton = GameObject.Find("Next");
        Levels = GameObject.Find("Level").GetComponent<Text>();
        lvlText = "Level: ";
        string nt = lvlText + Level.ToString();
        Levels.text = nt;
        NextButton.SetActive(false);
	}

    void Update()
    {
        float percentSanity = currentSanity / Sanity;
        sanityImage.fillAmount = percentSanity;
        if (percentSanity > 0.66f && State != 0)
        {
            State = 0;
            GetComponent<Image>().sprite = happy;
            sanityImage.color = Color.green;
        }
        else if (percentSanity <= 0.66f && percentSanity > 0.33f && State != 1)
        {
            State = 1;
            GetComponent<Image>().sprite = normal;
            sanityImage.color = Color.yellow;
        }
        else if (percentSanity <= 0.33f && percentSanity > 0.0f && State != 2)
        {
            State = 2;
            GetComponent<Image>().sprite = angry;
            sanityImage.color = Color.red;
        }
        else if( percentSanity <= 0.0f && State != 3)
        {
            State = 3;
            GetComponent<Image>().sprite = fartface;
            sanityImage.color = Color.white;
            audioSource.clip = sounds[3];
            audioSource.Play();
            animatorController.SetBool("Spinning", true);
            NextButton.SetActive(true);
            Score += ((levelTimer / timer)) * (Level) * 100;
            Score = Mathf.CeilToInt(Score);
            Clicks.text = Score.ToString();
        }

        if (State != 3)
        {
            TimerObject.GetComponent<Image>().fillAmount = levelTimer / timer;
            if (timing)
            {
                levelTimer -= Time.deltaTime;
            }
            if (levelTimer <= 0)
            {
                currentSanity = Sanity;
                levelTimer = timer;
                timing = false;
            }            
            if (percentSanity < 1.0f)
            {
                currentSanity += 2f * Time.deltaTime;
            }
            if (percentSanity <= 0.66f)
            {
                currentSanity += 2f * Time.deltaTime;
            }
            if (percentSanity <= 0.33f)
            {
                currentSanity += 2f * Time.deltaTime;
            }
        }
    }

    public void ClickEvent(){
        if (State != 3)
        {
            if (timing == false)
            {
                timing = true;
            }
            clicks++;
            currentSanity--;
            if (Random.Range(0, 5) == 0)
            {
                audioSource.clip = sounds[Random.Range(0, 3)];
                audioSource.Play();
            }
        }
    }

    public void NextLevel()
    {
        Level++;
        timer += 1;
        clicks = 0;
        levelTimer = timer;
        timing = false;
        string nt = lvlText + Level.ToString();
        animatorController.SetBool("Spinning", false);
        Levels.text = nt;
        Sanity += 5;
        currentSanity = Sanity;
        NextButton.SetActive(false);
    }

}
