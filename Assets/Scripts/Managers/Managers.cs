using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; //유일성 보장,싱글톤 사용
    static Managers Instance
    { // 유일한 매니저를 갖고온다, 프로퍼티 사용
        get
        {
            init();
            return s_instance;
        }
    }

    InputManager inputManager = new InputManager();
    public static InputManager Input
    {
        get
        {
            return Instance.inputManager;
        }
    }

    ResourceManager resourceManager = new ResourceManager();
    public static ResourceManager Resource
    {
        get
        {
            return Instance.resourceManager;
        }
    }

    // Start is called before the first frame update
    void Start()
    //아무리 각각의 Managers Instance가 Start()호출해도 실제로 전역에 저장되는 것은 managers 원본만 인식함
    {
        init();
    }

    void Update()
    {
        inputManager.OnUpdate();
    }

    /*
     * 혹시라도 @Managers가 없을 경우 getInstacne시 Instance가 null인 상태로 들어온다.
     * 이후에 코드가 추가시 manager  접근해서 무언ㄴ가를 하려고 하면 crash가 발생하게 된다.
     */
    static void init()
    {
        if (s_instance == null)
        {
            GameObject gameObject = GameObject.Find("@Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject
                {
                    name = "@Managers"
                };
                gameObject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(gameObject);
            s_instance = gameObject.GetComponent<Managers>();
        }

    }
}
