using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instance = null;
    public static Managers Instance { get { return s_instance; } }

    private static ResourceManager s_resourceManager = new ResourceManager();
    private static GameManagerEx s_gameManager = new GameManagerEx();
    private static UIManager s_UIManager = new UIManager();

    
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }
    public static GameManagerEx Game { get { Init(); return s_gameManager; } }
    public static UIManager UI { get { Init(); return s_UIManager; } }
    
    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        // 초기화 안됐다면 새로 생성함
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            s_instance = Utils.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);
            
            s_resourceManager.Init();

            // 프레임 60으로 설정
            Application.targetFrameRate = 60;
        }
    }
}
