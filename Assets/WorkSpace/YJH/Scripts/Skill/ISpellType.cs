using Photon.Pun;

public interface ISpellType
{
    [PunRPC]
    public void UseSpell();

    public void InitSpell();

    public void UpdateTime();

    public void SetPlayer(PlayerManager player);

    public bool ReturnState();
    public void SetState(bool state);
}