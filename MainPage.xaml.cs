using GameOfThronesApp.Models;
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
        private ObservableCollection<Character> GOTCharacters { get; set; }
        public ObservableCollection<Book> GOTBooks { get; set; }
        public ObservableCollection<Book> GOTPovBooks { get; set; }

        private List<string> CharacterLabelStrings { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            GOTCharacters = new ObservableCollection<Character>();
            GOTBooks = new ObservableCollection<Book>();
            GOTPovBooks = new ObservableCollection<Book>();

            CharacterLabelStrings = new List<string>()
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

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;
        }

        private void InitBookDetailGrid()
        {
            int rowindex = 0;
            foreach (var childItem in BookDetailGrid.Children)
            {
                childItem.SetValue(Grid.RowProperty, rowindex++);
                childItem.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
                childItem.SetValue(MarginProperty, new Thickness(10, 0, 0, 0));
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
                if(i == 0)
                {
                    CharacterDetailGrid.Children[i].SetValue(Grid.ColumnProperty, 0);
                    CharacterDetailGrid.Children[i].SetValue(Grid.ColumnSpanProperty, 2);
                    continue;
                }

                TextBlock label = new TextBlock();
                label.Text = CharacterLabelStrings[i];
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);
                CharacterDetailGrid.Children.Add(label);
            }
        }

        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            ClearBookDetailsPanel();

            var selectedCharacter = (Character)e.ClickedItem;

            DetailNameTextBlock.Text = selectedCharacter.displayName;
            DetailGenderTextBlock.Text = selectedCharacter.gender;
            DetailCultureTextBlock.Text = selectedCharacter.culture;
            DetailBornTextBlock.Text = selectedCharacter.born;
            DetailDiedTextBlock.Text = selectedCharacter.died;
            DetailFatherTextBlock.Text = selectedCharacter.father;
            DetailMotherTextBlock.Text = selectedCharacter.mother;
            DetailSpouseTextBlock.Text = selectedCharacter.spouse;
            DetailTitlesTextBlock.Text = String.Join(", ", selectedCharacter.titles);
            DetailAliasesTextBlock.Text = String.Join(", ", selectedCharacter.aliases);
            DetailAllegiancesTextBlock.Text = String.Join(", ", selectedCharacter.allegiances);
            DetailTvSeriesTextBlock.Text = String.Join(", ", selectedCharacter.tvSeries);
            DetailPlayedByTextBlock.Text = String.Join(", ", selectedCharacter.playedBy);

            GOTBooks.Clear();
            GOTPovBooks.Clear();
            await GOTFacade.GetBookListAsync(selectedCharacter.books, GOTBooks);
            await GOTFacade.GetBookListAsync(selectedCharacter.povBooks, GOTPovBooks);

            Debug.WriteLine(GOTPovBooks.ToString());


            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedBook = (Book)e.ClickedItem;

            BookDetailNameTextBlock.Text = selectedBook.name;
            BookDetailISBNTextBlock.Text = "ISBN: " + selectedBook.isbn;
            BookDetailNumberOfPagesTextBlock.Text = "Number of pages: " + selectedBook.numberOfPages.ToString();
            BookDetailPublisherTextBlock.Text = "Publisher: " + selectedBook.publisher;
            BookDetailCountryTextBlock.Text = "Country: " + selectedBook.country;
            BookDetailMediaTypeTextBlock.Text = "MediaType: " + selectedBook.mediaType;
            BookDetailReleasedTextBlock.Text = "Released: " + selectedBook.released;

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
        }
    }
}
