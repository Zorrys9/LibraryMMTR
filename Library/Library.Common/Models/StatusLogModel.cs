using Library.Common.Enums;
using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель действий над книгами
    /// </summary>
    public class StatusLogModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }   
        
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid BookId { get; set; }      
        
        /// <summary>
        /// Дата операции 
        /// </summary>
        public DateTime Date { get; set; }         
        
        /// <summary>
        /// Вид операции
        /// </summary>
        public Operations Operation { get; set; }      

    }
}
