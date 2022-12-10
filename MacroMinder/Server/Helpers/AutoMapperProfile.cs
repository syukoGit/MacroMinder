namespace MacroMinder.Server.Helpers;

using AutoMapper;
using JetBrains.Annotations;
using MacroMinder.Server.Entities;
using MacroMinder.Shared.Food;

public class AutoMapperProfile : Profile
{
    [UsedImplicitly]
    public AutoMapperProfile()
    {
        CreateMap<Food, FoodDTO>();
        CreateMap<CreateFoodDTO, Food>();
    }
}