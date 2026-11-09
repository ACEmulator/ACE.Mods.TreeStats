namespace ACE.Mods.TreeStats.WebServices
{
    public class CharacterService
    {
        [ResourceMethod("")]
        public List<string> GetCharacters()
        {
            var players = PlayerManager.GetAllPlayers();
            return [.. players.OrderBy(player => player.Guid.Full).Select(player => player.Name)];
        }

        [ResourceMethod(":name")]
        public CharacterDTO? GetCharacter(string name)
        {
            var player = PlayerManager.FindByName(name, out var isOnline);

            if (player == null)
                return null;

            if (isOnline)
            {
                var onlinePlayer = player as Player;

                if (onlinePlayer != null)
                    return new(onlinePlayer);
                else
                    return null;
            }
            else
            {
                var offlinePlayer = player as OfflinePlayer;

                if (offlinePlayer != null)
                    return new(offlinePlayer);
                else
                    return null;
            }
        }
    }
}
