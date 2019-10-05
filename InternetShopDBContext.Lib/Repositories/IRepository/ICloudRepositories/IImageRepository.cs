using BaseModelLibrary.Attributes;
using BaseModelLibrary.IRepositories;
using BaseModelLibrary.Models.CloudModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetShopDBContext.Lib.Repositories.IRepository.ICloudRepositories
{
    [CustomRepositoryRegistration]
    public interface IImageRepository: IBaseRepository<ImageModel>
    {
        ImageModel UpdateImage(ImageModel imageModel);
    }
}
