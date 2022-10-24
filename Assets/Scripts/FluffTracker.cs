using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FluffTracker : MonoBehaviour
{
    public int fluffLossSpeed;
    WaitForSeconds delay;
    public GameObject fluff;
    Transform player;

    public int playerHealth;
    public Text fluffText;

    bool hasThread;
    bool hasNeedle;
    bool hasHeart;

    void Start()
    {
        delay = new WaitForSeconds(fluffLossSpeed);
        StartCoroutine(Waiter());
        player = GetComponent<Transform>();
    }

    void Update()
    {
        if(playerHealth<=0)
        {
            //death sound?
            //Restart
            SceneManager.LoadScene("Game");
        }
    }
    
    IEnumerator Waiter()
    {
        while(true)
        {
            yield return delay;
            //Debug.Log("dropping one fluff");

            //spawn fluff behind you
            Instantiate(fluff, new Vector3(player.position.x, player.position.y+2f, player.position.z-1f), Quaternion.identity);

            playerHealth--; //health change
            //text update
            fluffText.text = "Fluff: " + playerHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fluff")
        {
            //health goes up
            playerHealth++;
            //Debug.Log("destroy piece");
            Destroy(other.gameObject); //fluff gets destroyed
            

            //text update
            fluffText.text = "Fluff: " + playerHealth;
        }

        if (other.tag == "Needle")
        {
            hasNeedle = true;
            //crunch.Play();
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Thread")
        {
            hasThread = true;
            //crunch.Play();
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Heart")
        {
            hasHeart = true;
            //crunch.Play();
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Bear")
        {
            if (hasNeedle & hasThread & hasHeart)
            {
                SceneManager.LoadScene("GoodEnding");
            }
            else if (hasNeedle & hasThread)
            {
                SceneManager.LoadScene("BadEnding");
            }
        }

    }
}
