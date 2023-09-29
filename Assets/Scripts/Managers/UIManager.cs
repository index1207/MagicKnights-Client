using Core;
using UnityEngine;

public class UIManager : BaseManager
{
    public LoadingUI LoadingUI
    {
        get
        {
            if (_loading == null)
            {
                LoadItems();
                if (_loading == null)
                {
                    return null;
                }
            }
            
            return _loading;
        }
    }

    public PlayButton PlayButton
    {
        get
        {
            if (_play == null)
            {
                LoadItems();
                if (_play == null)
                {
                    return null;
                }
            }
            
            return _play;
        }
    }
    public Popup Popup
    {
        get
        {
            if (_popup == null)
            {
                LoadItems();
                if (_popup == null)
                {
                    return null;
                }
            }
            
            return _popup;
        }
    }

    public RoomMenu Room
    {
        get
        {
            if (_room == null)
            {
                LoadItems();
                if (_room == null)
                {
                    return null;
                }
            }

            return _room;
        }
    }

    private LoadingUI _loading;
    private PlayButton _play;
    private Popup _popup;
    private RoomMenu _room;
    private GameScene _curScene;

    public void Init()
    {
        LoadItems();
        if(PlayButton != null)
            PlayButton.SetActive(false);
    }

    public void LoadItems()
    {
        UIItem[] items = Object.FindObjectsOfType<UIItem>();
        foreach (UIItem elem in items)
        {
            switch (elem.Type)
            {
                case Define.UIType.Loading:
                    _loading = (LoadingUI)elem;
                    break;
                case Define.UIType.Play:
                    _play = (PlayButton)elem;
                    break;
                case Define.UIType.Popup:
                    _popup = (Popup)elem;
                    _popup.gameObject.SetActive(false);
                    break;
                case Define.UIType.Room:
                    _room = (RoomMenu)elem;
                    _room.gameObject.SetActive(false);
                    break;
            }
        }

        _curScene = Object.FindObjectOfType<GameScene>();
    }

    public void Update()
    {
        
    }
}