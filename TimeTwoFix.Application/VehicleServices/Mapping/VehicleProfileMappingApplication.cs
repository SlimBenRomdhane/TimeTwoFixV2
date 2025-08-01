﻿using AutoMapper;
using TimeTwoFix.Application.VehicleServices.Dtos;
using TimeTwoFix.Core.Entities.VehicleManagement;

namespace TimeTwoFix.Application.VehicleServices.Mapping
{
    public class VehicleProfileMappingApplication : Profile
    {
        public VehicleProfileMappingApplication()
        {
            CreateMap<Vehicle, CreateVehicleDto>().ReverseMap();
            CreateMap<Vehicle, ReadVehicleDto>()
                .ForMember(dest => dest.ReadClientDto, opt => opt.MapFrom(src => src.Client))
                .ReverseMap();
            CreateMap<Vehicle, UpdateVehicleDto>().ReverseMap();
            CreateMap<Vehicle, DeleteVehicleDto>().ReverseMap();
        }
    }
}