using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace ProdaneVozy
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			
			InitializeComponent();
			List<string> names = new List<string>();
			List<double> costs = new List<double>();
			List<double> costWithDPHs = new List<double>();
			//Create OpenFileDialog
			OpenFileDialog openFileDialog = new OpenFileDialog();

			//Set filter for file extension and default file extension
			openFileDialog.DefaultExt = ".xml";
			openFileDialog.Filter = "All files (*.*)|*.*|XML File (*.xml)|*.xml";

			//Display OpenFileDialog by calling ShowDialog method
			Nullable<bool> result = openFileDialog.ShowDialog();

			
			if (result == true)
			{
				string text = "";
				XDocument xdoc = XDocument.Load(openFileDialog.FileName);
				foreach (XElement element in xdoc.Descendants("Car"))
				{
					string name = element.Element("Name").Value;
					double cost = double.Parse(element.Element("Cost").Value);
					double DPH = double.Parse(element.Element("DPH").Value);
					double costWithDPH = cost + (cost * DPH / 100);
					if (names.Contains(name))
					{
						int index = names.IndexOf(name);
						costs[index] += cost;
						costWithDPHs[index] += costWithDPH;
					}
					else
					{
						names.Add(name);
						costs.Add(cost);
						costWithDPHs.Add(costWithDPH);
					}
				}
				//int numberOfRow = names.Count;
				//Grid.SetRow(grid, numberOfRow);


				sellCar.Margin = new Thickness(10);
				sellCar.ShowGridLines = true;
				// Define the Rows and Columns
				//J at and is just for difrend
				ColumnDefinition columnDefJ = new ColumnDefinition();
				sellCar.ColumnDefinitions.Add(columnDefJ);

				for(int countRows = 0; countRows <= names.Count; countRows++)
				{
					RowDefinition rowDefJ = new RowDefinition();
					sellCar.RowDefinitions.Add(rowDefJ);
				}

				//Add the cells to the Grid
				for(int indexCell = 0; indexCell < names.Count; indexCell++)
				{
					//Header of the grid
					if(indexCell == 0)
					{
						Grid singleCellMain = new Grid();
						singleCellMain.Background = Brushes.Gray;

						DefineRowsAndColumns(2, singleCellMain);

						// Create and add to the Grid cell
						CreateTextBlock("Název Modelu", 0, 0, singleCellMain);
						CreateTextBlock("Cena bez DPH", 0, 1, singleCellMain);
						CreateTextBlock("Cena s DPH", 1, 1, singleCellMain);

						//Add the Grid element to the Grid Children collection
						sellCar.Children.Add(singleCellMain);
					}

					Grid singleCell = new Grid();

					DefineRowsAndColumns(2, singleCell);

					CreateTextBlock(names[indexCell], 0, 0, singleCell);
					CreateTextBlock(costs[indexCell].ToString(), 0, 1, singleCell);					
					CreateTextBlock(costWithDPHs[indexCell].ToString(), 1, 1, singleCell);

					sellCar.Children.Add(singleCell);

					Grid.SetColumn(singleCell, 0);
					Grid.SetRow(singleCell, indexCell + 1);


				}



				// Add the first text cell to the Grid
				TextBlock txt1 = new TextBlock();
				txt1.Text = "Ahoj";
				txt1.FontSize = 20;
				txt1.FontWeight = FontWeights.Bold;
				Grid.SetColumn(txt1, 0);
				Grid.SetRow(txt1, 0);

			}
		}

		/// <summary>
		/// Create and add textbox in Grid
		/// </summary>
		/// <param name="text">text</param>
		/// <param name="column">column</param>
		/// <param name="row">row</param>
		/// <param name="grid">grid</param>
		private void CreateTextBlock(string text, int column, int row, Grid grid)
		{
			// Set value in textblock
			TextBlock txtValue = new TextBlock();
			txtValue.Text = text;
			txtValue.VerticalAlignment = VerticalAlignment.Center;
			txtValue.Background = Brushes.Transparent;
			txtValue.FontSize = 25;
			txtValue.Margin = new Thickness(10, 0, 0, 0);
			txtValue.FontWeight = FontWeights.Bold;
			Grid.SetColumn(txtValue, column);
			Grid.SetRow(txtValue, row);

			//Add the TextBox elements to the Grid Children collection
			grid.Children.Add(txtValue);
		}

		/// <summary>
		/// Define Rows and Columns with same number
		/// </summary>
		/// <param name="count">Count rows and column</param>
		/// <param name="grid"></param>
		private void DefineRowsAndColumns(int count, Grid grid)
		{
			for (int countRowAndCols = 0; countRowAndCols < 2; countRowAndCols++)
			{
				ColumnDefinition columnDef = new ColumnDefinition();
				RowDefinition rowDef = new RowDefinition();
				grid.RowDefinitions.Add(rowDef);
				grid.ColumnDefinitions.Add(columnDef);
			}
		}

		
	}
}
