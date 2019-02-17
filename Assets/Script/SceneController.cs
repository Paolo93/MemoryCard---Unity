using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public const int gridRows = 4;
    public const int gridCols = 4;
    public const float offsetX = 2.8f;
    public const float offsetY = 2.5f;
    public string sceneName;


    [SerializeField] private MemoryCard originalCard = null; 
    [SerializeField] private Sprite[] images = null; 
    [SerializeField] private TextMeshPro scoreLabel;
    [SerializeField] private TextMeshPro chanceLabel;
    [SerializeField] private int chance;

    public string nextLevel = "Level2";
    public int levelToUnlock = 2;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    private int _score = 0;
    private int maxCards;
    
    public GameObject particle;

    public bool canReveal
    {
        get { return _secondRevealed == null; }//Getter which  return false, if it will cover second card
    }

   
    void Start()
    {
        if(!scoreLabel)
        {
            scoreLabel = null;
        } else
        {
            scoreLabel.text = "Score: " + _score + "/16";
        }
        if (!chanceLabel)
        {
            chanceLabel = null;
        }
        else
        {
            chanceLabel.text = "Chance: " + chance;
        }

        if (originalCard)
        {
           
            Vector3 startPos = originalCard.transform.position;
            maxCards = gridRows * gridCols;

            int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
            numbers = ShuffleArray(numbers);

            for (int i = 0; i < gridCols; i++)
            {
                for (int j = 0; j < gridRows; j++)
                {
                    MemoryCard card;
                    if (i == 0 && j == 0)
                    {
                        card = originalCard;
                    }
                    else
                    {
                        card = Instantiate(originalCard) as MemoryCard;
                    }

                    // next card in the list for each grid space
                    int index = j * gridCols + i;
                    int id = numbers[index];
                    card.SetCart(id, images[id]);

                    float posX = (offsetX * i) + startPos.x;
                    float posY = -(offsetY * j) + startPos.y;
                    card.transform.position = new Vector3(posX, posY, startPos.z);

                }
            }
        } 
     }

    public void Update()
    {
        if (chanceLabel)
        {
            if (chance <= 0)
            {
                GameOver();
            }
        }
    }



    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }



    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null) // if we havent the first card
        {
            _firstRevealed = card; // in this card, we assign first click
        } else //if we have the first click
        {
            _secondRevealed = card; //The card, which we assign second click
            StartCoroutine(CheckMatch());
            if (chanceLabel)
            {
                chance -= 1;
                chanceLabel.text = "Chance: " + chance;
            }
        }
    }

    private IEnumerator CheckMatch()
    {
        
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score += 2;
            scoreLabel.text = "Score: " + _score + "/16";
            PlayParticle();
            FindObjectOfType<AudioManager>().Play("Correct");
            if (_score >= maxCards)
            {
                Invoke("NextLevel", 1.5f);
            }
            
        } else
        {
            yield return new WaitForSeconds(.5f);
            FindObjectOfType<AudioManager>().Play("fail");
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        //usuniecie wartosci zmiennych niezaleznie od dopasowania kart It removes variable values independent from matching card
        _firstRevealed = null;
        _secondRevealed = null;
    
    }
    void PlayParticle()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Instantiate(particle, mousePosition, Quaternion.identity);
        Debug.Log(mousePosition);
    }
    /*
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    */
    public void NextLevel()
    {
        int CurrentLevel = PlayerPrefs.GetInt("ReachLevel", 1);
        PlayerPrefs.SetInt("ReachLevel", CurrentLevel + 1);
        SceneManager.LoadScene("SelectLevel");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("StartMenu");   
    }

}
