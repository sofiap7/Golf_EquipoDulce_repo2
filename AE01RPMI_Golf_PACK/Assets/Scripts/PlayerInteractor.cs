using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Points System")]
    public int points; 
    public int winPoints = 1; 

    public GameObject HideObject;

    [Header("Scene Management")]
    public int sceneToLoad; 
    void Start()
    {
        points = 0; 
    }

    [Header("Sound References")]
    public PlayerController playerCont;

    void Update()
    {
        if (points >= winPoints) 
        {
            LoadScene();
        }

        if (points == winPoints - 1)
        {
            HideObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NextLevel"))
        {
            points++;
        }

        if (other.gameObject.CompareTag("PickUp"))
        {
            points++; 

            other.gameObject.SetActive(false); 

            playerCont.PlaySFX(2);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
