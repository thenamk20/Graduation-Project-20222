using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeCharacterTest : HCMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentCharText;

    private void Start()
    {
        SetCurrentCharText();
    }

    public void ChangeCharacter()
    {
        Gm.data.user.currentCharacter += 1;
        if(Gm.data.user.currentCharacter == Cfg.characters.Count)
        {
            Gm.data.user.currentCharacter = 0;
        }
        SetCurrentCharText();
    }

    void SetCurrentCharText() 
    {
        currentCharText.text = Gm.data.user.currentCharacter.ToString();
    }
}
