using GameOfThronesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace GameOfThronesApp.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<House> GOTHouses { get; set; } = new ObservableCollection<House>();
        public ObservableCollection<Character> GOTCharacters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Book> GOTBooks { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> GOTPovBooks { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Character> GOTBookCharacters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Character> GOTBookPOVCharacters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Character> GOTHouseSwornMembers { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<House> GOTHouseCadetBranches { get; set; } = new ObservableCollection<House>();

        public static List<string> CharacterLabels { get; set; } = new List<string>()
            {
                "",
                "Gender",
                "Culture",
                "Born",
                "Died",
                "Father",
                "Mother",
                "Spouse",
                "Titles",
                "Aliases",
                "Allegiances",
                "Tv series",
                "Played by"
            };
        public static List<string> BookLabels { get; set; } = new List<string>()
            {
               "",
               "ISBN",
               "Authors",
               "Number of pages",
               "Publisher",
               "Country",
               "MediaType",
               "Released",
               "Characters",
               "POV Characters"
            };
        public static List<string> HouseLabels { get; set; } = new List<string>()
            {
               "",
               "Region",
               "Coat of arms",
               "Words",
               "Titles",
               "Seats",
               "Current lord",
               "Heir",
               "Overlord",
               "Founded",
               "Founder",
               "Died out",
               "Ancestral weapons",
               "Cadet branches",
               "SwornMembers"
            };

        /// <summary>
        /// Init the first 20 characters to the app
        /// </summary>
        public async void InitCharacters()
        {
            
            await GOTFacade.AddCharactersToAppAsync(GOTCharacters,20);
            
            //GOTCharacters = await GOTFacade.GetAllCharacterListAsync();
        }

        /// <summary>
        /// Ovverrided method from ViewModelBase
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var characters = await GOTFacade.GetAllCharacterListAsync();
            foreach (var item in characters)
            {
                GOTCharacters.Add(item);
            }
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Updating the CharacterDetails grid datasources
        /// </summary>
        /// <param name="selectedCharacter"></param>
        public async void UpdateCharacterDetails(Character selectedCharacter)
        {
            GOTBooks.Clear();
            GOTPovBooks.Clear();
            GOTHouses.Clear();
            
            await GOTFacade.GetDataListAsync(selectedCharacter.books, GOTBooks);
            await GOTFacade.GetDataListAsync(selectedCharacter.povBooks, GOTPovBooks);
            await GOTFacade.GetDataListAsync(selectedCharacter.allegiances, GOTHouses);
        }

        /// <summary>
        /// Updating a single data of a character
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Returns with the data string value</returns>
        public async Task<string> UpdateSigleCharacterDetail(string url)
        {
            Character data = await GOTFacade.GetSingleDataAsync<Character>(url);
            return data == null ? "" : data.name;
        }

        /// <summary>
        /// Updating the BookDetail grid datasources
        /// </summary>
        /// <param name="selectedBook"></param>
        public async void UpdateBookDetails(Book selectedBook)
        {
            GOTBookCharacters.Clear();
            GOTBookPOVCharacters.Clear();
            await GOTFacade.GetDataListAsync(selectedBook.characters, GOTBookCharacters);
            await GOTFacade.GetDataListAsync(selectedBook.povCharacters, GOTBookPOVCharacters);
        }

        /// <summary>
        /// Updting a the HouseDetail grid datasources
        /// </summary>
        /// <param name="selectedHouse"></param>
        public async void UpdateHouseDetails(House selectedHouse)
        {
            GOTHouseSwornMembers.Clear();
            GOTHouseCadetBranches.Clear();
            await GOTFacade.GetDataListAsync(selectedHouse.swornMembers, GOTHouseSwornMembers);
            await GOTFacade.GetDataListAsync(selectedHouse.cadetBranches, GOTHouseCadetBranches);
        }

        /// <summary>
        /// Clear the housepanel's datasources
        /// </summary>
        public void ClearHouseData()
        {
            GOTHouseSwornMembers.Clear();
            GOTHouseCadetBranches.Clear();
        }

        /// <summary>
        /// Clear the bookpanel's datasources
        /// </summary>
        public void ClearBookData()
        {
            GOTBookCharacters.Clear();
            GOTBookPOVCharacters.Clear();
        }
    }
}
