namespace ZL.Unity
{
    public enum ScriptExecutionOrder
    {
        Min = -100,

        Loader,

        Singleton,

        Tweener,

        Default = 0,

        Lazy,

        SceneDirector
    }
}