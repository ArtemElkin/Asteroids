namespace _Project.Core.Save
{
    public abstract class BaseSaveController<TModel, TSave> : ISaveBootstrap
        where TModel : ISaveable<TSave>
        where TSave : ISave
    {
        private readonly ISaveService _saveService;
        private readonly TModel _model;
        protected abstract string FileName { get; }


        protected BaseSaveController(ISaveService saveService, TModel model)
        {
            _saveService = saveService;
            _model = model;
        }

        public void Save()
        {
            _saveService.Save(_model.GetSave(), FileName);
        }

        public void LoadOnBootstrap()
        {
            var save = _saveService.Load<TSave>(FileName);
            if (save != null)
            {
                _model.LoadSave(save);
            }
        }
    }
}