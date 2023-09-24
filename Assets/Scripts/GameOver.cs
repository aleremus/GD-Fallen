using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] PMoveController pMoveController;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Image black; 
    [SerializeField] GameObject credits;
    [SerializeField] float blackOutTime;
    [SerializeField] float CreditsSpeed;
    [SerializeField] Transform creditsStop;
    [SerializeField] TMPro.TMP_Text money_field;
    [SerializeField] List<string> moneyStrings;
    GameManager gameManager;
    bool win;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        if (win)
            return;
        StartCoroutine(Credits());
    }

    IEnumerator Credits()
    {
        pMoveController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        win = true;
        float time = 0;

        while (time < blackOutTime)
        {
            time += Time.fixedDeltaTime;
            black.color = new Color(0, 0, 0, time / blackOutTime);
            yield return new WaitForEndOfFrame();
        }

        money_field.text = gameManager.CoinsCollected + "/"+ gameManager.moneyOnMap+ "МОНЕТ\n" + moneyStrings[gameManager.CoinsCollected];

        while(credits.transform.position.y < creditsStop.position.y)
        {
            credits.transform.Translate(Vector3.up * Time.fixedDeltaTime * CreditsSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Win();
        }
    }

    public void ExitToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
