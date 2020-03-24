using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    [Table("Notification")]
    // В этой таблице хранятся id пользователя, которого необходимо уведомить, если появится в наличии книга с соответствующим id ( после уведомления запись удаляется )
    public class NotificationEntityModel
    {

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid BookId { get; set; }


        public UsersEntityModel User { get; set; }
        public BooksEntityModel Book { get; set; }

    }
}
