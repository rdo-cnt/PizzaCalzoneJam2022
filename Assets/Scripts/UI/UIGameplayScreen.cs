using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Needed to use text

public class UIGameplayScreen : MonoBehaviour
{

    // Noises UI
    //Candy counter on top left 
    //Health object on top right of the screen



    //Singleton (non persistent)
    public static UIGameplayScreen instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIGameplayScreen>();
            }

            return _instance;
        }
    }
    private static UIGameplayScreen _instance;

    //Public objects
    public Animator healthUI;
    public GameObject scoreUI;
    public GameObject spawnScorePrefab;
    public GameObject spawnScorePos;
    public CharacterHealthManager playerHealth;

    //Awake
    private void Awake()
    {
         if (GameManager.instance == null)
            return;

        if (playerHealth == null)
            playerHealth = GameManager.instance.playerReference.GetComponent<CharacterHealthManager>();

    }

    // Update is called once per frame
    void Update()
    {

        //Update score tally
        scoreUI.GetComponent<Text>().text = ScoreManager.instance.score.ToString();

        //Pause check



    }

    // public void SpawnScoreUI(int amount)
    // {
    //     GameObject m = (GameObject)Instantiate(spawnScorePrefab, spawnScorePos.transform);
    //     m.GetComponent<ScoreAdditionWait>().amount = amount;
    // }


}
