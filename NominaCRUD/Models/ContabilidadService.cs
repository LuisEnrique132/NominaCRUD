namespace NominaCRUD.Models
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class AsientoContableDTO
    {
        public string descripcion { get; set; }
        public int auxiliar { get; set; }
        public DateTime fecha { get; set; }
        public decimal monto { get; set; }
        public int estado { get; set; } // Cambiado a minúsculas
        public int moneda { get; set; } // Cambiado a minúsculas
        public List<TransaccionDTO> transacciones { get; set; } // Cambiado a minúsculas
    }

    public class TransaccionDTO
    {
        public int cuenta { get; set; } // Cambiado a minúsculas
        public int tipoMovimiento { get; set; } // Cambiado a minúsculas
        public decimal monto { get; set; }
    }



    public class EntradaContableDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Auxiliar { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int Estado { get; set; }
        public int Moneda { get; set; }
        public string EstadoNombre { get; set; }
        public string MonedaNombre { get; set; }
        public List<RegistroDTO> Registros { get; set; }
    }

    public class RegistroDTO
    {
        public int Cuenta { get; set; }
        public int TipoMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string CuentaNombre { get; set; }
        public string TipoMovimientoNombre { get; set; }
    }




    public class ContabilidadService
    {
        private readonly HttpClient _httpClient;

        public ContabilidadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://ap1-contabilidad.azurewebsites.net/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /*   public async Task<List<EntradaContableDTO>> ObtenerEntradasContablesxAuxAsync(int auxiliarId)
    {
        var response = await _httpClient.GetAsync($"/Contabilidad/ObtenerEntradaContablexAux?id={auxiliarId}");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Debes deserializar el JSON en un objeto anónimo para extraer la lista de entradaContable
        var jsonObject = JObject.Parse(jsonResponse);
        var entradaContableArray = jsonObject["entradaContable"].ToString();

        // Ahora puedes deserializar esta lista de entradaContable
        return JsonConvert.DeserializeObject<List<EntradaContableDTO>>(entradaContableArray);
    }*/



        public async Task<AsientoContable> ObtenerAsientoContableAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/Contabilidad/ObtenerAsientoContable?Id={id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AsientoContable>(jsonResponse);
        }

        public async Task<HttpResponseMessage> CrearAsientoContableAsync(AsientoContable asiento)
        {
            var dto = new AsientoContableDTO
            {
                descripcion = asiento.Descripcion,
                auxiliar = asiento.Auxiliar,
                fecha = asiento.Fecha,
                monto = asiento.Monto,
                estado = asiento.Estado.Id, // Asignar el ID del estado
                moneda = asiento.Moneda.Id, // Asignar el ID de la moneda
                transacciones = asiento.Transacciones.Select(t => new TransaccionDTO
                {
                    cuenta = t.Cuenta.Id, // Asignar el ID de la cuenta
                    tipoMovimiento = t.TipoMovimiento.Id, // Asignar el ID del tipo de movimiento
                    monto = t.Monto
                }).ToList()
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync("/Contabilidad/AsientoContable", content);
        }


        public async Task<List<Estado>> ObtenerEstadosContablesAsync()
        {
            var response = await _httpClient.GetAsync("/Contabilidad/EstadosContables");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var estadosResponse = JsonConvert.DeserializeObject<JObject>(jsonResponse);
            var estadosContables = estadosResponse?["estadosContables"].ToObject<List<Estado>>();
            return estadosContables;
        }

        public async Task<List<Moneda>> ObtenerMonedasAsync()
        {
            var response = await _httpClient.GetAsync("/Contabilidad/Monedas");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var monedasResponse = JsonConvert.DeserializeObject<JObject>(jsonResponse);
            var monedas = monedasResponse["monedas"].ToObject<List<Moneda>>();
            return monedas;
        }

        public async Task<List<TipoMovimiento>> ObtenerTiposMovimientoAsync()
        {
            var response = await _httpClient.GetAsync("/Contabilidad/OrigenCuenta");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tiposMovimientoResponse = JsonConvert.DeserializeObject<JObject>(jsonResponse);
            var tiposMovimiento = tiposMovimientoResponse["cuentasContables"].ToObject<List<TipoMovimiento>>();
            return tiposMovimiento;
        }

        public async Task<List<EntradaContableDTO>> ObtenerEntradasContablesxAuxAsync(int auxiliarId)
        {
            var response = await _httpClient.GetAsync($"/Contabilidad/ObtenerEntradaContablexAux?id={auxiliarId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var jsonObject = JObject.Parse(jsonResponse);
            var entradaContableArray = jsonObject["entradaContable"].ToString();

            var entradasContablesDTO = JsonConvert.DeserializeObject<List<EntradaContableDTO>>(entradaContableArray);

            // Obtener estados, monedas, tipos de movimiento y cuentas contables
            var estados = await ObtenerEstadosContablesAsync();
            var monedas = await ObtenerMonedasAsync();
            var tiposMovimiento = await ObtenerTiposMovimientoAsync();
            var cuentasContables = await ObtenerCuentasContablesAsync();

            // Iterar sobre las entradas contables DTO y asignar los nombres/descripciones
            foreach (var entradaContableDTO in entradasContablesDTO)
            {
                // Asignar nombres/descripciones de estado y moneda
                entradaContableDTO.EstadoNombre = estados.FirstOrDefault(e => e.Id == entradaContableDTO.Estado)?.Descripcion;
                entradaContableDTO.MonedaNombre = monedas.FirstOrDefault(m => m.Id == entradaContableDTO.Moneda)?.Descripcion;

                // Iterar sobre los registros y asignar nombres/descripciones de cuenta y tipo de movimiento
                foreach (var registroDTO in entradaContableDTO.Registros)
                {
                    registroDTO.CuentaNombre = cuentasContables.FirstOrDefault(c => c.Id == registroDTO.Cuenta)?.Descripcion;
                    registroDTO.TipoMovimientoNombre = tiposMovimiento.FirstOrDefault(t => t.Id == registroDTO.TipoMovimiento)?.Descripcion;
                }
            }

            return entradasContablesDTO;
        }

        public async Task<List<Cuenta>> ObtenerCuentasContablesAsync()
        {
            var response = await _httpClient.GetAsync("/Contabilidad/CuentasContables");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var cuentasContablesResponse = JsonConvert.DeserializeObject<JObject>(jsonResponse);
            var cuentasContables = cuentasContablesResponse["cuentasContables"].ToObject<List<Cuenta>>();
            return cuentasContables;
        }

        /* public async Task<HttpResponseMessage> ActualizarAsientoContableAsync(int id, AsientoContable asiento)
         {
             var json = JsonConvert.SerializeObject(asiento);
             var content = new StringContent(json, Encoding.UTF8, "application/json");
             return await _httpClient.PutAsync($"RUTA_API_ASIENTO_CONTABLE/{id}", content);
         }*/

        public async Task<HttpResponseMessage> EliminarAsientoContableAsync(int id)
        {
            return await _httpClient.DeleteAsync($"/Contabilidad/AsientoContable{id}");
        }
    }

}