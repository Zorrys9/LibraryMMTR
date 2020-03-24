using Library.Common.Enums;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Logic.Models
{
    public class StatusLogModel
    {

        [Required]
        public string UserId { get; set; }              // id пользователя, взявшего книгу
        [Required]
        public Guid BookId { get; set; }                // id книги
        [Required]
        public DateTime DateOfReceipt { get; set; }     // дата операции
        [Required]
        public Operations Operation { get; set; }       // Действие (взял, вернул)

        public static implicit operator StatusLogModel(StatusLogsEntityModel model)
        {
            if (model == null)
                return null;
            else return new StatusLogModel
            {
                UserId = model.UserId,
                BookId = model.BookId,
                DateOfReceipt = model.DateOfReceipt,
                Operation = model.Operation
            };
        }
        public static implicit operator StatusLogsEntityModel(StatusLogModel model)
        {
            if (model == null)
                return null;
            else return new StatusLogsEntityModel
            {
                UserId = model.UserId,
                BookId = model.BookId,
                DateOfReceipt = model.DateOfReceipt,
                Operation = model.Operation
            };
        }
    }
}
