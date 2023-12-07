using System.Windows;
using System.Windows.Controls;

namespace GuiLayer;

/// <summary>
/// Interaktionslogik für ChooseOptionsPage.xaml
/// </summary>
public partial class ChooseOptionsPage : Page
{
    int maxQuestions = 0;
    int choosenAmountOfQuestions = 0;
    bool pChecked = false;
    bool apChecked = false;
    bool mChecked = false;
    bool eChecked = false;
    bool fChecked = false;
    public ChooseOptionsPage()
    {
        InitializeComponent();
        UpdateChooseAmountText();
    }



    private void BtnContinue_Click(object sender, RoutedEventArgs e)
    {
        pChecked = Prüfungsfragen.IsChecked ?? false;
        apChecked = APfragen.IsChecked ?? false;
        mChecked = Mfragen.IsChecked ?? false;
        eChecked = Efragen.IsChecked ?? false;
        fChecked = Ffragen.IsChecked ?? false;
        string choosenAmountQuestions = ChoosenAmountQuestions.Text;

        try
        {
            choosenAmountOfQuestions = int.Parse(choosenAmountQuestions);
        }
        catch (Exception)
        {

            MessageBox.Show("Bitte einen gültigen Wert eingeben!");
            return;
        }

        if ((pChecked || apChecked || mChecked || eChecked || fChecked) && choosenAmountOfQuestions >= 1 && choosenAmountOfQuestions <= maxQuestions)
        {
            MainWindow.Frame.Navigate(new Uri("QuestionsPage.xaml", UriKind.Relative));
        }
        else if ((pChecked || apChecked || mChecked || eChecked || fChecked) is false)
        {
            MessageBox.Show("Bitte wählen Sie mindestens eine Option aus!");
        }
        else
        {
            MessageBox.Show("Bitte einen gültigen Wert eingeben!");
        }
    }

    private void UpdateChooseAmountText()
    {
        if (maxQuestions == 0)
        {
            ChooseAmountText.Content = "Bitte wählen Sie mindestens eine Option aus!";
            return;
        }
        ChooseAmountText.Content = $"Geben Sie ein Zahl zwischen 1 und {maxQuestions} ein: ";
    }

    private void APfragen_Checked(object sender, RoutedEventArgs e)
    {
        maxQuestions += 5;
        UpdateChooseAmountText();
    }

    private void APfragen_Unchecked(object sender, RoutedEventArgs e)
    {
        maxQuestions -= 5;
        UpdateChooseAmountText();
    }

    private void Mfragen_Checked(object sender, RoutedEventArgs e)
    {
        maxQuestions += 5;
        UpdateChooseAmountText();
    }

    private void Mfragen_Unchecked(object sender, RoutedEventArgs e)
    {
        maxQuestions -= 5;
        UpdateChooseAmountText();
    }

    private void Ffragen_Checked(object sender, RoutedEventArgs e)
    {
        maxQuestions += 5;
        UpdateChooseAmountText();
    }

    private void Ffragen_Unchecked(object sender, RoutedEventArgs e)
    {
        maxQuestions -= 5;
        UpdateChooseAmountText();
    }

    private void Efragen_Checked(object sender, RoutedEventArgs e)
    {
        maxQuestions += 5;
        UpdateChooseAmountText();
    }

    private void Efragen_Unchecked(object sender, RoutedEventArgs e)
    {
        maxQuestions -= 5;
        UpdateChooseAmountText();
    }

    private void Prüfungsfragen_Checked(object sender, RoutedEventArgs e)
    {
        maxQuestions += 5;
        UpdateChooseAmountText();
    }

    private void Prüfungsfragen_Unchecked(object sender, RoutedEventArgs e)
    {
        maxQuestions -= 5;
        UpdateChooseAmountText();
    }
}
