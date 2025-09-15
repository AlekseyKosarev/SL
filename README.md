```csharp
public class EntryPoint
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        //your logic
        //GameInitializer.Init();
        //and etc
    }
}


Requirements:
DS.Models - for type Result<T>
```