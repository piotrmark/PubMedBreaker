using System;
using System.Collections.Generic;
using System.Windows;
using QueryHandler;
using MeSHService.Xml;
using MeSHService;
using MeSHService.Model;

namespace PubMedBreaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private UserQueryHandler uqh = new UserQueryHandler();
        public MainWindow()
        {
            InitializeComponent();
            //var xmlRes = XmlMeshReader.Read();
            //MeshDictionaryBuilder builder = new MeshDictionaryBuilder();
            //MeshDictionary meshModel = builder.Build(xmlRes);

        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Button.IsEnabled = false;
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

            FinalResultsSet results = await uqh.GetResultsForQuery(userQuery, resultsNumber, timeout);

            TextBoxResults.Text = String.Empty;

            foreach (var res in results.UserQueryResults)
            {
                TextBoxResults.Text += (res.ArticleTitle + "\n" + "\n");
            }
            statusLabel.Content = String.Format("Wykonano zapytania w: {0} ms, przetworzono {1} z {2} synonimów", results.ExecutionTimeMilis, results.ProcesedSynonymsCount, results.SynonymsCount);
            Button.IsEnabled = true;
        }
    }
}
