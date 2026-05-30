using _Project.Core.Save;

namespace _Project.Core.Player
{
    public class PlayerSaveController
    {
        private readonly ISaveService _saveService;
        private readonly PlayerModel _playerModel;


        public PlayerSaveController(ISaveService saveService, PlayerModel playerModel)
        {
            _saveService = saveService;
			_playerModel = playerModel;
        }

        public void SaveProgress()
        {
            _saveService.Save(_playerModel.GetSave());
        }
        
    }
}