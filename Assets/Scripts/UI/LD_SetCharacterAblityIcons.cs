using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LD_SetCharacterAblityIcons : MonoBehaviour
{
    public GameObject basicAbilityImages;
    public GameObject castAbilityImages;
    public GameObject utilityAbilityImages;
    public GameObject movementAbilityImages;
    public GameObject ultimatedAbilityImages;

    public Sprite basicAbilitySprite;
    public Sprite castAbilitySprite;
    public Sprite utilityAbilitySprite;
    public Sprite movementAbilitySprite;
    public Sprite ultimatedAbilitySprite;

    [SerializeField]
    private string whatCharacter;

    private void Awake()
    {
        if(whatCharacter == ("Zylar"))
        {
            basicAbilityImages.GetComponent<Image>().sprite = basicAbilitySprite;
            castAbilityImages.GetComponent<Image>().sprite = castAbilitySprite;
            utilityAbilityImages.GetComponent<Image>().sprite = utilityAbilitySprite;
            movementAbilityImages.GetComponent<Image>().sprite = movementAbilitySprite;
            ultimatedAbilityImages.GetComponent<Image>().sprite = ultimatedAbilitySprite;
        }
    }
}
