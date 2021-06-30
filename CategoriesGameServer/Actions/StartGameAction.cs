using CategoriesGameContracts.Contracts;

namespace CategoriesGameServer.Actions
{
    public class StartGameAction
    {
        public SettingsDto Settings { get; set; }
        public string UserName { get; set; }
    }
}
