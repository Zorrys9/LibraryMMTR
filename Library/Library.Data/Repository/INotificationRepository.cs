using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    public interface INotificationRepository
    {

        NotificationEntityModel CreateNotification(NotificationEntityModel model);
        List<NotificationEntityModel> GetListNotification(Guid BookId);
        NotificationEntityModel CheckNotification(NotificationEntityModel model);
        NotificationEntityModel DeleteNotification(NotificationEntityModel model);

    }
}
