﻿using NewsPortal.Framework.Dtos.Request;
using NewsPortal.Framework.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Framework.Interfaces
{
    public interface INewsPortalService
    {
        Task<IEnumerable<NewsResponse>> GetCameras();
        Task InsertNews(NewsRequest request);
        Task<IEnumerable<NewsResponse>> SearchDetections(NewsRequest request);
    }
}
