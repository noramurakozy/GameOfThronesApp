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

        /// <summary>
        /// API call, get the whole character list async
        /// </summary>
        /// <returns>List of character objects</returns>
        public async static Task<ObservableCollection<Character>> GetAllCharacterListAsync()
        {
            ObservableCollection<Character> characterList = new ObservableCollection<Character>();

            var http = new HttpClient();
            List<Task<HttpResponseMessage>> requestList = new List<Task<HttpResponseMessage>>();

            var url = "https://anapioficeandfire.com/api/characters";
            requestList.Add(http.GetAsync(url));

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

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return characterList;
        }
        /// <summary>
        /// Get random characterlist by number of characters
        /// </summary>
        /// <param name="numOfElements"></param>
        /// <returns>List of characters</returns>
        public async static Task<List<Character>> GetRandomCharacterListAsync(int numOfElements)
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

        /// <summary>
        /// Get templated data list by url list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urls"></param>
        /// <param name="finalList"></param>
        /// <returns>List of the objects</returns>
        public async static Task<List<T>> GetDataListAsync<T>(List<string> urls, ObservableCollection<T> finalList)
        {
            List<T> list = new List<T>();

            var http = new HttpClient();
            List<Task<HttpResponseMessage>> requestList = new List<Task<HttpResponseMessage>>();
            foreach (var url in urls)
            {
                requestList.Add(http.GetAsync(url));
            }

            try
            {
                var results = await Task.WhenAll(requestList.ToArray());

                foreach (var response in results)
                {
                    var jsonMessage = await response.Content.ReadAsStringAsync();

                    var serializer = new DataContractJsonSerializer(typeof(T));
                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

                    var item = serializer.ReadObject(ms);
                    list.Add((T)item);
                    finalList.Add((T)item);
                    //Debug.WriteLine(((Book)book).isbn);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return list;
        }
        /// <summary>
        /// Get single data async by a single url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns>The object</returns>
        public async static Task<T> GetSingleDataAsync<T>(string url)
        {
            if (!url.Equals(""))
            {
                var http = new HttpClient();

                var jsonMessage = await http.GetAsync(url).Result.Content.ReadAsStringAsync();

                var serializer = new DataContractJsonSerializer(typeof(T));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

                return (T)serializer.ReadObject(ms);
            }
            else
            {
                return default(T);
            }
            
        }
        
        /// <summary>
        /// Adding the given number of random characters to the application
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="numOfCharacters"></param>
        /// <returns></returns>
        public async static Task AddCharactersToAppAsync(ObservableCollection<Character> characters, int numOfCharacters)
        {
            var characterList = await GetRandomCharacterListAsync(numOfCharacters);

            //put the characters in the final list
            foreach (var c in characterList)
            {
                characters.Add(c);
            }

            //remove duplication
            characters = new ObservableCollection<Character>(characters.Distinct());
        }
    }
}
