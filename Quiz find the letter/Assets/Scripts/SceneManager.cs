using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    GameObject blackout;
    public void ChangeScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void ChangeSceneWithBlackout(int sceneBuildIndex)
    {
        blackout.GetComponent<Image>().enabled = true;
        StartCoroutine(loadSceneWithBlackout(blackout.GetComponent<Image>(), sceneBuildIndex));
    }

    IEnumerator loadSceneWithBlackout(Image blackout, int sceneBuildIndex)
    {
        Color target = new Color(blackout.color.r, blackout.color.g, blackout.color.b, 1f);
        while (blackout.color != target)
        {
            blackout.color = Vector4.MoveTowards(blackout.color, target, 5 * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
