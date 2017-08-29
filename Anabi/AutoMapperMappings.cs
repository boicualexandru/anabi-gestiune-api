﻿using Anabi.DataAccess.Ef.DbModels;
using Anabi.Domain.Category;
using Anabi.Domain.Category.Commands;
using Anabi.Domain.Models;
using Anabi.Features.Category;
using Anabi.Features.Category.Models;
using Anabi.Features.Institution;
using AutoMapper;

namespace Anabi
{
    public class AutoMapperMappings : Profile
    {
        public AutoMapperMappings()
        {
            CreateMap<Address, AddressDb>().ReverseMap();
            CreateMap<Asset, AssetDb>().ReverseMap();

            CreateMap<Category, CategoryDb>().ReverseMap();
            CreateMap<AddCategory, CategoryDb>();
            CreateMap<EditCategory, CategoryDb>();

            CreateMap<County, CountyDb>().ReverseMap();
            CreateMap<Decision, DecisionDb>().ReverseMap();
            CreateMap<File, FileDb>().ReverseMap();
            CreateMap<FileNumber, FileNumberDb>().ReverseMap();
            CreateMap<HistoricalStage, HistoricalStageDb>().ReverseMap();
            CreateMap<Institution, InstitutionDb>().ReverseMap();
            CreateMap<Person, PersonDb>().ReverseMap();
            CreateMap<RecoveryBeneficiary, RecoveryBeneficiaryDb>().ReverseMap();
            CreateMap<Stage, StageDb>().ReverseMap();
            CreateMap<StorageSpace, StorageSpaceDb>().ReverseMap();
            CreateMap<User, UserDb>().ReverseMap();
        }
    }
}
