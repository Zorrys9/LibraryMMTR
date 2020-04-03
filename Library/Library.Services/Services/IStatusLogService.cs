using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public interface IStatusLogService
    {
        /// <summary>
        /// Создание новой операции с книгой
        /// </summary>
        /// <param name="model"> Модель операции </param>
        /// <returns> Модель операции </returns>
        Task<StatusLogModel> Create(StatusLogModel model);

        /// <summary>
        /// Возвращает список всех операций с данной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список всех операций </returns>
        List<StatusLogModel> GetList(Guid bookId);

    }
}
