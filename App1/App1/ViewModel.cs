using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                if (string.IsNullOrEmpty(value))
                    MovieList = new ObservableCollection<Search>(movies.Search);
                OnpropertyChanged();
            }
        }
        public Command FilterCommand
        {
            get
            {
                return new Command(() =>
                {
                    List<Search> searchList = new List<Search>();
                    if (string.IsNullOrEmpty(SearchText))
                        MovieList = new ObservableCollection<Search>(movies.Search);
                    else
                    {
                        for (int i = 0; i < movies.Search.Count; i++)
                        {
                            if (movies.Search[i].Title.ToLower().Contains(SearchText.ToLower()))
                                searchList.Add(movies.Search[i]);
                        }
                        MovieList = new ObservableCollection<Search>(searchList);
                    }
                });
            }
        }

        //public ICommand FilterCommand { private set; get; }

        public MovieViewModel()
        {
            client = new HttpClient();
            //FilterCommand = new Command(OnFilterCommand);
            GetMoviesList();
        }

        private void OnFilterCommand(object obj)
        {

        }

        private async void GetMoviesList()
        {
            try
            {
                Uri uri = new Uri(string.Format("https://www.omdbapi.com/?apikey=b9bd48a6&s=Marvel&type=movie", string.Empty));
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
