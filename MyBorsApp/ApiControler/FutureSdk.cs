using System;
using System.Net.Http;
using System.Collections.Generic;
using PhoenixFutureApiSdk.Model.Response;
using Newtonsoft.Json;
using PhoenixFutureApiSdk.Model;
using System.Threading.Tasks;
using PhoenixFutureApiSdk.Model.Request;
using System.Text;
using Models.Api.Request;
using Models.Api.Response;

namespace PhoenixFutureApiSdk
{
    public class FutureSdk
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        public FutureSdk(string brokerSubdomain)
        {
            _client = new HttpClient();
            _baseUrl = string.Format("https://{0}.exphoenixfuture.com", brokerSubdomain) + "/{0}";
        }
        
        public async  Task<ResponseModel<List<OpenPosition>>> GetOpenPositions(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = await _client.GetAsync(string.Format(_baseUrl, "api/customer/position-table"));
                return await ResponseHandler<List<OpenPosition>>(response);
                
            }
            catch
            {
                return new ResponseModel<List<OpenPosition>>
                {
                    Status = ResponseStatus.NetworkError
                };
            }  
        }
        public async Task<ResponseModel> NewTransferMoney(TransferMoneyRequest request,string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(string.Format(_baseUrl, "api/customer/new-transfer"),stringContent);
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel<List<OpenPosition>>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }
        public async Task<ResponseModel> NewOrder(NewOrderViewModel request, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(string.Format(_baseUrl, "api/order/new"), stringContent);
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel<List<OpenPosition>>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }
        public async Task<ResponseModel> DeleteOrder(DeleteOrderViewModel request , string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(string.Format(_baseUrl, "api/order/delete"), stringContent);
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel<List<OpenPosition>>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }
        public async Task<ResponseModel> EditOrder(EditOrderViewModel request, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(string.Format(_baseUrl, "api/order/edit"), stringContent);
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel<List<OpenPosition>>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }
        public async Task<ResponseModel<PageResponse>> pendingOrders(string Token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Token);
                OrderParams request = new OrderParams()
                {
                    page = 1,
                    type = -1,
                    status = 4,
                    persianToDate = "",
                    persianFromDate = "",
                    contractId = -1,
                    orderbookId = 0,
                    
                    
                };
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(string.Format(_baseUrl, "api/orders"),
                    stringContent);
                return await ResponseHandler<PageResponse>(response);
            }
            catch 
            {
                return new ResponseModel<PageResponse>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }
        public async Task<ResponseModel<List<OpenPositionTable>>> OpenPosstions( string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);


                var response = await _client.GetAsync(string.Format(_baseUrl, "api/customer/position-table")
                    );
                return await ResponseHandler<List<OpenPositionTable>>(response);
            }
            catch
            {
                return new ResponseModel<List<OpenPositionTable>>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }
        public async Task<ResponseModel<ContractDetailViewModel>> ContractDetail(int id, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);


                var response = await _client.GetAsync(string.Format(_baseUrl, "api/contract/" + id)
                    );
                return await ResponseHandler<ContractDetailViewModel>(response);
            }
            catch
            {
                return new ResponseModel<ContractDetailViewModel>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }
        public async Task<ResponseModel<List<ContractsViewModel>>> GetAllContract( string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);


                var response = await _client.GetAsync(string.Format(_baseUrl, "api/contracts/all")
                    );
                return await ResponseHandler<List<ContractsViewModel>>(response);
            }
            catch
            {
                return new ResponseModel<List<ContractsViewModel>>
                {
                    Status = ResponseStatus.NetworkError
                };
            }
        }




        private async Task<ResponseModel<T>> ResponseHandler<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                return new ResponseModel<T>
                {
                    Response = JsonConvert.DeserializeObject<T>(content),
                    Status = ResponseStatus.Success
                };
            }
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new ResponseModel<T>
                {
                    Status = ResponseStatus.InputError
                };
            }
            return new ResponseModel<T>
            {
                Status = ResponseStatus.InternalServerError
            };
        }
        private async Task<ResponseModel> ResponseHandler(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                return new ResponseModel
                {
                    Status = ResponseStatus.Success
                };
            }
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new ResponseModel
                {
                    Status = ResponseStatus.InputError,
                    ErrorMessage = await httpResponse.Content.ReadAsStringAsync()

                };
            }
            return new ResponseModel
            {
                Status = ResponseStatus.InternalServerError
            };
        }

    }
}
