using SwissTransport.Core;
using SwissTransport.Models;
using SwissTransportGUI.Models;
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

namespace SwissTransportGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void btnSuchezuruecksetzen_Click(object sender, RoutedEventArgs e)
        {
            txtboxVon.Clear();
            txtboxBis.Clear();
            txtboxDatum.Clear();
            txtboxUhrzeit.Clear();
        }

        private void btnVerbindunganzeigen_Click(object sender, RoutedEventArgs e)
        {
            Transport transport = new Transport();
            List<Connection> connections = new List<Connection>();
            List<ConnectionView> connectionViews = new List<ConnectionView>();
            string date = string.Empty;
            string time = string.Empty;
            string from = txtboxVon.Text;
            string until = txtboxBis.Text;
            if (!string.IsNullOrEmpty(txtboxDatum.Text) && !string.IsNullOrEmpty(txtboxUhrzeit.Text))
            {
                DateTime date1;
                DateTime time1;
                if (!DateTime.TryParse(txtboxDatum.Text, out date1) || !DateTime.TryParse(txtboxUhrzeit.Text, out time1))
                {
                    MessageBox.Show("Fehler beim Datum\nFormat Datum 24.02.2022\nZeit 12:15");
                }
                else if (DateTime.TryParse(txtboxDatum.Text, out date1) && DateTime.TryParse(txtboxUhrzeit.Text, out time1))
                {
                    date = date1.ToString("dd.MM.yyyy");
                    time = time1.ToString("HH:mm");
                    connections = transport.GetConnections(from, until, time, date).ConnectionList;
                }
            }
            else
            {
                connections = transport.GetConnections(from, until).ConnectionList;
            }

            if (connections.Count > 0)
            {
                foreach (Connection connection in connections)
                {

                    connectionViews.Add(new ConnectionView(connection.From.Station.Name, Convert.ToDateTime(connection.From.Departure), connection.To.Station.Name, connection.From.Platform, connection.To.Platform));

                }
                dataGridVerbindungen.ItemsSource = connectionViews;
            }
  
        }
        private void btnVerbindung_Click(object sender, RoutedEventArgs e)
        {
            tabCtrl.SelectedIndex = 0;
        }

        private void btnAbfahrtstafel_Click(object sender, RoutedEventArgs e)
        {
            tabCtrl.SelectedIndex = 1;
        }

        private void btnStationzuruecksetzen_Click(object sender, RoutedEventArgs e)
        {
            comboboxStationSuchen.Text = string.Empty;
        }

        private void btnStationSuchen_Click(object sender, RoutedEventArgs e)
        {
            string stationname = comboboxStationSuchen.Text;
            Transport transport = new Transport();
            List<StationBoard> stations = transport.GetStationBoard(stationname, "id").Entries;
            List<DepartureTableView> departures = new List<DepartureTableView>();  
            foreach(StationBoard stationBoard in stations)
            {
                departures.Add(new DepartureTableView(stationBoard.To, stationBoard.Stop.Departure.ToString(), stationBoard.Category + stationBoard.Number));
            }
            dataGridStationen.ItemsSource = departures;
        }

        private void tabCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboboxStationSuchen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(comboboxStationSuchen.Text))
            {
                Transport transport = new Transport();
                List<Station> stations = transport.GetStations(comboboxStationSuchen.Text).StationList;
                List<string> staionnames = new List<string>();
                foreach (Station station in stations)
                {
                    staionnames.Add(station.Name);
                }
                comboboxStationSuchen.ItemsSource = staionnames;
                comboboxStationSuchen.IsDropDownOpen = true;
            }
        }
    }
}
