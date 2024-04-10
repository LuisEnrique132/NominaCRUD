using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace NominaCRUD.Models
{
    public class ServiciosdeCotabilidad
    {
        private readonly HttpClient _httpClient;

        public ServiciosdeCotabilidad(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://ap1-contabilidad.azurewebsites.net/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<AsientoContable> GetAsientoContable(int id)
        {
            var reponse = await _httpClient.GetAsync($"/Contabilidad/ObtenerAsientoContable?Id={id}");
            reponse.EnsureSuccessStatusCode();
            var jsoR = await reponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AsientoContable>(jsoR);

        }
        public async Task<HttpResponseMessage> CrearAsientoContableAsync(AsientoContable asientoContable)
        {


            var dto = new AsientoContable
            {
                Descripcion = asientoContable.Descripcion,
                Auxiliar = asientoContable.Auxiliar,
                Fecha = asientoContable.Fecha,
                Monto = asientoContable.Monto,
                estado = asientoContable.estado,
                moneda = asientoContable.moneda,
                Transaccions = asientoContable.Transaccions.Select(t => new Transaccion
                {
                    Cuenta = t.Cuenta,
                    TipoMovimiento = t.TipoMovimiento,
                    Monto = t.Monto
                }).ToList()
            };
            var json = JsonConvert.SerializeObject(dto);
            var contentent = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("/Contabilidad/AsientoContable", contentent);

        }
        public async Task<List<Estado>> GetEstadosAsync()
        {
            var reponse = await _httpClient.GetAsync("/Contabilidad/EstadosContables");
            reponse.EnsureSuccessStatusCode();
            var jsonR =await reponse.Content.ReadAsStringAsync();
            var estadosR = JsonConvert.DeserializeObject<JObject>(jsonR);
            var estadosContables = estadosR?["estadosContables"].ToObject<List<Estado>>();

            return estadosContables;

        }
        public async Task<List<Moneda>> GestMonedasAsync()
        {
            var reponse = await _httpClient.GetAsync("/Contabilidad/Monedas");
            reponse.EnsureSuccessStatusCode();
            var jsonR = await reponse.Content.ReadAsStringAsync();
            var MonedasR = JsonConvert.DeserializeObject<JObject>(jsonR);
            var Monedas = MonedasR?["monedas"].ToObject<List<Moneda>>();

            return Monedas;

        }
        public async Task<List<TipoMovimiento>> GetTipoMovimientoAsync()
        {
            var reponse = await _httpClient.GetAsync("/Contabilidad/OrigenCuenta");
            reponse.EnsureSuccessStatusCode();
            var jsonR = await reponse.Content.ReadAsStringAsync();
            var TimovimientoR = JsonConvert.DeserializeObject<JObject>(jsonR);
            var movimientos = TimovimientoR?["monedas"].ToObject<List<TipoMovimiento>>();

            return movimientos;

        }

    }
}
