using AutoMapper;
using Microsoft.Extensions.Logging;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Applications.Entities.BankAccountTypes.Commands;
using q_wallet.Applications.Entities.UserAccounts.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;

namespace q_wallet.Extensions
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			#region General

			//BankAccounts
			CreateMap<BankAccount, BankAccountResponse>().ReverseMap();
			CreateMap<BankAccount, CreateBankAccountCommand>().ReverseMap();
			CreateMap<BankAccount, CreditBankAccountCommand>().ReverseMap();
			CreateMap<BankAccount, DebitBankAccountCommand>().ReverseMap();
			CreateMap<BankAccountResponse, CreateBankAccountCommand>().ReverseMap();
			CreateMap<BankAccount, UpdateBankAccountCommand>().ReverseMap();
			CreateMap<BankAccount, DeleteBankAccountCommand>().ReverseMap();

			//BankAccountTypes
			CreateMap<BankAccountType, BankAccountTypeResponse>().ReverseMap();
			CreateMap<BankAccountType, CreateBankAccountTypeCommand>().ReverseMap();
			CreateMap<BankAccountTypeResponse, CreateBankAccountTypeCommand>().ReverseMap();
			CreateMap<BankAccountType, UpdateBankAccountTypeCommand>().ReverseMap();
			CreateMap<BankAccountType, DeleteBankAccountTypeCommand>().ReverseMap();

			//UserAccounts
			CreateMap<UserAccount, UserAccountResponse>().ReverseMap();
			CreateMap<UserAccount, CreateUserAccountCommand>().ReverseMap();
			CreateMap<UserAccountResponse, CreateUserAccountCommand>().ReverseMap();
			CreateMap<UserAccount, UpdateUserAccountCommand>().ReverseMap();
			CreateMap<UserAccount, DeleteUserAccountCommand>().ReverseMap();

			#endregion
		}
	}
}
