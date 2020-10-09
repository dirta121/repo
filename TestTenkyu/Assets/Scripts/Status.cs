using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Status : MonoBehaviour
{
    private TextMeshProUGUI status;
    public void ShowWin() 
    {
        status=GetComponent<TextMeshProUGUI>();
        status.gameObject.SetActive(true);
        status.color = Color.green;
        status.text = "WIN!!! Let's do this again..";
        StartCoroutine(WaitCoroutine());
    }
    public void ShowLose() 
    {
        status = GetComponent<TextMeshProUGUI>();
        status.gameObject.SetActive(true);
        status.color = Color.red;
        status.text = "Lose =( Try again now..";
        StartCoroutine(WaitCoroutine());
    }
    private void Hide() 
    {
        status.gameObject.SetActive(false);
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        Hide();
    }
}
