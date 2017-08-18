using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HW_MongoDB_ConsoleApp
{
    class Film
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string name { set; get; }
        public string Actors { set; get; }
        public string genre { set; get; }
        public int year { set; get; }

        internal object SetElementName(string v)
        {
            throw new NotImplementedException();
        }
    }
}
