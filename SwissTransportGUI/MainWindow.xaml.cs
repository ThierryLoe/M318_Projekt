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
   
    public partial class MainWindow : Window
    {
        private readonly Transport _transport;
        public MainWindow()
        {
            InitializeComponent();
            _transport = new Transport();
            btnVerbindunganzeigen.IsEnabled = false;
            btnStationzuruecksetzen.IsEnabled = false;
            btnStationSuchen.IsEnabled = false;
        }

        private void BtnSuchezuruecksetzenClick(object sender, RoutedEventArgs e)
        {
            txtboxVon.Clear();
            txtboxBis.Clear();
            txtboxDatum.Clear();
            txtboxUhrzeit.Clear();
        }

        private void BtnVerbindunganzeigenClick(object sender, RoutedEventArgs e)
        {
            List<Connection> connections = new List<Connection>();
            List<ConnectionView> connectionViews = new List<ConnectionView>();
            string from = txtboxVon.Text;
            string until = txtboxBis.Text;

            if (!string.IsNullOrEmpty(txtboxDatum.Text) && !string.IsNullOrEmpty(txtboxUhrzeit.Text))
            {

                if (!DateTime.TryParse(txtboxDatum.Text, out _) || !DateTime.TryParse(txtboxUhrzeit.Text, out _))
                {
                    MessageBox.Show("Fehler beim Datum\nFormat Datum 24.02.2022\nFormat Zeit 12:15");
                }
                else if (DateTime.TryParse(txtboxDatum.Text, out DateTime date1) && DateTime.TryParse(txtboxUhrzeit.Text, out DateTime time1))
                {
                    connections = _transport.GetConnections(from, until, time1.ToString("HH:mm"), date1.ToString("dd.MM.yyyy")).ConnectionList;
                }
            }
            else
            {
                connections = _transport.GetConnections(from, until).ConnectionList;
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
        private void BtnVerbindungClick(object sender, RoutedEventArgs e)
        {
            tabCtrl.SelectedIndex = 0;
        }

        private void BtnAbfahrtstafelClick(object sender, RoutedEventArgs e)
        {
            tabCtrl.SelectedIndex = 1;
        }

        private void BtnStationzuruecksetzenClick(object sender, RoutedEventArgs e)
        {
            comboboxStationSuchen.Text = string.Empty;
        }

        private void BtnStationSuchenClick(object sender, RoutedEventArgs e)
        {
            string stationname = comboboxStationSuchen.Text;
            List<StationBoard> stations = _transport.GetStationBoard(stationname, "id").Entries;
            List<DepartureTableView> departures = new List<DepartureTableView>();

            foreach(StationBoard stationBoard in stations)
            {
                departures.Add(new DepartureTableView(stationBoard.To, stationBoard.Stop.Departure.ToString(), stationBoard.Category + stationBoard.Number));
            }

            dataGridStationen.ItemsSource = departures;
        }

        

        private void ComboboxStationSuchenTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(comboboxStationSuchen.Text))
            {
                btnStationzuruecksetzen.IsEnabled = true;
                btnStationSuchen.IsEnabled = true;
                List<Station> stations = _transport.GetStations(comboboxStationSuchen.Text).StationList;
                List<string> staionnames = new List<string>();

                foreach (Station station in stations)
                {
                    staionnames.Add(station.Name);
                }

                comboboxStationSuchen.ItemsSource = staionnames;
                comboboxStationSuchen.IsDropDownOpen = true;
            }
            else
            {
                btnStationzuruecksetzen.IsEnabled = false;
                btnStationSuchen.IsEnabled= false;
            }
        }

        private void TxtboxBisTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtboxBis.Text) && !string.IsNullOrEmpty(txtboxVon.Text))
            {
                btnVerbindunganzeigen.IsEnabled = true;
            }
            else
            {
                btnVerbindunganzeigen.IsEnabled = false;
            }
        }
    }
}
