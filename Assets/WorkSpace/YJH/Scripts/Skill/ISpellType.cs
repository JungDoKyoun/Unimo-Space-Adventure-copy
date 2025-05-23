public interface ISpellType
{
    public void UseSpell();

    public void InitSpell();

    public void UpdateTime();

    public void SetPlayer(PlayerManager player);

    public bool ReturnState();
}