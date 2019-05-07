﻿using GameOfThronesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GameOfThronesApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<House> GOTHouses { get; set; }
        private ObservableCollection<Character> GOTCharacters { get; set; }
        public ObservableCollection<Book> GOTBooks { get; set; }
        public ObservableCollection<Book> GOTPovBooks { get; set; }
        public ObservableCollection<Character> GOTBookCharacters { get; set; }
        public ObservableCollection<Character> GOTBookPOVCharacters { get; set; }
        public ObservableCollection<Character> GOTHouseSwornMembers { get; set; }
        public ObservableCollection<House> GOTHouseCadetBranches { get; set; }

        private List<string> CharacterLabels { get; set; }
        private List<string> BookLabels { get; set; }
        private List<string> HouseLabels { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            GOTCharacters = new ObservableCollection<Character>();
            GOTBooks = new ObservableCollection<Book>();
            GOTPovBooks = new ObservableCollection<Book>();
            GOTHouses = new ObservableCollection<House>();
            GOTBookCharacters = new ObservableCollection<Character>();
            GOTBookPOVCharacters = new ObservableCollection<Character>();
            GOTHouseSwornMembers = new ObservableCollection<Character>();
            GOTHouseCadetBranches = new ObservableCollection<House>();

            CharacterLabels = new List<string>()
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
            BookLabels = new List<string>()
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
            HouseLabels = new List<string>()
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
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            while (GOTCharacters.Count < 10)
            {
                await GOTFacade.AddCharactersToAppAsync(GOTCharacters);
            }

            InitBookDetailGrid();
            InitCharacterDetailGrid();
            InitHouseDetailGrid();

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;
        }

        private void InitHouseDetailGrid()
        {
            int rowindex = 0;
            foreach (var childItem in HouseDetailGrid.Children)
            {
                childItem.SetValue(Grid.RowProperty, rowindex++);
                childItem.SetValue(Grid.ColumnProperty, 1);
                childItem.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
                childItem.SetValue(MarginProperty, new Thickness(10, 0, 0, 0));
            }

            //add labels
            for (int i = 0; i < rowindex; i++)
            {
                if (i == 0)
                {
                    HouseDetailGrid.Children[i].SetValue(Grid.ColumnProperty, 0);
                    HouseDetailGrid.Children[i].SetValue(Grid.ColumnSpanProperty, 2);
                    continue;
                }

                TextBlock label = new TextBlock();
                label.Text = HouseLabels[i];
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);
                HouseDetailGrid.Children.Add(label);
            }
        }

        private void InitBookDetailGrid()
        {
            int rowindex = 0;
            foreach (var childItem in BookDetailGrid.Children)
            {
                childItem.SetValue(Grid.RowProperty, rowindex++);
                childItem.SetValue(Grid.ColumnProperty, 1);
                childItem.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
                childItem.SetValue(MarginProperty, new Thickness(10, 0, 0, 0));
            }

            //add labels
            for (int i = 0; i < rowindex; i++)
            {
                if (i == 0)
                {
                    BookDetailGrid.Children[i].SetValue(Grid.ColumnProperty, 0);
                    BookDetailGrid.Children[i].SetValue(Grid.ColumnSpanProperty, 2);
                    continue;
                }

                TextBlock label = new TextBlock();
                label.Text = BookLabels[i];
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);
                BookDetailGrid.Children.Add(label);
            }
        }

        private void InitCharacterDetailGrid()
        {
            int rowindex = 0;
            foreach (var childItem in CharacterDetailGrid.Children)
            {
                childItem.SetValue(Grid.RowProperty, rowindex++);
                childItem.SetValue(Grid.ColumnProperty, 1);
                childItem.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
                childItem.SetValue(MarginProperty, new Thickness(10, 0, 0, 0));
            }

            //add labels
            for (int i = 0; i < rowindex; i++)
            {
                if (i == 0)
                {
                    CharacterDetailGrid.Children[i].SetValue(Grid.ColumnProperty, 0);
                    CharacterDetailGrid.Children[i].SetValue(Grid.ColumnSpanProperty, 2);
                    continue;
                }

                TextBlock label = new TextBlock();
                label.Text = CharacterLabels[i];
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);
                CharacterDetailGrid.Children.Add(label);
            }
        }

        private async void Character_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedCharacter = (Character)e.ClickedItem;

            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            DetailNameTextBlock.Text = selectedCharacter.displayName;
            DetailGenderTextBlock.Text = selectedCharacter.gender;
            DetailCultureTextBlock.Text = selectedCharacter.culture;
            DetailBornTextBlock.Text = selectedCharacter.born;
            DetailDiedTextBlock.Text = selectedCharacter.died;
            Character father = await GOTFacade.GetSingleDataAsync<Character>(selectedCharacter.father);
            DetailFatherTextBlock.Text = father == null ? "" : father.name;
            Character mother = await GOTFacade.GetSingleDataAsync<Character>(selectedCharacter.mother);
            DetailMotherTextBlock.Text = mother == null ? "" : mother.name;
            Character spouse = await GOTFacade.GetSingleDataAsync<Character>(selectedCharacter.spouse);
            DetailSpouseTextBlock.Text = spouse == null ? "" : spouse.name;
            DetailTitlesTextBlock.Text = String.Join(", ", selectedCharacter.titles);
            DetailAliasesTextBlock.Text = String.Join(", ", selectedCharacter.aliases);
            DetailTvSeriesTextBlock.Text = String.Join(", ", selectedCharacter.tvSeries);
            DetailPlayedByTextBlock.Text = String.Join(", ", selectedCharacter.playedBy);

            GOTBooks.Clear();
            GOTPovBooks.Clear();
            GOTHouses.Clear();
            
            await GOTFacade.GetDataListAsync(selectedCharacter.books, GOTBooks);
            await GOTFacade.GetDataListAsync(selectedCharacter.povBooks, GOTPovBooks);
            await GOTFacade.GetDataListAsync(selectedCharacter.allegiances, GOTHouses);

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;

        }

        private async void Book_ItemClick(object sender, ItemClickEventArgs e)
        {
            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            var selectedBook = (Book)e.ClickedItem;

            ClearBookDetailsPanel();
            BookDetailNameTextBlock.Text = selectedBook.name;
            BookDetailISBNTextBlock.Text = selectedBook.isbn;
            BookDetailAuthorsTextBlock.Text = String.Join(", ", selectedBook.authors);
            BookDetailNumberOfPagesTextBlock.Text = selectedBook.numberOfPages.ToString();
            BookDetailPublisherTextBlock.Text = selectedBook.publisher;
            BookDetailCountryTextBlock.Text = selectedBook.country;
            BookDetailMediaTypeTextBlock.Text = selectedBook.mediaType;
            BookDetailReleasedTextBlock.Text = selectedBook.released;

            GOTBookCharacters.Clear();
            GOTBookPOVCharacters.Clear();
            await GOTFacade.GetDataListAsync(selectedBook.characters, GOTBookCharacters);
            await GOTFacade.GetDataListAsync(selectedBook.povCharacters, GOTBookPOVCharacters);

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;
        }

        private void ClearBookDetailsPanel()
        {
            BookDetailNameTextBlock.Text = "";
            BookDetailISBNTextBlock.Text = "";
            BookDetailNumberOfPagesTextBlock.Text = "";
            BookDetailPublisherTextBlock.Text = "";
            BookDetailCountryTextBlock.Text = "";
            BookDetailMediaTypeTextBlock.Text = "";
            BookDetailReleasedTextBlock.Text = "";
            BookDetailAuthorsTextBlock.Text = "";
            GOTBookCharacters.Clear();
            GOTBookPOVCharacters.Clear();
        }

        private async void House_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedHouse = (House)e.ClickedItem;

            /*Character spouse = await GOTFacade.GetSingleDataAsync<Character>(selectedCharacter.spouse);
            DetailSpouseTextBlock.Text = spouse == null ? "" : spouse.name;*/

            ClearHousesDetailsPanel();
            HouseDetailNameTextBlock.Text = selectedHouse.name;
            HouseDetailRegionTextBlock.Text = selectedHouse.region;
            HouseDetailCoatOfArmsTextBlock.Text = selectedHouse.coatOfArms;
            HouseDetailWordsTextBlock.Text = selectedHouse.words;
            HouseDetailTitlesTextBlock.Text = String.Join(", ", selectedHouse.titles);
            HouseDetailSeatsTextBlock.Text = String.Join(", ", selectedHouse.seats);
            Character currentLord = await GOTFacade.GetSingleDataAsync<Character>(selectedHouse.currentLord);
            HouseDetailCurrentLordTextBlock.Text = currentLord == null ? "" : currentLord.name;
            Character heir = await GOTFacade.GetSingleDataAsync<Character>(selectedHouse.heir);
            HouseDetailHeirTextBlock.Text = heir == null ? "" : heir.name;
            Character overlord = await GOTFacade.GetSingleDataAsync<Character>(selectedHouse.overlord);
            HouseDetailOverlordTextBlock.Text = overlord == null ? "" : overlord.name;
            HouseDetailFoundedTextBlock.Text = selectedHouse.founded;
            Character founder = await GOTFacade.GetSingleDataAsync<Character>(selectedHouse.founder);
            HouseDetailFounderTextBlock.Text = founder == null? "" : founder.name;
            HouseDetailDiedOutTextBlock.Text = selectedHouse.diedOut;
            HouseDetailWeaponsTextBlock.Text = String.Join(", ", selectedHouse.ancestralWeapons);

            GOTHouseSwornMembers.Clear();
            GOTHouseCadetBranches.Clear();
            await GOTFacade.GetDataListAsync(selectedHouse.swornMembers, GOTHouseSwornMembers);
            await GOTFacade.GetDataListAsync(selectedHouse.cadetBranches, GOTHouseCadetBranches);
        }

        private void ClearHousesDetailsPanel()
        {
            HouseDetailNameTextBlock.Text = "";
            HouseDetailRegionTextBlock.Text = "";
            HouseDetailCoatOfArmsTextBlock.Text = "";
            HouseDetailWordsTextBlock.Text = "";
            HouseDetailCurrentLordTextBlock.Text = "";
            HouseDetailHeirTextBlock.Text = "";
            HouseDetailOverlordTextBlock.Text = "";
            HouseDetailFoundedTextBlock.Text = "";
            HouseDetailFounderTextBlock.Text = "";
            HouseDetailDiedOutTextBlock.Text = "";
            HouseDetailTitlesTextBlock.Text = "";
            HouseDetailSeatsTextBlock.Text = "";
            HouseDetailWeaponsTextBlock.Text = "";
            GOTHouseSwornMembers.Clear();
            GOTHouseCadetBranches.Clear();
        }
    }
}
