using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCollectible : MonoBehaviour
{
    private int scoreToAdd = 1;
    public GameObject particleToSpawn;
    public GameObject particleToDestroy;
    private GameObject currentParticle;

    private void OnTriggerEnter(Collider other)
    {
        NoiseController noise = other.GetComponent<NoiseController>();  

        if (noise)
        {
            //playerInventory.collectedFloppy++;
            ScoreManager.instance.IncreaseScore(scoreToAdd);
            Debug.Log("SCORE!");
            //ScoreManager.instance.IncreaseScore(scoreToAdd);
            // UIManager.instance.gameScreen.SpawnScoreUI(scoreToAdd);
            // UIManager.instance.gameScreen.SpawnFloppyUI();
            // currentParticle = Instantiate(particleToSpawn, transform.position, particleToSpawn.transform.rotation);
            // Destroy(currentParticle, 2.0f);

            // AkSoundEngine.PostEvent("UI_Disk_Get", gameObject);
            // Destroy(particleToDestroy);
            Destroy(gameObject);
        }
    }
}
