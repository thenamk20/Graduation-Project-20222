using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : Singleton<CharacterPreview>
{
    [SerializeField]
    private List<GameObject> characters;

    [SerializeField]
    private GameObject previewCam;

    [SerializeField]
    private GameObject previewLight;

    public void ToggleCharacterPreview(bool isEnable)
    {
        previewCam.SetActive(isEnable);
        previewLight.SetActive(isEnable);
    }

    private void Start()
    {
        ChangePreviewModel();
        EventGlobalManager.Instance.OnChangeCharacter.AddListener(ChangePreviewModel);
    }

    void ChangePreviewModel()
    {
        for(int i=0; i< characters.Count; i++)
        {
            characters[i].SetActive(i == GameManager.Instance.data.user.currentCharacter);
        }
    }
}
