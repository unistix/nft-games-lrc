using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float timeUntilEnemy = 1.0f;
    public float minTimeUntilEnemy = 0.25f;
    public float maxTimeUntilEnemy = 2.0f;

    public GameObject SparkBlasterGraphic;
    public GameObject CrossBlasterGraphic;

    public Text highScoreText;
    public Text scoreText;

    private PlayerProfile _playerProfile;

    void OnEnable()
    {
        _playerProfile = RealmController.Instance.GetPlayerProfile();
        highScoreText.text = "HIGH SCORE: " + _playerProfile.HighScore.ToString();
        scoreText.text = "SCORE: " + _playerProfile.Score.ToString();
    }
    

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = "HIGH SCORE: " + _playerProfile.HighScore.ToString();
        scoreText.text = "SCORE: " + _playerProfile.Score.ToString();
        timeUntilEnemy -= Time.deltaTime; //reduce the time between enemy spawns 

        if (timeUntilEnemy <= 0) //if an an enemy hasnt just appeared get one and then set a random time interval between
        {
            GameObject enemy = ObjectPool.SharedInstance.GetPooledEnemy();
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
            timeUntilEnemy = Random.Range(minTimeUntilEnemy, maxTimeUntilEnemy);
        }

        if (_playerProfile != null)
        {
            SparkBlasterGraphic.SetActive(_playerProfile.SparkBlasterEnabled); //sparkblaster results are bool so pass in based on this
            CrossBlasterGraphic.SetActive(_playerProfile.CrossBlasterEnabled);
        }
        if (Input.GetKey("escape"))
        {
            RealmController.Instance.ResetScore();
            Application.Quit();

        }
    }
}
