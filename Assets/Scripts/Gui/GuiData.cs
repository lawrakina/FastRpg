using Gui.Characters;
using UnityEngine;


namespace Gui
{
    [CreateAssetMenu(fileName = "GuiData", menuName = "Data/GuiData")]
    public sealed class GuiData : ScriptableObject
    {
        public NavigationBar NavigationBar;
        public CharacterPanel CharacterPanel;
        public EquipmentPanel EquipmentPanel;
        public BattlePanel BattlePanel;
        public SpellsPanel SpellsPanel;
        public TalentsPanel TalentsPanel;
    }
}