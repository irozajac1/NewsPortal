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

        public async Task<IEnumerable<NewsResponse>> GetCameras()
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
                //var camera = await _saUow.GetRepository<Camera>().FirstOrDefault(x => x.SerialNumber.ToLower() == request.SerialNumber.ToLower());

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

        public Task<IEnumerable<NewsResponse>> SearchDetections(NewsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
