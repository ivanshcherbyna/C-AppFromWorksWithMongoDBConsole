using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Configuration;
using MongoDB.Bson.Serialization;
//using HW_MongoDB_ConsoleApp.

namespace HW_MongoDB_ConsoleApp
{
    class Program
    {
        static  void Main(string[] args)
        {


            try
            {
                string con = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
                MongoClient client = new MongoClient(con);
                client.GetDatabase("films");
                Console.WriteLine("Загрузка базы данных 'films'");
                Console.WriteLine("--------------------");
                FindCollection(client).GetAwaiter().GetResult();
                Console.WriteLine("--------------------");

         //     AddFilm(client, "The Headhunter's Calling", "Drama", "Jerard Butler", 2016).GetAwaiter().GetResult();
         //     AddFilm(client, "Security", "Triller", "Antonio Banderas", 2017).GetAwaiter().GetResult();
         //     AddFilm(client, "Kong: Skull Island", "Fantasy", "Samuel L'Jackson", 2017).GetAwaiter().GetResult();
                AddFilm(client, "The Wizard of Lies", "Biography", "Robert Deniro", 2017).GetAwaiter().GetResult();
                // Console.WriteLine("нажмите любую кноку для продолжения");
                //Console.ReadLine();
                Console.WriteLine("--------------------");
                //DelFilm(client, "The Headhunter's Calling").GetAwaiter().GetResult();
                DelFilm(client, "Security").GetAwaiter().GetResult();;
                DelFilm(client, "The Wizard of Lies").GetAwaiter().GetResult();;
                Console.WriteLine("--------------------");

                FindCollection(client).GetAwaiter().GetResult();


            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private static async Task FindCollection(MongoClient client)
        {
            var database = client.GetDatabase("films");
            var collection = database.GetCollection<BsonDocument>("film");
            var filter = new BsonDocument();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var film = cursor.Current;
                    foreach (var doc in film)
                    {
                        Console.WriteLine(doc);

                    }
                }
            }
        }
        private static async Task AddFilm(MongoClient client, string name_,  string genre_, string actors_, int year_)
        {
            Console.WriteLine("Добавление фильма в базу...");
            var database = client.GetDatabase("films");
            var collection = database.GetCollection<Film>("film");
            Film film1 = new Film
            {
                name = name_,
                Actors = actors_,
                genre = genre_,
                year = year_

            };
            await collection.InsertOneAsync(film1);
            Console.WriteLine("Фильм '" + film1.name + "' добавлен в базу!");

           
        }
        private static async Task DelFilm(MongoClient client, string name_)
        {
            Console.WriteLine("Удаление фильма..");
            
            var database = client.GetDatabase("films");
            var collection = database.GetCollection<Film>("film");
            var filter = new BsonDocument();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var film = cursor.Current;
                    foreach (var doc in film)
                    {

                        if (name_.ToString() == doc.name.ToString())
                        {
                            collection.DeleteOneAsync(doc.ToBsonDocument());
                            Console.WriteLine("Фильм '" + doc.name + "' удален из базы!");
                        }
                        else
                        {
                             Console.WriteLine("Фильм '" + name_ + "' не найден в базе!");
                            break;
                        }
                    }
                }
            }
            
        }
        private static async Task GetCollectionsNames(MongoClient client)
        {

            using (var cursor = await client.ListDatabasesAsync())
            {
                var dbs = await cursor.ToListAsync();
                foreach (var db in dbs)
                {
                    Console.WriteLine("В базе данных {0} имеются следующие коллекции:", db["name", "films"]);

                    /*     IMongoCollection<Film> film_ =new  client.GetDatabase(db["name"].ToString());

                          using (var collCursor = await film_.ListCollectionsAsync())
                          {
                              var colls = await collCursor.ToListAsync();
                              //foreach (var col in colls)
                              //{
                                  Console.WriteLine(col["name"]);
                             // }
                          }
                      }
                  }
              }*/

                }
            }
        } //можно удалить - не нужный метод
    }
}

    

