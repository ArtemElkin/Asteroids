using _Project.Core.Save;
using _Project.Core.StaticData;

namespace _Project.Core.Player
{
    public sealed class PlayerSaveController : SaveController<PlayerModel, PlayerSave>
    {
        protected override string FileName => FileNames.Save.Player;

        public PlayerSaveController(ISaveService saveService, PlayerModel playerModel) 
            : base(saveService, playerModel) { }
    }
}