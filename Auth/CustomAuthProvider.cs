using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Funeraria.Web.Auth // Asegúrate que coincida con tu carpeta
{
    public class CustomAuthProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        // Esta función se ejecuta cada vez que la página carga
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // 1. Buscamos el token en la memoria del navegador
            string token = await _localStorage.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // 2. Si hay token, lo leemos y extraemos los datos (Claims)
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

                    // 3. ¡IMPORTANTE! Se lo ponemos al HttpClient para que la API nos deje pasar
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                }
                catch
                {
                    // Si el token estaba corrupto, lo borramos
                    await _localStorage.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            // Avisamos a Blazor del estado actual
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        // --- FUNCIONES MÁGICAS PARA LEER EL TOKEN (No tocar) ---
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}