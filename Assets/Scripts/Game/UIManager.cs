using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameManager _gameManag;




    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score : " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManag = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(_gameManag == null)
        {
            Debug.LogError("GameManager is NULL in UIManager.cs");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScoreOnKill(int points)
    {
        _scoreText.text = "Score : " + points.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _livesSprite[currentLives];

        if(currentLives==0)
        {
            _gameManag.GameOver();
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
        }
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        
    }


}
