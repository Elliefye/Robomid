using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour
{
    [SerializeField]
    private Image blackScreen;
    public bool fadeFromBlack = true;
    public int fadeSpeed = 5;

    void Start()
    {
        if (fadeFromBlack)
        {
            //blackScreen.color = new Color(0, 0, 0, 255);
            StartCoroutine(FadeFromBlack(fadeSpeed));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FadeFromBlack(int speed)
    {
        while (blackScreen.color.a > 0)
        {
            float fadeAmount = blackScreen.color.a - (speed * Time.deltaTime);
            blackScreen.color = new Color(0, 0, 0, fadeAmount);
            yield return null;
        }
        blackScreen.gameObject.SetActive(false);
    }

    public void ExitPressed()
    {
        GlobalControl.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }

    public void ContinuePressed()
    {
        GlobalControl.Instance.ResetGame();
        SceneManager.LoadScene(1);
    }
}
