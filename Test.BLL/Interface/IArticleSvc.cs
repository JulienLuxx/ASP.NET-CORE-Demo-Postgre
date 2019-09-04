﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Dto;
using Test.Core.IOC;
using Test.Service.Dto;
using Test.Service.QueryModel;

namespace Test.Service.Interface
{
    public interface IArticleSvc : IDependency 
    {
        ResultDto AddSingle(ArticleDto dto);

        Task<ResultDto> AddSingleAsync(ArticleDto dto);

        ResultDto Edit(ArticleDto dto);

        Task<ResultDto<ArticleDto>> GetPageDataAsync(ArticleQueryModel qModel);

        Task<ResultDto<ArticleDetailDto>> GetSingleDataAsync(int Id);
    }
}
