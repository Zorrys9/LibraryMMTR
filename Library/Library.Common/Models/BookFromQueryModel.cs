using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель книги для получения информации из json-файла, полученного из OpenLibrary Api
    /// </summary>
    public class BookFromQueryModel
    {
        [JsonProperty("ISBN")]
        public BookDetails AllInfo { get; set; }

    }

    public class BookDetails
    {
        [JsonProperty("details")]
        public InfoBook Details { get; set; }
    }

    public class InfoBook
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("authors")]
        public IEnumerable<Authors> Authors { get; set; }

        [JsonProperty("publish_date")]
        public string Date { get; set; }
    }

    public class Authors
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
