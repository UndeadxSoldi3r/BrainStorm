using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnnoyChris : MonoBehaviour {


    public float Sanity;
    public float currentSanity;
    int State;
    int clicks;
    public Sprite happy;
    public Sprite normal;
    public Sprite angry;
    public Sprite fartface;
    private AudioSource audioSource;
    public AudioClip[] sounds;
    private Image sanityImage;
    private Text Clicks;
    private Animator animatorController;

	void Start () {
        animatorController = GetComponent<Animator>();
        Sanity = 10;
        currentSanity = Sanity;
        State = 0;
        audioSource = GetComponent<AudioSource>();
        sanityImage = GameObject.Find("Sanity").GetComponent<Image>();
        Clicks = GameObject.Find("Clicks").GetComponent<Text>();
        sanityImage.color = Color.green;

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
            sanityImage.color = Color.gray;
            audioSource.clip = sounds[3];
            audioSource.Play();
            animatorController.SetBool("Spinning", true);
            
        }

        if (State != 3)
        {
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
            clicks++;
            Clicks.text = clicks.ToString();
            currentSanity--;
            if (Random.Range(0, 5) == 0)
            {
                audioSource.clip = sounds[Random.Range(0, 3)];
                audioSource.Play();
            }
        }
    }
}
