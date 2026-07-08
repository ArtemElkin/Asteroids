using _Project.Core.Save;
using _Project.Core.StaticData;

namespace _Project.Features.Common.Settings
{
    public sealed class SettingsSaveController : BaseSaveController<SettingsModel, SettingsSave>
    {
        protected override string FileName => FileNames.Save.GameSettings;

        public SettingsSaveController(ISaveService saveService, SettingsModel settingsModel) 
            : base(saveService, settingsModel) { }
    }
}