namespace ZL.Unity
{
    public enum ScriptExecutionOrder
    {
        Min = -99,

        Singleton,

        UIBuilder,

        Default = 0,

        SceneDirector,

        Max = 99
    }
}