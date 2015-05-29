using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnnoyChris : MonoBehaviour {


    public float Sanity;
    public float currentSanity;
    int State;
    public Sprite happy;
    public Sprite normal;
    public Sprite angry;
    private AudioSource audioSource;
    public AudioClip[] sounds;
    private Image sanityImage;

	void Start () {
        Sanity = 30;
        currentSanity = Sanity;
        State = 0;
        audioSource = GetComponent<AudioSource>();
        sanityImage = GameObject.Find("Sanity").GetComponent<Image>();
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
        if (percentSanity <= 0.66f && percentSanity > 0.33f && State != 1)
        {
            State = 1;
            GetComponent<Image>().sprite = normal;
            sanityImage.color = Color.yellow;
        }
        if (percentSanity <= 0.33f && percentSanity > 0.0f && State != 2)
        {
            State = 2;
            GetComponent<Image>().sprite = angry;
            sanityImage.color = Color.red;
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

    public void ClickEvent(){
        currentSanity--;
        if (Random.Range(0, 5) == 0)
        {
            audioSource.clip = sounds[Random.Range(0, 3)];
            audioSource.Play();
        }
    }
}
