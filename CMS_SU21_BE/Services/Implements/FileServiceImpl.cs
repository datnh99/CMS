using CMS_SU21_BE.Models.Entities;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public class FileServiceImpl : FileService
    {
        private FileRepository fileRepository = new FileRepository();
        public int Add(FileRequest request)
        {
            return fileRepository.Add(request);
        }

        public bool deleteByIds(List<int> id)
        {
            return fileRepository.deleteByIds(id);
        }

        public List<FileResponse> getByArticleId(int? articleId)
        {
            return fileRepository.getByArticleId(articleId);
        }

        public FileResponse getById(int id)
        {
            return fileRepository.getById(id);
        }

        public List<FileResponse> getByListIds(List<int> ids)
        {
            return fileRepository.getByListIds(ids);
        }
    }
}