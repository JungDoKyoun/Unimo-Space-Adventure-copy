namespace ZL.Unity
{
    public enum ScriptExecutionOrder
    {
        Min = -100,

        Singleton,

        Tweener,

        Default = 0,

        SceneDirector,
    }
}