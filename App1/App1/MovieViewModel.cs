using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{

    public class MovieViewModel : INotifyPropertyChanged
    {
        HttpClient client;
        private ObservableCollection<Search> movieList;
        private Movies movies;

        public ObservableCollection<Search> MovieList
        {
            get { return movieList; }
            set { movieList = value; OnpropertyChanged(); }
        }
        private bool isIndicatorBusy;

        public bool IsIndicatorBusy
        {
            get { return isIndicatorBusy; }
            set { isIndicatorBusy = value; OnpropertyChanged(); }
        }


        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnpropertyChanged();
            }
        }
        public  Command FilterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsIndicatorBusy = true;
                     await GetMoviesList(SearchText);
                    IsIndicatorBusy = false;
                });
            }
        }

        public MovieViewModel()
        {
            client = new HttpClient();
        }
        private async Task GetMoviesList(string title)
        {
            try
            {
                Uri uri = new Uri(string.Format("https://www.omdbapi.com/?apikey=b9bd48a6&s=" + title + "&type=movie", string.Empty));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<Movies>(content);
                    MovieList = new ObservableCollection<Search>(movies.Search);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnpropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Movies
    {
        public List<Search> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

    public class Search
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
