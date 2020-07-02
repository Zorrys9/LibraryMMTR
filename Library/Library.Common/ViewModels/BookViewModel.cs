using Library.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления книги
    /// </summary>
    public class BookViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }             
        
        public string Author { get; set; }                      

        public int YearOfPublication { get; set; }             

        public string Language { get; set; }                    

        public int Count { get; set; }                         

        public int Aviable { get; set; }                       

        public int PrevCount { get; set; }                      

        public int CountPages { get; set; }                     

        public List<string> Categories { get; set; }           

        public List<int> IdCategories { get; set; }             

        public List<string> KeyWordsName { get; set; }          

        public string Description { get; set; }                 

        public string URL { get; set; }                        

        public IFormFile Cover { get; set; }                   

        public byte[] CoverBytes { get; set; }                  

        public RaitingBooksViewModel Raiting { get; set; }

        public double RaitingUser { get; set; }

    }
}
