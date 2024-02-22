using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_CollectItem : UI_Base
{
    enum Texts
    {
        LevelText,
    }

    enum Buttons
    {
        MergeButton
    }

    CollectData _collectData;
    public CollectData CollectData
    {
        get { return _collectData; }
    }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetText((int)Texts.LevelText).text = "";
        GetButton((int)Buttons.MergeButton).image.sprite = null;
        GetButton((int)Buttons.MergeButton).gameObject.BindEvent(OnPointerClick, Define.UIEvent.Click);
        
        return true;
    }

    public void SetInfo(int level)
    {
        Init();
        if (level == 0)
        {
            InitUI();
            return;
        }
            
        if (Managers.Data.Collections.TryGetValue(level, out _collectData) == false)
        {
            Debug.Log($"{level}, {_collectData}");
            return;
        }
        
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (_init == false)
            return;

        GetText((int)Texts.LevelText).text = $"{_collectData.Level}";
        GetButton((int)Buttons.MergeButton).image.sprite = Managers.Resource.Load<Sprite>("Sprites/Name/box_16");
        GetButton((int)Buttons.MergeButton).image.color = Utils.HexColor(_collectData.Hex);
    }

    public void InitUI()
    {
        _collectData = null;
        GetText((int)Texts.LevelText).text = "";
        GetButton((int)Buttons.MergeButton).image.sprite = null;
        GetButton((int)Buttons.MergeButton).image.color = Color.white;
    }

    public int GetCollectLevel()
    {
        return _collectData.Level;
    }
    
    
    public void OnPointerClick()
    {
        if (_collectData == null)
            return;
        GetComponentInParent<UI_CollectItemBowl>().MergeCollectItem(gameObject.transform.GetSiblingIndex(), _collectData.Level);
    }
    
}
