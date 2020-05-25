using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public SInt heartsAmount;
    public float spacing;

    private List<Heart> hearts = new List<Heart>();

    void OnEnable()
    {
        heartsAmount.variableChanged += UpdateHearts;
    }

    void OnDisable()
    {
        heartsAmount.variableChanged -= UpdateHearts;
    }

    public void UpdateHearts()
    {
        if (heartsAmount.Value == 0)
        {
            for (int i = hearts.Count - 1; i > -1; i--)
            {
                GameObject heartToRemove = hearts[i].gameObject;
                hearts.Remove(hearts[i]);
                Destroy(heartToRemove);
            }
            return;
        }

        int amountOfHeartsNeeded = Mathf.CeilToInt(heartsAmount.Value / 2f);
        if (transform.childCount == 0) // if the hearts haven't been made yet
        {
            for (int i = 0; i < amountOfHeartsNeeded; i++)
            {
                GameObject heartObject = Instantiate(heartPrefab);
                Heart newHeart = heartObject.GetComponent<Heart>();

                if (i + 1 == amountOfHeartsNeeded) // if it's the last heart to be made
                {
                    newHeart.ChangeValue(2 - (heartsAmount.Value % 2)); // if there's just a half heart at the end, change the value to one half
                    // 11 % 2 = 1 OR 10 % 2 = 0
                    // 2 - 1 = 1   |   2 - 0 = 2
                }
                else // if it's not the last heart needed, then it should be full
                    newHeart.ChangeValue(2);

                hearts.Add(newHeart);
                heartObject.transform.position = new Vector3
                    (
                        hearts.Count > 1 ? // if there's more than 1 heart already, position it relative to the last heart
                        hearts[hearts.Count - 2].gameObject.transform.position.x + spacing :
                        transform.position.x, // else set it to the heart manager's position
                        transform.position.y,
                        0
                    );

                heartObject.transform.SetParent(transform);
            }
        }
        else if (transform.childCount < amountOfHeartsNeeded) // if more hearts are needed
        {
            // update current hearts
            for(int i = 0; i < transform.childCount; i++)
            {
                hearts[i].ChangeValue(2);
            }

            // make new hearts
            amountOfHeartsNeeded = amountOfHeartsNeeded - transform.childCount;
            for (int i = 0; i < amountOfHeartsNeeded; i++)
            {
                GameObject heartObject = Instantiate(heartPrefab);
                Heart newHeart = heartObject.GetComponent<Heart>();

                if (i + 1 == amountOfHeartsNeeded) // if it's the last heart to be made
                {
                    newHeart.ChangeValue(2 - (heartsAmount.Value % 2)); // if there's just a half heart at the end, change the value to one half
                    // 11 % 2 = 1 OR 10 % 2 = 0
                    // 2 - 1 = 1   |   2 - 0 = 2
                }
                else // if it's not the last heart needed, then it should be full
                    newHeart.ChangeValue(2);

                hearts.Add(newHeart);
                // there should already be some hearts in the list
                heartObject.transform.position = new Vector3
                    (
                        hearts[hearts.Count - 2].gameObject.transform.position.x + spacing,
                        transform.position.y,
                        0
                    );

                heartObject.transform.SetParent(transform);
            }
        }
        else // if there are the right amount of hearts
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i + 1 > amountOfHeartsNeeded)
                {
                    GameObject heartToRemove = hearts[i].gameObject;
                    hearts.RemoveAt(i);
                    Destroy(heartToRemove);
                }
                else if (i + 1 == amountOfHeartsNeeded) // if it's the last heart to be made
                {
                    hearts[i].ChangeValue(2 - (heartsAmount.Value % 2)); // if there's just a half heart at the end, change the value to one half
                    // 11 % 2 = 1 OR 10 % 2 = 0
                    // 2 - 1 = 1   |   2 - 0 = 2
                }
                else // if it's not the last heart needed, then it should be full
                {
                    hearts[i].ChangeValue(2);
                }
            }
        }
    }
}
