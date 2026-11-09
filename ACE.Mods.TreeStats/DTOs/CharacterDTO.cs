namespace ACE.Mods.TreeStats.DTOs
{
    public class CharacterDTO
    {
        [JsonPropertyName("account_name")]
        public string? AccountName { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; } = 2;

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("race")]
        public string? Race { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("level")]
        public int? Level { get; set; }

        [JsonPropertyName("allegiance_name")]
        public string? AllegianceName { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("followers")]
        public int? Followers { get; set; }

        [JsonPropertyName("server")]
        public string? Server { get; set; } = ConfigManager.Config.Server.WorldName;

        [JsonPropertyName("server_population")]
        public int? ServerPopulation { get; set; }

        [JsonPropertyName("deaths")]
        public int? Deaths { get; set; }

        [JsonPropertyName("birth")]
        public string? Birth { get; set; }

        [JsonPropertyName("total_xp")]
        public long? TotalXp { get; set; }

        [JsonPropertyName("unassigned_xp")]
        public long? UnassignedXp { get; set; }

        [JsonPropertyName("skill_credits")]
        public int? SkillCredits { get; set; }

        [JsonPropertyName("luminance_earned")]
        public long? LuminanceEarned { get; set; }

        [JsonPropertyName("luminance_total")]
        public long? LuminanceTotal { get; set; }

        [JsonPropertyName("attribs")]
        public Dictionary<string, Attribute>? Attribs { get; set; }

        [JsonPropertyName("vitals")]
        public Dictionary<string, Vital>? Vitals { get; set; }

        [JsonPropertyName("skills")]
        public Dictionary<string, Skill>? Skills { get; set; }

        [JsonPropertyName("monarch")]
        public AllegianceMember? Monarch { get; set; }

        [JsonPropertyName("patron")]
        public AllegianceMember? Patron { get; set; }

        [JsonPropertyName("vassals")]
        public List<AllegianceMember>? Vassals { get; set; }

        [JsonPropertyName("current_title")]
        public int? CurrentTitle { get; set; }

        [JsonPropertyName("titles")]
        public HashSet<int>? Titles { get; set; }

        [JsonPropertyName("properties")]
        public Dictionary<int, int>? Properties { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        

        public CharacterDTO()
        {
        }

        public CharacterDTO(Player player)
        {
            Name = player.Name.TrimStart('+');

            //Server = ConfigManager.Config.Server.WorldName;

            //var creature = new Creature(player.Biota);

            Race = player.HeritageGroupName;
            Gender = player.Sex;

            Level = player.GetProperty(PropertyInt.Level);

            //var allegiance = AllegianceManager.GetAllegiance(player);
            var allegiance = player.Allegiance;
            if (allegiance != null)
            {
                //var allegianceNode = AllegianceManager.GetAllegianceNode(player);
                var allegianceNode = player.AllegianceNode;
                if (allegianceNode != null)
                {
                    AllegianceName = allegiance.AllegianceName;
                    Rank = (int)allegianceNode.Rank;
                    Followers = allegianceNode.TotalFollowers;

                    Monarch = new() { Name = allegianceNode.Monarch.Player.Name.TrimStart('+'), Race = allegianceNode.Monarch.Player.Heritage, Rank = (int)allegianceNode.Monarch.Rank, Gender = allegianceNode.Monarch.Player.Gender, Followers = allegianceNode.Monarch.TotalFollowers };
                    if (allegianceNode.Patron != null)
                        Patron = new() { Name = allegianceNode.Patron.Player.Name.TrimStart('+'), Race = allegianceNode.Patron.Player.Heritage, Rank = (int)allegianceNode.Patron.Rank, Gender = allegianceNode.Patron.Player.Gender };

                    foreach (var vassal in allegianceNode.Vassals.Values)
                    {
                        Vassals ??= new();
                        Vassals.Add(new() { Name = vassal.Player.Name.TrimStart('+'), Race = vassal.Player.Heritage, Rank = (int)vassal.Rank, Gender = vassal.Player.Gender });
                    }
                }
            }

            Deaths = player.GetProperty(PropertyInt.NumDeaths);
            Birth = $"{Time.GetDateTimeFromTimestamp(player.GetProperty(PropertyInt.CreationTimestamp) ?? 0):M/d/yyyy h:mm:ss tt}";

            TotalXp = player.GetProperty(PropertyInt64.TotalExperience);
            UnassignedXp = player.GetProperty(PropertyInt64.AvailableExperience);
            SkillCredits = player.GetProperty(PropertyInt.AvailableSkillCredits);

            LuminanceEarned = player.GetProperty(PropertyInt64.AvailableLuminance);
            LuminanceTotal = player.GetProperty(PropertyInt64.MaximumLuminance);

            Attribs = new()
            {
                [player.Strength.Attribute.ToString().ToLower()] = new() { Name = player.Strength.Attribute.ToString(), Base = (int)player.Strength.Base, Creation = (int)player.Strength.StartingValue },
                [player.Endurance.Attribute.ToString().ToLower()] = new() { Name = player.Endurance.Attribute.ToString(), Base = (int)player.Endurance.Base, Creation = (int)player.Endurance.StartingValue },
                [player.Quickness.Attribute.ToString().ToLower()] = new() { Name = player.Quickness.Attribute.ToString(), Base = (int)player.Quickness.Base, Creation = (int)player.Quickness.StartingValue },
                [player.Coordination.Attribute.ToString().ToLower()] = new() { Name = player.Coordination.Attribute.ToString(), Base = (int)player.Coordination.Base, Creation = (int)player.Coordination.StartingValue },
                [player.Focus.Attribute.ToString().ToLower()] = new() { Name = player.Focus.Attribute.ToString(), Base = (int)player.Focus.Base, Creation = (int)player.Focus.StartingValue },
                [player.Self.Attribute.ToString().ToLower()] = new() { Name = player.Self.Attribute.ToString(), Base = (int)player.Self.Base, Creation = (int)player.Self.StartingValue },
            };

            Vitals = new()
            {
                [player.Health.Vital.ToSentence().Replace("Maximum ", "").ToLower().ToString()] = new() { Name = player.Health.Vital.ToSentence().Replace("Maximum ", ""), Base = (int)player.Health.Base },
                [player.Stamina.Vital.ToSentence().Replace("Maximum ", "").ToLower().ToString()] = new() { Name = player.Stamina.Vital.ToSentence().Replace("Maximum ", ""), Base = (int)player.Stamina.Base },
                [player.Mana.Vital.ToSentence().Replace("Maximum ", "").ToLower().ToString()] = new() { Name = player.Mana.Vital.ToSentence().Replace("Maximum ", ""), Base = (int)player.Mana.Base },
            };

            Skills = [];
            foreach (var skill in Enum.GetValues<ACE.Entity.Enum.Skill>())
            {
                var creatureSkill = player.GetCreatureSkill(skill, false);

                if (creatureSkill != null)
                {
                    Skills.Add(skill.ToSentence().Replace(" ", "_").ToLower().ToString(), new()
                    {
                        Name = skill.ToSentence().Replace(" ", "_").ToLower().ToString(),
                        Base = (int)creatureSkill.Base,
                        Training = $"{(creatureSkill.AdvancementClass == SkillAdvancementClass.Untrained ? $"{(creatureSkill.IsUsable ? "Untrained" : "Unusable")}" : creatureSkill.AdvancementClass.ToString())}"
                    });
                }
            }

            Properties = new();
            var propertiesInt = player.GetAllPropertyIntWhere(SendOnLoginProperties.PropertiesInt);
            foreach (var property in propertiesInt)
                Properties.TryAdd((int)property.Key, property.Value);

            CurrentTitle = player.GetProperty(PropertyInt.CharacterTitleId);
            //var character = DatabaseManager.Shard.BaseDatabase.GetCharacter(player.Guid.Full);
            var character = player.Character;
            if (character != null)
            {
                //var titles = character.GetTitles(new()).Select(x => x.TitleId).ToHashSet();
                var titles = character.CharacterPropertiesTitleBook.Select(x => (int)x.TitleId).ToHashSet();
                if (titles != null)
                {
                    Titles = titles;
                }
            }
        }

        public CharacterDTO(OfflinePlayer player)
        {
            Name = player.Name.TrimStart('+');

            //Server = ConfigManager.Config.Server.WorldName;

            var creature = new Creature(player.Biota);

            Race = creature.HeritageGroupName;
            Gender = creature.Sex;

            Level = player.GetProperty(PropertyInt.Level);

            var allegiance = AllegianceManager.GetAllegiance(player);
            if (allegiance != null)
            {
                var allegianceNode = AllegianceManager.GetAllegianceNode(player);
                if (allegianceNode != null)
                {
                    AllegianceName = allegiance.AllegianceName;
                    Rank = (int)allegianceNode.Rank;
                    Followers = allegianceNode.TotalFollowers;

                    Monarch = new() { Name = allegianceNode.Monarch.Player.Name.TrimStart('+'), Race = allegianceNode.Monarch.Player.Heritage, Rank = (int)allegianceNode.Monarch.Rank, Gender = allegianceNode.Monarch.Player.Gender, Followers = allegianceNode.Monarch.TotalFollowers };
                    if (allegianceNode.Patron != null)
                        Patron = new() { Name = allegianceNode.Patron.Player.Name.TrimStart('+'), Race = allegianceNode.Patron.Player.Heritage, Rank = (int)allegianceNode.Patron.Rank, Gender = allegianceNode.Patron.Player.Gender };

                    foreach(var vassal in allegianceNode.Vassals.Values)
                    {
                        Vassals ??= new();
                        Vassals.Add(new() { Name = vassal.Player.Name.TrimStart('+'), Race = vassal.Player.Heritage, Rank = (int)vassal.Rank, Gender = vassal.Player.Gender });
                    }
                }
            }

            Deaths = player.GetProperty(PropertyInt.NumDeaths);
            Birth = $"{Time.GetDateTimeFromTimestamp(player.GetProperty(PropertyInt.CreationTimestamp) ?? 0):M/d/yyyy h:mm:ss tt}";

            TotalXp = player.GetProperty(PropertyInt64.TotalExperience);
            UnassignedXp = player.GetProperty(PropertyInt64.AvailableExperience);
            SkillCredits = player.GetProperty(PropertyInt.AvailableSkillCredits);

            LuminanceEarned = player.GetProperty(PropertyInt64.AvailableLuminance);
            LuminanceTotal = player.GetProperty(PropertyInt64.MaximumLuminance);

            Attribs = new()
            {
                [creature.Strength.Attribute.ToString().ToLower()] = new() { Name = creature.Strength.Attribute.ToString(), Base = (int)creature.Strength.Base, Creation = (int)creature.Strength.StartingValue },
                [creature.Endurance.Attribute.ToString().ToLower()] = new() { Name = creature.Endurance.Attribute.ToString(), Base = (int)creature.Endurance.Base, Creation = (int)creature.Endurance.StartingValue },
                [creature.Quickness.Attribute.ToString().ToLower()] = new() { Name = creature.Quickness.Attribute.ToString(), Base = (int)creature.Quickness.Base, Creation = (int)creature.Quickness.StartingValue },
                [creature.Coordination.Attribute.ToString().ToLower()] = new() { Name = creature.Coordination.Attribute.ToString(), Base = (int)creature.Coordination.Base, Creation = (int)creature.Coordination.StartingValue },
                [creature.Focus.Attribute.ToString().ToLower()] = new() { Name = creature.Focus.Attribute.ToString(), Base = (int)creature.Focus.Base, Creation = (int)creature.Focus.StartingValue },
                [creature.Self.Attribute.ToString().ToLower()] = new() { Name = creature.Self.Attribute.ToString(), Base = (int)creature.Self.Base, Creation = (int)creature.Self.StartingValue },
            };

            Vitals = new()
            {
                [creature.Health.Vital.ToSentence().Replace("Maximum ","").ToLower().ToString()] = new() { Name = creature.Health.Vital.ToSentence().Replace("Maximum ", ""), Base = (int)creature.Health.Base },
                [creature.Stamina.Vital.ToSentence().Replace("Maximum ", "").ToLower().ToString()] = new() { Name = creature.Stamina.Vital.ToSentence().Replace("Maximum ", ""), Base = (int)creature.Stamina.Base },
                [creature.Mana.Vital.ToSentence().Replace("Maximum ", "").ToLower().ToString()] = new() { Name = creature.Mana.Vital.ToSentence().Replace("Maximum ", ""), Base = (int)creature.Mana.Base },
            };

            Skills = [];
            foreach (var skill in Enum.GetValues<ACE.Entity.Enum.Skill>())
            {
                var creatureSkill = creature.GetCreatureSkill(skill, false);

                if (creatureSkill != null)
                {
                    Skills.Add(skill.ToSentence().Replace(" ", "_").ToLower().ToString(), new() { Name = skill.ToSentence().Replace(" ", "_").ToLower().ToString(), Base = (int)creatureSkill.Base,
                        Training = $"{(creatureSkill.AdvancementClass == SkillAdvancementClass.Untrained ? $"{(creatureSkill.IsUsable ? "Untrained" : "Unusable")}" : creatureSkill.AdvancementClass.ToString())}"
                    });
                }
            }

            Properties = new();
            var propertiesInt = creature.GetAllPropertyIntWhere(SendOnLoginProperties.PropertiesInt);
            foreach(var property in propertiesInt)
                Properties.TryAdd((int)property.Key, property.Value);

            CurrentTitle = player.GetProperty(PropertyInt.CharacterTitleId);
            var character = DatabaseManager.Shard.BaseDatabase.GetCharacter(player.Guid.Full);
            if (character != null)
            {
                //var titles = character.GetTitles(new()).Select(x => x.TitleId).ToHashSet();
                var titles = character.CharacterPropertiesTitleBook.Select(x => (int)x.TitleId).ToHashSet();
                if (titles != null)
                {
                    Titles = titles;
                }
            }
        }        
    }

    public class Attribute
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("base")]
        public int? Base { get; set; }

        [JsonPropertyName("creation")]
        public int? Creation { get; set; }
    }

    public class Vital
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("base")]
        public int? Base { get; set; }
    }

    public class Skill
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("base")]
        public int? Base { get; set; }

        [JsonPropertyName("training")]
        public string? Training { get; set; }
    }

    public class AllegianceMember
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("race")]
        public int? Race { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("gender")]
        public int? Gender { get; set; }

        [JsonPropertyName("followers")]
        public int? Followers { get; set; }
    }
}
