using GameOfThronesApp.Models;
using GameOfThronesApp.ViewModels;
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
using Windows.UI;
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
        
        public MainPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            ViewModel.InitCharacters();

            InitBookDetailGrid();
            InitCharacterDetailGrid();
            InitHouseDetailGrid();

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Initalization of the House Detail grid (necessary because of the labels)
        /// </summary>
        private void InitHouseDetailGrid()
        {
            int rowindex = 0;
            foreach (var childItem in HouseDetailGrid.Children)
            {
                childItem.SetValue(Grid.RowProperty, rowindex++);
                childItem.SetValue(Grid.ColumnProperty, 1);
                childItem.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
                childItem.SetValue(MarginProperty, new Thickness(10, 10, 10, 10));
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
                label.Text = MainPageViewModel.HouseLabels[i];
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);
                label.SetValue(MarginProperty, new Thickness(10, 10, 10, 10));
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
                childItem.SetValue(MarginProperty, new Thickness(10, 10, 10, 10));
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
                label.Text = MainPageViewModel.BookLabels[i];
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);
                label.SetValue(MarginProperty, new Thickness(10, 10, 10, 10));
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
                childItem.SetValue(MarginProperty, new Thickness(10, 10, 10, 10));
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
                label.Text = MainPageViewModel.CharacterLabels[i];
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);
                label.SetValue(MarginProperty, new Thickness(10, 10, 10, 10));
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
            DetailFatherTextBlock.Text = await ViewModel.UpdateSigleCharacterDetail(selectedCharacter.father);
            DetailMotherTextBlock.Text = await ViewModel.UpdateSigleCharacterDetail(selectedCharacter.mother);
            DetailSpouseTextBlock.Text = await ViewModel.UpdateSigleCharacterDetail(selectedCharacter.spouse);
            DetailTitlesTextBlock.Text = String.Join(", ", selectedCharacter.titles);
            DetailAliasesTextBlock.Text = String.Join(", ", selectedCharacter.aliases);
            DetailTvSeriesTextBlock.Text = String.Join(", ", selectedCharacter.tvSeries);
            DetailPlayedByTextBlock.Text = String.Join(", ", selectedCharacter.playedBy);

            ViewModel.UpdateCharacterDetails(selectedCharacter);

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;

        }

        private void Book_ItemClick(object sender, ItemClickEventArgs e)
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

            ViewModel.UpdateBookDetails(selectedBook);

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
            ViewModel.ClearBookData();
        }

        private async void House_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedHouse = (House)e.ClickedItem;

            ClearHousesDetailsPanel();
            HouseDetailNameTextBlock.Text = selectedHouse.name;
            HouseDetailRegionTextBlock.Text = selectedHouse.region;
            HouseDetailCoatOfArmsTextBlock.Text = selectedHouse.coatOfArms;
            HouseDetailWordsTextBlock.Text = selectedHouse.words;
            HouseDetailTitlesTextBlock.Text = String.Join(", ", selectedHouse.titles);
            HouseDetailSeatsTextBlock.Text = String.Join(", ", selectedHouse.seats);
            HouseDetailCurrentLordTextBlock.Text = await ViewModel.UpdateSigleCharacterDetail(selectedHouse.currentLord);
            HouseDetailHeirTextBlock.Text = await ViewModel.UpdateSigleCharacterDetail(selectedHouse.heir);
            HouseDetailOverlordTextBlock.Text = await ViewModel.UpdateSigleCharacterDetail(selectedHouse.overlord);
            HouseDetailFoundedTextBlock.Text = selectedHouse.founded;
            HouseDetailFounderTextBlock.Text = await ViewModel.UpdateSigleCharacterDetail(selectedHouse.founder);
            HouseDetailDiedOutTextBlock.Text = selectedHouse.diedOut;
            HouseDetailWeaponsTextBlock.Text = String.Join(", ", selectedHouse.ancestralWeapons);

            ViewModel.UpdateHouseDetails(selectedHouse);
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
            ViewModel.ClearHouseData();
        }
    }
}
