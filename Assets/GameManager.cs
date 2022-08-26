using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("ScriptableObjects")]
    public ButtonNames[] guinessGeneralSystem, guinessSportSystems, guinessCultureSystems;
   
    [Header("Buttons")]
    public Button button1, button2, button3;

    [Header("text")]
    public TextMeshProUGUI text1,text2,text3, question,timeMode;
   
    [Header("Bools")]
    public bool buttonsport, buttonculture, general;
   
    [Header("timeline,values,vs")]
    public PlayableDirector[] playableDirector;
    public int RandomControl, timeModeControl = 0;
    private float timeModeCount = 60;

    [SerializeField] GameObject startbuttons;

    [SerializeField] Canvas startbutton1canvas, startbutton2canvas, startbutton3canvas;
    
    void Start()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);

        startbuttons = GameObject.Find("startButtons");
        timeMode.gameObject.SetActive(false);
        
    }
    private void Update()
    {
        if(timeModeControl == 1 && (general ==true || buttonsport == true || buttonculture == true))
        {
            timeCount();
        }
    }
    public void Control()
    {
        if (general == true)
        {
            
          controlsettings(guinessGeneralSystem,RandomControl);
          interactible(1);
          Debug.Log(RandomControl);
            
        }
        else if(buttonsport == true)
        {
          
          controlsettings(guinessSportSystems,RandomControl); 
          interactible(1);
          Debug.Log(RandomControl);
           
        }
        else if(buttonculture == true)
        {
            
          controlsettings(guinessCultureSystems,RandomControl);
          interactible(1);
          Debug.Log(RandomControl);
            
        }
    }
    void controlsettings(ButtonNames[] buttonNames, int randomcontrol)
    {
        if (randomcontrol <= buttonNames.Length)
        {
            randomcontrol = Random.Range(0, buttonNames.Length);
            question.text = buttonNames[randomcontrol].question;
            text1.color = Color.white;
            text2.color = Color.white;
            text3.color = Color.white;

            text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text1.text = buttonNames[randomcontrol].buttonName1.ToString();

            text2 = button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text2.text = buttonNames[randomcontrol].buttonName2.ToString();

            text3 = button3.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text3.text = buttonNames[randomcontrol].buttonName3.ToString();
        }

    } //control ayarlarý genel yönetim
    // buton  kontrolleri baþlangýç
    public void button1control()
    {
        categories(guinessGeneralSystem[RandomControl].buttonName1Control, guinessSportSystems[RandomControl].buttonName1Control, guinessCultureSystems[RandomControl].buttonName1Control,text1);
        interactible(0);
        StartCoroutine(TimeLineTimer());
        StartCoroutine(timer());
    }
    public void button2control()
    {
        categories(guinessGeneralSystem[RandomControl].buttonName2Control, guinessSportSystems[RandomControl].buttonName2Control, guinessCultureSystems[RandomControl].buttonName2Control,text2);
        interactible(0);
        StartCoroutine(TimeLineTimer());
        StartCoroutine(timer());
    }
    public void button3control()
    {
        categories(guinessGeneralSystem[RandomControl].buttonName3Control, guinessSportSystems[RandomControl].buttonName3Control, guinessCultureSystems[RandomControl].buttonName3Control,text3);
        interactible(0);
        StartCoroutine(TimeLineTimer());
        StartCoroutine(timer());
    }

    // buton  kontrolleri bitiþ
    public IEnumerator timer()
    {
        yield return new WaitForSeconds(2f);
        Control();
        
    }// yeni sorunun gelmesini saðlar
    public IEnumerator TimeLineTimer()
    {
        playableDirector[1].Play();
        yield return new WaitForSeconds(2f);
        playableDirector[0].Play();
    }// sorunun animasyonu buradan 
    
    public void timeCount()
    {
        timeModeCount -= Time.deltaTime;
        float seconds = Mathf.FloorToInt(timeModeCount % 60);
        timeMode.text = seconds.ToString();

        if(seconds<= 0)
        {
            SceneManager.LoadScene(0);  
        }
    }
    //buton etkileþimleri 
    void interactible(int value)
    {
        if (value == 0)
        {
            button1.interactable = false;
            button2.interactable = false;
            button3.interactable = false;
        }
        else if(value == 1)
        {
            button1.interactable = true;
            button2.interactable = true;
            button3.interactable = true;
        }
    }
    
    //buton etkileþimleri bitiþ

    public void startbutton1()
    {
        buttonControl(1);
    }
    public void startbutton2()
    {
        buttonControl(2);
    }
    public void startbutton3()
    {
        buttonControl(3);
    }

    public void TimeButton()
    {
        if(timeModeControl == 0)
        {
            timeModeControl = 1;
        }
        else if(timeModeControl == 1)
        {
            timeModeControl = 0;
        }
        Debug.Log(timeModeControl);
    }
    public void buttonControl (int boolsecim)
    {
        startbuttons.gameObject.SetActive(false);
        if(boolsecim == 1)
        {
            general = true;
        }
        else if(boolsecim == 2)
        {
            buttonsport = true;
        }
        else if(boolsecim == 3)
        {
            buttonculture = true;
        }
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        Control();
        playableDirector[0].Play();

        if(timeModeControl == 1)
        {
            timeMode.gameObject.SetActive(true);
        }
        else if(timeModeControl == 0)
        {
            timeMode.gameObject.SetActive(false);
        }

        
    }//butonlarýn nereye gideceðine dair yönetim kýsmý
    void categories(bool categorygeneral, bool categorysport, bool categoryculture, TextMeshProUGUI text)
    {
        if (general == true)
        {
            if (categorygeneral == true)
            {
                text.color = Color.blue;
            }
            else
            {
                text.color = Color.red;
                if (timeModeControl == 0)
                {
                    Debug.Log("gameOver");
                }
            }
        }
        else if (buttonsport == true)
        {
            if (categorysport == true)
            {
                text.color = Color.blue;
            }
            else
            {
                text.color = Color.red;
                if (timeModeControl == 0)
                {
                    Debug.Log("gameOver");
                }
            }
        }
        else if (buttonculture == true)
        {
            if (categoryculture == true)
            {
                text.color = Color.blue;
            }
            else
            {
                text.color = Color.red;
                if (timeModeControl == 0)
                {
                    Debug.Log("gameOver");
                }
            }
        }
    }

    

}
