using System;
using System.Text;
using System.Text.Json;
using JokerService.SyncXmlWithCloud.Models;

namespace JokerService.SyncXmlWithCloud.Services;
public class DeliveryAPIService
{
  private readonly HttpClient _httpClient;

  public DeliveryAPIService()
  {
    string myApiUrl = Environment.GetEnvironmentVariable("MY_API_URL");
    _httpClient = new HttpClient
    {
      BaseAddress = new Uri(myApiUrl)
    };
  }

  public async Task RequestCancellation(string keyNFe)
  {
    var bodyRequest = new
    {
      key = keyNFe,
    };
    string convertBodyRequestToString = JsonSerializer.Serialize(bodyRequest);

    StringContent content = new(convertBodyRequestToString, Encoding.UTF8, "application/json");
    HttpResponseMessage response = await _httpClient.PostAsync("/cancellation", content);

    if (response.IsSuccessStatusCode)
    {
      string responseBody = await response.Content.ReadAsStringAsync();
    }
    else
    {
      string errorMessage = await response.Content.ReadAsStringAsync();
    }
  }

  public async Task RequestCreateNewDelivery(Delivery delivery, string companyToken)
  {
    var bodyRequest = new
    {
      delivery = delivery,
      companyToken = companyToken
    };
    string convertBodyRequestToString = JsonSerializer.Serialize(bodyRequest);

    StringContent content = new(convertBodyRequestToString, Encoding.UTF8, "application/json");
    HttpResponseMessage response = await _httpClient.PostAsync("/", content);

    if (response.IsSuccessStatusCode)
    {
      string responseBody = await response.Content.ReadAsStringAsync();
    }
    else
    {
      string errorMessage = await response.Content.ReadAsStringAsync();
    }
  }
}
