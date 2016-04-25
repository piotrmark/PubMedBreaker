using System;
using System.Collections.Generic;
using System.Windows;
using QueryHandler;

namespace PubMedBreaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string userQuery = textBoxQuery.Text;
            int resultsNumber = 0;
            if (integerUpDownResultNumber.Value != null)
            {
                resultsNumber = integerUpDownResultNumber.Value.Value;
            }

            List<UserQueryResult> results = await UserQueryHandler.GetResultsForQuery(userQuery, resultsNumber);

            textBoxResults.Text = String.Empty;

            foreach (var res in results)
            {
                textBoxResults.Text += (res.ArticleTitle + "\n" + "\n");
            }
        }
    }
}
