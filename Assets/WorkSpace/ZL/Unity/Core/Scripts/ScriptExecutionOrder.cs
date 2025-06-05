namespace ZL.Unity
{
    public enum ScriptExecutionOrder
    {
        Min = -100,

        Singleton,

        Tweener,

        Awake,

        Default = 0,

        SceneDirector,
    }
}