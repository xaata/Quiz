using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class QuizManager : MonoBehaviour
{

    /// <summary>
    ///вместо public сменить на  privat  get set
    /// </summary>
    [SerializeField]
    private List<Sprite> image = new List<Sprite>();
    [SerializeField]
    private GameObject[] buttons;
    public string qusetion;
    private int randomImage;
    [SerializeField]
    public int difficultyCount;
    private int difficultyStep;
    private int currDifficulty;
    private int currQuestionAnswer;
    private int filledCell;
    public Text questionText;
    private bool questionsLeft = true;
   // private List<int> usedAnswers = new List<int>();
    private List<Sprite> answersLeft = new List<Sprite>();
    private GameObject[] buttonsStack;
    [SerializeField]
    GameObject board;
    private MainMenu mainMenu;
    // Start is called before the first frame update
    void Start()
    {

        DOTween.Init();
        buttonsStack = FindObjectsOfType<GameObject>();
        answersLeft.AddRange(image);
        difficultyStep = buttons.Length / difficultyCount;
        currDifficulty = difficultyStep;
        GenerateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
        //answers[0].gameObject;
    }

    void BounceEffect(GameObject bounce)
    {
        bounce.gameObject.transform.DOShakeScale(1, 0.5f, 5, 0, true);
        //bounce.gameObject.transform.DO;
    }
    void ShakeEffect(GameObject bounce)
    {
        bounce.gameObject.transform.DOShakePosition(2, 5f, 5, 0, true, true);
        //bounce.gameObject.transform.DO;
    }
    void CheckOriginal()
    {
        currQuestionAnswer = Random.Range(0, answersLeft.Count);
        for (int i = 0; i < answersLeft.Count; i++)
            if (answersLeft[currQuestionAnswer] == null)
                currQuestionAnswer = Random.Range(0, answersLeft.Count);
        if (answersLeft[currQuestionAnswer] == null)
        {
            //вызов победы
            mainMenu.GameOver();
            Time.timeScale = 0;
            for (int i = 0; i < buttonsStack.Length; i++)
            {
                buttonsStack[i].SetActive(false);
            }
        }
        
    }
    public void GenerateQuestion()
    {
        CheckOriginal();
        questionText.text = qusetion + " " + image[currQuestionAnswer].name;
        SetDifficulty();
        SetAnswers();    
    }
    public void SetDifficulty()
    {
        for (int i = currDifficulty; i < buttons.Length; i++)
        {
            buttons[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            buttons[i].gameObject.transform.gameObject.SetActive(false);
        }
    }
    public void NextDifficulty()
    {
        if (currDifficulty < buttons.Length)
        {
            for (int i = currDifficulty; i < currDifficulty + difficultyStep; i++)
            {
                buttons[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                buttons[i].gameObject.transform.gameObject.SetActive(true);
            }
            currDifficulty += difficultyStep;
        }
    }
    void SetAnswers()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                while (buttons[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite == null)
                {
                    randomImage = Random.Range(0, image.Count);
                    if (randomImage != currQuestionAnswer)//excludes setting several answers
                        buttons[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = image[randomImage];//image set
                }
                filledCell++;
            }
        }
        if(answersLeft[currQuestionAnswer] == null)
        {
            buttons[Random.Range(0, filledCell)].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = image[currQuestionAnswer];//SAVAGE COSTYL' for game ender
        }
        buttons[Random.Range(0, filledCell)].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = answersLeft[currQuestionAnswer];//answer image set "the answer should be only one" 
        answersLeft[currQuestionAnswer] = null;
    }
    void DeleteAnswers()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }
        filledCell = 0;
    }
    public void CheckAnswer(Image imageTarget)
    {
        if (imageTarget.sprite == image[currQuestionAnswer])
        {
            BounceEffect(imageTarget.gameObject);
            Invoke("Regenerate", 1f);
        }
        else
        {
            InCorrect(imageTarget.gameObject);
        }
    }
    public void InCorrect(GameObject bounce)
    {
        ShakeEffect(bounce);
    }

    void Regenerate()
    {
        DeleteAnswers();
        NextDifficulty();
        GenerateQuestion();
    }
}
