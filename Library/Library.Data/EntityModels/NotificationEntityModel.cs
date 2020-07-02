using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    /// <summary>
    /// В этой таблице хранятся id пользователя, которого необходимо уведомить, если появится в наличии книга с соответствующим id ( после уведомления запись удаляется )
    /// </summary>
    [Table("Notification")]
    public class NotificationEntityModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }      // Id пользователя

        [Required]
        public Guid BookId { get; set; }        // Id книги


        public UserEntityModel User { get; set; }
        public BookEntityModel Book { get; set; }

    }
}
