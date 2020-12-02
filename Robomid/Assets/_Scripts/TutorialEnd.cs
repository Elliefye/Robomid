using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("DoorClosed");
            foreach(var door in doors)
            {
                Destroy(door);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");
                foreach(var controller in controllers)
                {
                    Destroy(controller);
                }
                SceneManager.LoadScene(1);
            }
        }
    }
}
