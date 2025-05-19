namespace ZL.Unity
{
    public enum ScriptExecutionOrder
    {
        Min = -99,

        Singleton,

        Default = 0,

        SceneDirector,

        Max = 99
    }
}