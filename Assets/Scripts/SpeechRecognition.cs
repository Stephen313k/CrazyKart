#if UNITY_ANDROID

#else
using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.Windows.Speech;
    using System.Linq;
    using UnityEngine.UI;
#endif

using UnityEngine;

public class SpeechRecognition : MonoBehaviour
{

    #if UNITY_ANDROID
    #else    
        KeywordRecognizer keywordRecognizer;

        Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    #endif

    public GameObject gameMenu;
    public GameObject player;

    public GameObject pauseMenuUI;


    void Start()
    {

#if UNITY_ANDROID
#else
        keywords.Add("restart", () =>
        {
            /* Fire off the method */
            Play();
        });
        keywords.Add("play", () =>
        {
            /* Fire off the method */
            Play();
        });

        keywords.Add("quit", () =>
        {
            /* Fire off the method */
            Exit();
        });

        keywords.Add("exit", () =>
        {
            /* Fire off the method */
            Exit();
        });

        keywords.Add("pause", () =>
        {
            /* Fire off the method */
            Pause();
        });

        keywords.Add("resume", () =>
        {
            /* Fire off the method */
            Resume();
        });

        /* New KeywordRecognizer object and passing all the keys (words) */
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        /* When a phrase was recognized, pop off a new keywordRecognizer */
        keywordRecognizer.OnPhraseRecognized += KeyWordRecognizerOnPhraseRecognized;
        /* Start figuring out what to do once it was recognized */
        keywordRecognizer.Start();

#endif

    }

#if UNITY_ANDROID
#else
    /* If keyword is in the list of recognized words */
    void KeyWordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }



    /* When 'One' was picked up */
    void Play()
    {
        /* Terminate the Application */
        Application.LoadLevel("Level1");
    }

    void Exit()
    {
        Application.Quit();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
#endif
}