using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JB_DisplayAbilityInfoUI : MonoBehaviour
{
    public Image abilityIcon;
    public Text abilityTextBox;

    public AbilitySwap thisAbility;

    private void Start()
    {
        // avoiding errors while empty
        if(thisAbility.abilityIcon != null)
        {
            abilityIcon = thisAbility.abilityIcon;
        }
        
        abilityTextBox.text = thisAbility.abilityInfoText;
    }
    
    public void ToggleTooltipInfo(bool toggle)
    {
        abilityIcon.enabled = toggle;
        abilityTextBox.enabled = toggle;
        

    }
}
