using NewsPortal.Framework.Data.Entities;
using NewsPortal.Framework.Dtos.Request;
using NewsPortal.Framework.Dtos.Response;
using NewsPortal.Framework.Interfaces;
using NewsPortal.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Framework.Services
{
    public class NewsPortalService : INewsPortalService
    {
        private readonly INewsPortalUnitOfWork _saUow;

        public NewsPortalService(INewsPortalUnitOfWork saUow)
        {
            _saUow = saUow;
        }

        public async Task<IEnumerable<NewsResponse>> GetNews()
        {
            List<NewsResponse> result = new List<NewsResponse>();
            try
            {
                var news = await _saUow.GetRepository<News>().GetAll();
                foreach(var item in news)
                {
                    var newsresponse = new NewsResponse
                    {
                        Id = item.Id,
                        Context = item.Context,
                        Title = item.Title,
                        PublishTime = item.PublishTime
                    };

                    result.Add(newsresponse);
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        

        public async Task InsertNews(NewsRequest request)
        {
            try
            {
                var new_response = new News
                {
                    Id = Guid.NewGuid(),
                    Context = request.Context,
                    Title = request.Title,
                    PublishTime = DateTime.Now
                };
                _saUow.GetRepository<News>().Insert(new_response);
                await _saUow.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<NewsResponse>> SearchNews(string title)
        {
            List<NewsResponse> response = new List<NewsResponse>();

            var news = await _saUow.GetRepository<News>().Where(x=>x.Title == title);

            foreach(var item in news)
            {
                var news_response = new NewsResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                    Context = item.Context,
                    PublishTime = item.PublishTime
                };
                response.Add(news_response);
            }
            return response;
        }

        public async Task Update(News request)
        {
            var news = await _saUow.GetRepository<News>().GetById(request.Id);

            if (news == null)
                throw new ArgumentNullException("News not found");

            if (!string.IsNullOrEmpty(request.Title)) news.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Context)) news.Context = request.Context;
            if (!string.IsNullOrEmpty(request.PublishTime.ToString())) news.PublishTime = request.PublishTime;

            _saUow.GetRepository<News>().Update(news);
            await _saUow.SaveChangesAsync();
        }


    }
}

