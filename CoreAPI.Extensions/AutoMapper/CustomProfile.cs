using CoreAPI.DTO;
using CoreAPI.Model;
using AutoMapper;
using CoreAPI.Model.Models;

namespace CoreAPI.Extensions.AutoMapper
{
    /// <summary>
    ///  配置构造函数，用来创建关系映射
    /// </summary>
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            //CreateMap<UserInfo, UserInfoDTO>();
            CreateMap<UserInfoDTO, ApplySignIn>();
        }
    }
}
