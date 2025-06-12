namespace ZL.Unity
{
    public enum ScriptExecutionOrder
    {
        Min = -100,

        Singleton,

        Tweener,

        FastAwake,

        Default = 0,

        SceneDirector,
    }
}