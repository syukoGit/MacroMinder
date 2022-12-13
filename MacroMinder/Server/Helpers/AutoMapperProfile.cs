namespace MacroMinder.Server.Helpers;

using AutoMapper;
using JetBrains.Annotations;
using MacroMinder.Server.Entities;
using MacroMinder.Shared.Food;
using MacroMinder.Shared.MacroDailyReport;
using MacroMinder.Shared.MacroNutriment;
using MacroMinder.Shared.User;

public class AutoMapperProfile : Profile
{
    [UsedImplicitly]
    public AutoMapperProfile()
    {
        CreateMap<Food, FoodDTO>();
        CreateMap<CreateFoodDTO, Food>();

        CreateMap<User, UserDTO>();
        CreateMap<UserPatchUpdateDTO, User>();

        CreateMap<MacroNutriment, MacroNutrimentDTO>();
        CreateMap<CreateMacroNutrimentDTO, MacroNutriment>();

        CreateMap<MacroDailyReport, MacroDailyReportDTO>();
        CreateMap<CreateMacroDailyReportDTO, MacroDailyReportDTO>();
    }
}