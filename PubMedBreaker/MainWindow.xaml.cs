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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string userQuery = textBoxQuery.Text;

            List<UserQueryResult> results = UserQueryHandler.GetResultsForQuery(userQuery);

            textBoxResults.Text = String.Empty;

            foreach (var res in results)
            {
                textBoxResults.Text += (res.ArticleTitle + "\n");
            }
        }
    }
}
