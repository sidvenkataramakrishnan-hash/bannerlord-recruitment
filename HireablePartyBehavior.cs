using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EagleRadiantCrossClean
{
    public class HireablePartyBehavior : CampaignBehaviorBase
    {
        private struct FactionTroop
        {
            public string CultureId;
            public string TroopId;
            public string Label;
            public int    Cost;
            public string MenuId;
        }

        private static readonly FactionTroop[] FactionTroops =
        {
            // Vlandia
            new FactionTroop { CultureId = "vlandia",  TroopId = "imperial_recruit", Label = "Marienburg Pikeman",            Cost = 180, MenuId = "erc_ft_vlandia_1" },
            new FactionTroop { CultureId = "vlandia",  TroopId = "imperial_recruit", Label = "Swadian Demilancer",             Cost = 280, MenuId = "erc_ft_vlandia_2" },
            new FactionTroop { CultureId = "vlandia",  TroopId = "imperial_recruit", Label = "Zollern Great Armour",           Cost = 380, MenuId = "erc_ft_vlandia_3" },
            new FactionTroop { CultureId = "vlandia",  TroopId = "imperial_recruit", Label = "Ulm Mounted Arquebusier",        Cost = 300, MenuId = "erc_ft_vlandia_4" },
            // Empire
            new FactionTroop { CultureId = "empire",   TroopId = "imperial_recruit", Label = "Lowland Infantry",               Cost = 100, MenuId = "erc_ft_empire_1" },
            new FactionTroop { CultureId = "empire",   TroopId = "imperial_recruit", Label = "Deva Spearman",                  Cost = 150, MenuId = "erc_ft_empire_2" },
            new FactionTroop { CultureId = "empire",   TroopId = "imperial_recruit", Label = "Mylesian Horseman",              Cost = 200, MenuId = "erc_ft_empire_3" },
            new FactionTroop { CultureId = "empire",   TroopId = "imperial_recruit", Label = "Kara-Khitan Archer",             Cost = 200, MenuId = "erc_ft_empire_4" },
            new FactionTroop { CultureId = "empire",   TroopId = "imperial_recruit", Label = "Brigadier of the Radiant Cross",  Cost = 250, MenuId = "erc_ft_empire_5" },
            new FactionTroop { CultureId = "empire",   TroopId = "imperial_recruit", Label = "Gunther-Piedmont Gunner",         Cost = 200, MenuId = "erc_ft_empire_9" },
            new FactionTroop { CultureId = "empire",   TroopId = "imperial_recruit", Label = "Ptian Cavalry",                  Cost = 350, MenuId = "erc_ft_empire_8" },
            // Sturgia
            new FactionTroop { CultureId = "sturgia",  TroopId = "imperial_recruit", Label = "Laurian Royal Guard",            Cost = 300, MenuId = "erc_ft_sturgia_1" },
            new FactionTroop { CultureId = "sturgia",  TroopId = "imperial_recruit", Label = "Rosrolian Knight",               Cost = 350, MenuId = "erc_ft_sturgia_2" },
            new FactionTroop { CultureId = "sturgia",  TroopId = "imperial_recruit", Label = "Laurian Herreruelo",             Cost = 180, MenuId = "erc_ft_sturgia_3" },
            new FactionTroop { CultureId = "sturgia",  TroopId = "imperial_recruit", Label = "Inquisitor",                     Cost = 250, MenuId = "erc_ft_sturgia_4" },
            // Nord
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Haelmarian Musketeer",           Cost = 180, MenuId = "erc_ft_nord_1" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Haelmarian Axeman",              Cost = 150, MenuId = "erc_ft_nord_2" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Haelmarian Pikeman",             Cost = 150, MenuId = "erc_ft_nord_3" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Berthe Musketeer",               Cost = 180, MenuId = "erc_ft_nord_4" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Berthe Axeman",                  Cost = 150, MenuId = "erc_ft_nord_5" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Berthe Pikeman",                 Cost = 150, MenuId = "erc_ft_nord_6" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Nordland Musketeer",             Cost = 180, MenuId = "erc_ft_nord_7" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Nordland Axeman",                Cost = 150, MenuId = "erc_ft_nord_8" },
            new FactionTroop { CultureId = "nord",     TroopId = "imperial_recruit", Label = "Nordland Pikeman",               Cost = 150, MenuId = "erc_ft_nord_9" },
            // Battania
            new FactionTroop { CultureId = "battania", TroopId = "imperial_recruit", Label = "Vaegir Cossack",                 Cost = 200, MenuId = "erc_ft_battania_1" },
            new FactionTroop { CultureId = "battania", TroopId = "imperial_recruit", Label = "Vaegir Streltsy",                Cost = 200, MenuId = "erc_ft_battania_2" },
            // Aserai
            new FactionTroop { CultureId = "aserai",   TroopId = "imperial_recruit", Label = "Western Volunteer",              Cost = 100, MenuId = "erc_ft_aserai_1" },
            new FactionTroop { CultureId = "aserai",   TroopId = "imperial_recruit", Label = "Northern Volunteer",             Cost = 100, MenuId = "erc_ft_aserai_2" },
            new FactionTroop { CultureId = "aserai",   TroopId = "imperial_recruit", Label = "Eastern Volunteer",              Cost = 100, MenuId = "erc_ft_aserai_3" },
            new FactionTroop { CultureId = "aserai",   TroopId = "imperial_recruit", Label = "Duchy Drabant",                  Cost = 250, MenuId = "erc_ft_aserai_4" },
            new FactionTroop { CultureId = "aserai",   TroopId = "imperial_recruit", Label = "Duchy Hussar",                   Cost = 300, MenuId = "erc_ft_aserai_5" },
            new FactionTroop { CultureId = "aserai",   TroopId = "imperial_recruit", Label = "Duchy Registered Cossacks",      Cost = 280, MenuId = "erc_ft_aserai_6" },
            // Khuzait
            new FactionTroop { CultureId = "khuzait",  TroopId = "imperial_recruit", Label = "Ormeli Sipahi",                  Cost = 280, MenuId = "erc_ft_khuzait_1" },
            new FactionTroop { CultureId = "khuzait",  TroopId = "imperial_recruit", Label = "Ormeli Janissary",               Cost = 200, MenuId = "erc_ft_khuzait_2" },
            new FactionTroop { CultureId = "khuzait",  TroopId = "imperial_recruit", Label = "Ormeli Voynik",                  Cost = 150, MenuId = "erc_ft_khuzait_3" },
            new FactionTroop { CultureId = "khuzait",  TroopId = "imperial_recruit", Label = "Sarranid Mercenary Jezzail",     Cost = 250, MenuId = "erc_ft_khuzait_4" },
            new FactionTroop { CultureId = "khuzait",  TroopId = "imperial_recruit", Label = "Sarranid Mercenary Mamluke",     Cost = 350, MenuId = "erc_ft_khuzait_5" },
        };

        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
        }

        public override void SyncData(IDataStore dataStore) { }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            starter.AddGameMenuOption(
                "town",
                "erc_hire_faction_troops",
                "Hire faction soldiers.",
                args =>
                {
                    args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                    return Settlement.CurrentSettlement != null && Settlement.CurrentSettlement.IsTown;
                },
                args => GameMenu.SwitchToMenu("erc_faction_troop_menu")
            );

            starter.AddGameMenu("erc_faction_troop_menu",
                "Local soldiers await employment. Pay them and they march with you.", null);

            foreach (FactionTroop ft in FactionTroops)
            {
                FactionTroop captured = ft;
                starter.AddGameMenuOption(
                    "erc_faction_troop_menu",
                    captured.MenuId,
                    captured.Label + " (" + captured.Cost + " gold)",
                    args =>
                    {
                        args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                        string culture = Settlement.CurrentSettlement?.Culture?.StringId ?? "";
                        CharacterObject troop = CharacterObject.Find(captured.TroopId);
                        return troop != null && culture == captured.CultureId && Hero.MainHero.Gold >= captured.Cost;
                    },
                    args =>
                    {
                        CharacterObject troop = CharacterObject.Find(captured.TroopId);
                        if (troop != null && Hero.MainHero.Gold >= captured.Cost)
                        {
                            Hero.MainHero.ChangeHeroGold(-captured.Cost);
                            MobileParty.MainParty.MemberRoster.AddToCounts(troop, 1);
                            InformationManager.DisplayMessage(new InformationMessage(
                                captured.Label + " recruited. (-" + captured.Cost + " gold)", Colors.Green));
                        }
                        GameMenu.SwitchToMenu("erc_faction_troop_menu");
                    }
                );
            }

            starter.AddGameMenuOption(
                "erc_faction_troop_menu",
                "erc_faction_troop_leave",
                "Leave.",
                args => { args.optionLeaveType = GameMenuOption.LeaveType.Leave; return true; },
                args => GameMenu.SwitchToMenu("town"),
                true
            );
        }
    }
}
