using System;
using System.Windows;
using QueryHandler;

namespace PubMedBreaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly UserQueryHandler _uqh = new UserQueryHandler();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Button.IsEnabled = false;
            try
            {
                string userQuery = TextBoxQuery.Text;
                int resultsNumber = 0;
                if (IntegerUpDownResultNumber.Value != null)
                {
                    resultsNumber = IntegerUpDownResultNumber.Value.Value;
                }

                int timeout = 0;
                if (IntegerUpDownTimeout.Value != null)
                {
                    timeout = IntegerUpDownTimeout.Value.Value;
                }

                FinalResultsSet results = await _uqh.GetResultsForQuery(userQuery, resultsNumber, timeout);

                TextBoxUnifiedQuery.Text = results.UnifiedQuery;

                ListViewResults.ItemsSource = results.UserQueryResults;

                StatusLabel.Content =
                    $"Wykonano zapytania w: {results.ExecutionTimeMilis} ms, przetworzono {results.ProcesedSynonymsCount} z {results.SynonymsCount} synonimów";
            }
            catch (Exception exc)
            {
                StatusLabel.Content = exc.Message;
            }
            Button.IsEnabled = true;
            
            
        }
    }
}
