namespace GUILayer
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            //DrawGridWithBordersAndLabels();
        }


        private void DrawGridWithBordersAndLabels()
        {
            // Anzahl der Zeilen und Spalten im Grid
            int numRows = mainGrid.RowDefinitions.Count;
            int numColumns = mainGrid.ColumnDefinitions.Count;

            // Schleife zum Hinzufügen von Zeilen und Spalten
            for (int column = 0; column < numColumns; column++)
            {


                for (int row = 0; row < numRows; row++)
                {

                    // Border erstellen
                    Border border = new Border();


                    // Label erstellen und beschriften
                    Label label = new()
                    {
                        Text = $"({column}, {row})" // Beschriftung des Labels
                    };

                    // Border und Label dem Grid hinzufügen
                    border.Content = label;
                    mainGrid.Add(border, column, row);
                }
            }
        }

        private void BtnA_Clicked(object sender, EventArgs e)
        {
            if (BtnA.Text == "A")
            {
                BtnA.Text = "Clicked";
                return;
            }
            BtnA.Text = "A";
        }

        private void BtnB_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnC_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnD_Clicked(object sender, EventArgs e)
        {

        }
    }

}
