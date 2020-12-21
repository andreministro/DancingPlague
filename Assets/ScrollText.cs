using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollText : MonoBehaviour
{
    private TextMeshProUGUI tmP;

    IEnumerator Start()
    {
        Debug.Log("ola");
        tmP = gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(tmP);
        int totalVisChars = tmP.textInfo.characterCount;
        int counter = 0;
        Debug.Log(totalVisChars);
        while (true)
        {
            int visibleCount = counter % (totalVisChars + 1);
            tmP.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisChars)
                yield return new WaitForSeconds(1.0f);

            counter += 1;

            yield return new WaitForSeconds(0.05f);

        }
    }
}
  
