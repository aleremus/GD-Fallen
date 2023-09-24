using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image black;
    [SerializeField] List<Image> tutorials;
    [SerializeField] float blackOutTime;
    [SerializeField] float tutorialTime;
    [SerializeField] TMPro.TMP_Text click;
    GameManager gameManager;
    Animator animator;
    bool istutorial;


    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame

    IEnumerator LetsGO()
    {
        float time = 0;

        
        //yield return new WaitForSeconds(2);

        while (time < blackOutTime)
        {
            time += Time.deltaTime;
            black.color = new Color(time / blackOutTime, time / blackOutTime, time / blackOutTime, time / blackOutTime);
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        while (time < tutorialTime)
        {
            time += Time.deltaTime;
            foreach(var tutorial in tutorials)
                tutorial.color = new Color(1, 1, 1, time / tutorialTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);
        istutorial = true;
        click.gameObject.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    

    public void StartGame()
    {
        StartCoroutine(LetsGO());
    }

    private void Update()
    {
        if (!istutorial)
            return;
        if (Input.GetKeyDown(KeyCode.Tilde))
            return;
        if (Input.anyKeyDown)
        {
            istutorial = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main 2");
        }
    }

}
