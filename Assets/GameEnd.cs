using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public string NextLevel;
    public List<Image> UIImages = new List<Image>();
    public float Speed;
    public int TimeDelay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeToBlack());
        StartCoroutine(Delay());

    }

    IEnumerator FadeToBlack()
    {
        float alpha = UIImages[0].color.a;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * Speed;
            for (int i = 0; i < UIImages.Count; i++)
            {
                UIImages[i].color = new Color(UIImages[i].color.r, UIImages[i].color.g, UIImages[i].color.b, alpha);
            }
            yield return null;
        }
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(TimeDelay);
        SceneManager.LoadScene(NextLevel);
    }
}
