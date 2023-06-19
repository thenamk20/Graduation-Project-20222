using System.Collections.Generic;
using UnityEngine;

public class PopupCharacters : UIPanel
{
    [SerializeField]
    private List<CharacterSlot> characterSlots;

    [SerializeField]
    private List<SelectCharacterButton> selectCharacterButtons;

    public static PopupCharacters Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupCharacters;
    }

    public static void Show()
    {
        var newInstance = (PopupCharacters) GUIManager.Instance.NewPanel(UiPanelType.PopupCharacters);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();

        Init();
    }

    private void Init()
    {
        foreach(var selectBtn in selectCharacterButtons)
        {
            selectBtn.Setup(OpenSlot);
        }
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void OpenSlot(int slotIndex)
    {
        for(int i=0; i < characterSlots.Count; i++)
        {
            if(i == slotIndex)
            {
                characterSlots[i].Init();   
            }
            characterSlots[i].gameObject.SetActive(i == slotIndex);
        }

        for(int i = 0; i < selectCharacterButtons.Count; i++)
        {
            selectCharacterButtons[i].SetActive(i == slotIndex);
        }
    }
}