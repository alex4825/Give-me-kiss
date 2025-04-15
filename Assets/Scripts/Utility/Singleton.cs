using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

//так тут будет в целом общее ревью, рекомендации и впечатления
//в целом - реально неплохо, понятно что делалось в спешке
//когда возникает подребность в огромном количестве статики и синглотонов - значит что то не в порядке
//стоит смотреть в сторону DependecyInjection фреймворков(например Zenject/Extenject) и в целом более управляемого кода
//когда ты сам создаёшь и прокидываешь зависимости - ты управляешь процессом, а с синглтонами так не получается
//также можно посмотреть в сторону слоёв приложения - какие есть, как могут взаимодействовать и как лучше выстраивать связи
//также можно почитать про паттерны самые базовые и про семейство MVC паттернов(там всякие MVP, MVA, MVVM и тд в контексте Unity)
//вот,а так в целом всё неплохо, хорошая работа)
//а и я поиграть нормально не смог, ошибки выбивало какие-то, мб из-за gpt

public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }
}
