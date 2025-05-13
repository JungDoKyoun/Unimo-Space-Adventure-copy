namespace ZL.Unity.UI
{
    public interface ITextController
    {
        public string Text { get; set; }

        public void SetText(int value);

        public void SetText(float value);
    }
}