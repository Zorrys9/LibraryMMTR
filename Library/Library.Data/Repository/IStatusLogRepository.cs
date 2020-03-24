using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    public interface IStatusLogRepository
    {

        StatusLogsEntityModel CreateStatusLog(StatusLogsEntityModel model);
        List<StatusLogsEntityModel> GetListStatusLogs();
        List<StatusLogsEntityModel> GetListStatusLogs(Guid bookId);

    }
}
