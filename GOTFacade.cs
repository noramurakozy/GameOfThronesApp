using GameOfThronesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesApp
{
    class GOTFacade
    {
        private static int MaxCharacters = 2138;
        public async static Task<List<Character>> GetCharacterListAsync(int numOfElements)
        {
            List<Character> characterList = new List<Character>();
            Random random = new Random();

            var http = new HttpClient();
            List<Task<HttpResponseMessage>> requestList = new List<Task<HttpResponseMessage>>();
            for(int i = 0; i < numOfElements; i++)
            {
                var characterNumber = random.Next(MaxCharacters);

                var url = $"https://anapioficeandfire.com/api/characters/{characterNumber}";
                requestList.Add(http.GetAsync(url));
            }

            try
            {
               var results = await Task.WhenAll(requestList.ToArray());

                foreach (var response in results)
                {
                    var jsonMessage = await response.Content.ReadAsStringAsync();

                    var serializer = new DataContractJsonSerializer(typeof(Character));
                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

                    characterList.Add((Character)serializer.ReadObject(ms));
                }

            } catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return characterList;
        }

        public async static Task<List<Book>> GetBookListAsync(List<string> bookUrls, ObservableCollection<Book> finalBookList)
        {
            List<Book> bookList = new List<Book>();

            var http = new HttpClient();
            List<Task<HttpResponseMessage>> requestList = new List<Task<HttpResponseMessage>>();
            foreach (var url in bookUrls)
            {
                requestList.Add(http.GetAsync(url));
            }

            try
            {
                var results = await Task.WhenAll(requestList.ToArray());

                foreach (var response in results)
                {
                    var jsonMessage = await response.Content.ReadAsStringAsync();

                    var serializer = new DataContractJsonSerializer(typeof(Book));
                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

                    var book = serializer.ReadObject(ms);
                    bookList.Add(book as Book);
                    finalBookList.Add(book as Book);
                    //Debug.WriteLine(((Book)book).isbn);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return bookList;
        }

        public async static Task AddCharactersToAppAsync(ObservableCollection<Character> characters)
        {
            var characterList = await GetCharacterListAsync(10);

            //put the characters in the final list
            foreach (var c in characterList)
            {
                characters.Add(c);
            }
            
        }
    }
}
