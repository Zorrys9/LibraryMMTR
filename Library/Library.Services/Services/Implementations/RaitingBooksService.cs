using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Services.Implementations
{
    public class RaitingBooksService:IRaitingBooksService
    {

        private readonly IRaitingBooksRepository _raitingBooksRepository;

        public RaitingBooksService(IRaitingBooksRepository raitingBooksRepository)
        {
            _raitingBooksRepository = raitingBooksRepository;
        }




        public RaitingBooksModel Create(RaitingBooksViewModel model, string userId)
        {

            if(model != null && userId != null)
            {

                RaitingBooksModel createModel = model;
                createModel.UserId = userId;

                var result = _raitingBooksRepository.Create(createModel);

                return result;

            }
            else
            {

                throw new Exception("Данные заполнены не полностью");

            }

        }

        public RaitingBooksModel Update(RaitingBooksViewModel model, string userId)
        {
            if(model != null && userId != null)
            {

                RaitingBooksModel updateModel = model;
                updateModel.UserId = userId;

                var result = _raitingBooksRepository.UpdateRaiting(updateModel);

                return result;

            }
            else
            {

                throw new Exception("Данный заполенны не полностью");

            }
        }

        public List<RaitingBooksModel> GetRaiting(Guid bookId)
        {
            if(bookId != null)
            {

                List<RaitingBooksModel> result = new List<RaitingBooksModel>();
                var listRaitings = _raitingBooksRepository.GetRaiting(bookId);

                foreach(var raiting in listRaitings)
                {

                    result.Add(raiting);

                }

                return result;

            }
            else
            {

                throw new Exception("Id книги не указан");

            }
        }
    }
}
