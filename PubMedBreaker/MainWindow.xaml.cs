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
            string userQuery = TextBoxQuery.Text;
            int resultsNumber = 0;
            if (IntegerUpDownResultNumber.Value != null)
            {
                resultsNumber = IntegerUpDownResultNumber.Value.Value;
            }

            List<UserQueryResult> results = await UserQueryHandler.GetResultsForQuery(userQuery, resultsNumber);

            TextBoxResults.Text = String.Empty;

            foreach (var res in results)
            {
                TextBoxResults.Text += (res.ArticleTitle + "\n" + "\n");
            }
        }
    }
}
