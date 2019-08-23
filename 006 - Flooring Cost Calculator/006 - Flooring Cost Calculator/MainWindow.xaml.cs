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

namespace _006___Flooring_Cost_Calculator
{

    public partial class MainWindow : Window
    {
        Tile SelectedTile = new Tile();
        double? RoomWidth;
        double? RoomDepth;

        // creates instances of Tile for each of the three types of tile
        readonly Tile SmallCheapTile = new Tile()
        {
            Name = "Small Cheap Tiles",
            Area = 0.03,
            Price = 1
        };
        readonly Tile VeryMediumTile = new Tile()
        {
            Name = "Very Medium Tiles",
            Area = 0.1,
            Price = 5
        };
        readonly Tile BigExpensiveTile = new Tile()
        {
            Name = "Big Expensive Tiles",
            Area = 0.35,
            Price = 25
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();

            if (!NullAnswers())
            {
                AssignInputToVariables();
                double RoomArea = Convert.ToDouble(RoomDepth * RoomWidth);
                long NumberOfTiles = Convert.ToInt64(Math.Ceiling(RoomArea / SelectedTile.Area));
                double TotalPrice = NumberOfTiles * SelectedTile.Price;

                OutputText.Text = String.Format(
                    "To floor this room with {0}, you will need {1} tiles. This will cost £{2} in total.",
                    SelectedTile.Name,NumberOfTiles,TotalPrice);
            }
        }

        // resets to start conditions
        private void Reset()
        {
            OutputText.Text = "";
            SelectedTile = new Tile();
            RoomWidth = null;
            RoomDepth = null;
        }

        // Displays error messages and returns true if any of the three answers are null.
        private bool NullAnswers()
        {
            if (FlooringTypeSelect.SelectedItem == null)
            {
                OutputText.Text = "Please select a flooring type and try again.";
                return true;
            }
            else if (WidthInput.Value == null)
            {
                OutputText.Text = "Please input the room's width and try again.";
                return true;
            }
            else if (DepthInput.Value == null)
            {
                OutputText.Text = "Please input the room's depth and try again.";
                return true;
            }
            else
                return false;
        }

        // assigns the three inputs to RoomWidth, RoomDepth, and SelectedTile respectively
        private void AssignInputToVariables()
        {
            RoomWidth = WidthInput.Value;
            RoomDepth = DepthInput.Value;

            if (SmallTilesOption.IsSelected)
                SelectedTile = SmallCheapTile;
            else if (MediumTilesOption.IsSelected)
                SelectedTile = VeryMediumTile;
            else
                SelectedTile = BigExpensiveTile;
        }
    }

    public class Tile
    {
        public string Name { get; set; }
        public double Area { get; set; }
        public double Price { get; set; }
    }
}
