﻿using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entites;

namespace Talabat.APIS.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDTO>()
				.ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
				.ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name));
		}
	}
}