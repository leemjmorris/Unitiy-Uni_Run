using UnityEngine;

/// <summary>
/// ���׸� ���Ӽ� �̱��� ���̽� Ŭ����
/// �پ��� Ÿ���� �̱����� ���� ������ �� �ְ� �մϴ�.
/// </summary>
/// <typeparam name="T">�̱������� ���� MonoBehaviour Ÿ��</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    // ������ ����: ���� �������� �ߺ� ���Ÿ� ������ų �� ����
    [Tooltip("�����: �ߺ� �ν��Ͻ� ���Ÿ� ��������� ȣ��� ������ ����")]
    [SerializeField]
    private bool m_DelayDuplicateRemoval;

    // �̱��� �ν��Ͻ��� ���� ���� ����
    private static T s_Instance;

    // �̱��� �ν��Ͻ��� ���� ���� ������
    public static T Instance
    {
        get
        {
            // �ν��Ͻ��� ������ ������ ã�ų� ����
            if (s_Instance == null)
            {
                s_Instance = FindFirstObjectByType<T>();

                if (s_Instance == null)
                {
                    // �ν��Ͻ��� ������ ���� ����
                    SetupInstance();
                }
                else
                {
                    // �ν��Ͻ��� �̹� �����ϸ� �α� ���
                    string typeName = typeof(T).Name;
                    Debug.Log("[Singleton] " + typeName + " instance already created: " +
                                s_Instance.gameObject.name);
                }
            }

            return s_Instance;
        }
    }

    // MonoBehaviour �����ֱ� - �ʱ�ȭ
    public virtual void Awake()
    {
        // ������ ���� �ߺ� ���� ��� ���� �Ǵ� ����
        if (!m_DelayDuplicateRemoval)
            RemoveDuplicates();
    }

    // �̱��� �ν��Ͻ� ���� �� ����
    private static void SetupInstance()
    {
        // ������ ���� �ν��Ͻ� �˻�
        s_Instance = FindFirstObjectByType<T>();

        // ������ ���� ����
        if (s_Instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;

            s_Instance = gameObj.AddComponent<T>();
            // �� ��ȯ�ÿ��� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObj);
        }
    }

    // �ߺ� �ν��Ͻ� ó��
    public void RemoveDuplicates()
    {
        if (s_Instance == null)
        {
            // ó�� ������ �ν��Ͻ���� ����ϰ� ����
            s_Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (s_Instance != this)
        {
            // �̹� �ν��Ͻ��� ������ �� �ߺ� ��ü�� �ı�
            Destroy(gameObject);
        }
    }
}
