using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputPrompt : MonoBehaviour
{
    private List<Image> sprites;



    private void Awake()
    {
        sprites = new List<Image>(GetComponentsInChildren<Image>());

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for (int i = 0; i < 3; i++)
        {
            float time = 0f;
            while (time < 1f)
            {
                foreach (Image sprite in sprites) sprite.color = new Color(1, 1, 1, time);

                time += Time.deltaTime;
                yield return null;
            }
            while (time > 0f)
            {
                foreach (Image sprite in sprites) sprite.color = new Color(1, 1, 1, time);

                time -= Time.deltaTime;
                yield return null;
            }
        }

        gameObject.SetActive(false);
    }
}