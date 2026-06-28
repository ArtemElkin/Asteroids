using System.Collections.Generic;

namespace _Project.Features.UI.Settings
{
    public class SettingsCoordinator
    {
        private int _currentPageId;
        private ISettingsPage _currentSettingsPage;
        private readonly List<ISettingsPage> _settingsPages;


        public SettingsCoordinator(List<ISettingsPage> settingsPages)
        {
            _settingsPages = settingsPages;
            _currentSettingsPage =  _settingsPages[0];
        }

        public void NextPage()
        {
            _currentSettingsPage.Hide();
            _currentPageId++;
            if (_currentPageId == _settingsPages.Count)
            {
                _currentPageId = 0;
            }
            _currentSettingsPage = _settingsPages[_currentPageId];
            _currentSettingsPage.Show();
        }
    }
}