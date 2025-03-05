using Blazored.SessionStorage;
using DagaCommon.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DagaDatatable.Services
{
    public class AuthService : AuthenticationStateProvider
    {
        private readonly DBService _dbService;
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal? _currentUser = null;

        public AuthService(DBService dbService, ISessionStorageService sessionStorageService)
        {
            _dbService = dbService;
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (null == _currentUser)
            {
                var account = await _sessionStorageService.GetItemAsync<AccountInfo>(ClaimTypes.Sid);
                if (account != null)
                {
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.Name, account.Name),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.Sid, account.Id.ToString()),
                    ], "apiauth_type"));
                }
                else
                {
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
                }
            }

            return new AuthenticationState(_currentUser);
        }

        public async Task<AccountInfo?> MarkUserAsAuthenticated(SigninInfo info)
        {
            var account = await _dbService.SigninAsync(info);
            if (null == account)
            {
                return null;
            }

            _currentUser = new ClaimsPrincipal(new ClaimsIdentity([
                new Claim(ClaimTypes.Name, account.Name),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Sid, account.Id.ToString()),
            ], "apiauth_type"));
            await _sessionStorageService.SetItemAsync(ClaimTypes.Sid, account);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return account;
        }

        public async Task MarkUseAsLoggedOut()
        {
            _currentUser = null;
            await _sessionStorageService.RemoveItemAsync(ClaimTypes.Sid);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
