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
        public MainPage()
        {
            this.InitializeComponent();

            GOTCharacters = new ObservableCollection<Character>();
            GOTBooks = new ObservableCollection<Book>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            while (GOTCharacters.Count < 10)
            {
                await GOTFacade.AddCharactersToAppAsync(GOTCharacters);
            }
            

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;
        }

        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            ClearBookDetailsPanel();

            var selectedCharacter = (Character)e.ClickedItem;

            DetailNameTextBlock.Text = selectedCharacter.displayName;
            DetailGenderTextBlock.Text = "Gender: " + selectedCharacter.gender;
            DetailCultureTextBlock.Text = "Culture: " + selectedCharacter.culture;
            DetailBornTextBlock.Text = "Born: " + selectedCharacter.born;
            DetailDiedTextBlock.Text = "Died: " + selectedCharacter.died;
            DetailFatherTextBlock.Text = "Father: " + selectedCharacter.father;
            DetailMotherTextBlock.Text = "Mother: " + selectedCharacter.mother;
            DetailSpouseTextBlock.Text = "Spouse: " + selectedCharacter.spouse;

            GOTBooks.Clear();

            await GOTFacade.GetBookListAsync(selectedCharacter.books, GOTBooks);

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
