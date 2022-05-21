using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Solitair.Classes;
using Solitair_gui.Helper;

namespace Solitair_gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SolitairGui : Window
    {
        
        private readonly GuiPlayer _player;
        
        public SolitairGui()
        {
            _player = new GuiPlayer();
            _player.CurrentField += PlayerOnCurrentField;
            _player.NewStatus += PlayerOnNewStatus;
            InitializeComponent();
            FillGui(_player.PlayingField);
        }

        private void PlayerOnNewStatus(object? sender, string e)
        {
            lblStatus.Content = "Status: " + e;
        }

        private void PlayerOnCurrentField(object? sender, PlayingField e)
        {
            FillGui(_player.PlayingField);
        }

        protected virtual void OnOnNextStep()
        {
            _player.PlayRound();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OnOnNextStep();
        }

        private void FillGui(PlayingField playingField)
        {
            lblPile.Content = playingField._pile.Pile.Count;

            if (playingField._pile.GetCard() != null)
            {
                lblTalon.Content = playingField._pile.GetCard();
            }

            if (playingField._clubsDeck.GetCard() != null)
            {
                lblClubsDeck.Content = playingField._clubsDeck.GetCard();
            }

            if (playingField._heartsDeck.GetCard() != null)
            {
                lblHeartsDeck.Content = playingField._heartsDeck.GetCard();
            }

            if (playingField._diamondsDeck.GetCard() != null)
            {
                lblDiamondsDeck.Content = playingField._diamondsDeck.GetCard();
            }

            if (playingField._spadesDeck.GetCard() != null)
            {
                lblSpadesDeck.Content = playingField._spadesDeck.GetCard();
            }

            lbBuild1.Items.Clear();
            lbBuild2.Items.Clear();
            lbBuild3.Items.Clear();
            lbBuild4.Items.Clear();
            lbBuild5.Items.Clear();
            lbBuild6.Items.Clear();
            lbBuild7.Items.Clear();
            
           
            foreach (Label buildStack1 in playingField._buildStack1.Cards.Select(CardHelper.GetCardLabel).Reverse())
            {
                lbBuild1.Items.Add(buildStack1);
            }
            
            foreach (Label buildStack2 in playingField._buildStack2.Cards.Select(CardHelper.GetCardLabel).Reverse())
            {
                lbBuild2.Items.Add(buildStack2);
            }

            foreach (Label buildStack3 in playingField._buildStack3.Cards.Select(CardHelper.GetCardLabel).Reverse())
            {
                lbBuild3.Items.Add(buildStack3);
            }

            foreach (Label buildStack4 in playingField._buildStack4.Cards.Select(CardHelper.GetCardLabel).Reverse())
            {
                lbBuild4.Items.Add(buildStack4);
            }

            foreach (Label buildStack5 in playingField._buildStack5.Cards.Select(CardHelper.GetCardLabel).Reverse())
            {
                lbBuild5.Items.Add(buildStack5);
            }

            foreach (Label buildStack6 in playingField._buildStack6.Cards.Select(CardHelper.GetCardLabel).Reverse())
            {
                lbBuild6.Items.Add(buildStack6);
            }

            foreach (Label buildStack7 in playingField._buildStack7.Cards.Select(CardHelper.GetCardLabel).Reverse())
            {
                lbBuild7.Items.Add(buildStack7);
            }
            
        }
        
    }
}