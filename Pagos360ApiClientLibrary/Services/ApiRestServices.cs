﻿using System;
using System.IO;
using System.Text;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Pagos360ApiClientLibrary.Model;

namespace Pagos360ApiClientLibrary.Services
{
    public static class ApiRestServices
    {
        public static PaginationResult<T> ListObjects<T>(string pPath, string pAPIKey)
        {
            using (HttpClient client = new HttpClient())
            {
                var serializer = new DataContractJsonSerializer(typeof(PaginationResult<T>));

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pAPIKey);
                
                var response = client.GetAsync(pPath).Result;
                var streamTask = response.Content.ReadAsStreamAsync().Result;

                var responseObjets = serializer.ReadObject(streamTask) as PaginationResult<T>;
                return responseObjets;
            }
        }

        public static T CreateObject<T>(string pPath, string pAPIKey, string rootName, T pObject) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                MemoryStream streamObjet = new MemoryStream();
                var serializer = new DataContractJsonSerializer(typeof(T));

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pAPIKey);

                serializer.WriteObject(streamObjet, pObject);
                streamObjet.Position = 0;
                StreamReader sr = new StreamReader(streamObjet);
                string json = "{ \"" + rootName + "\": " + sr.ReadToEnd() + "}";
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync(pPath, httpContent).Result;
                var streamTask = response.Content.ReadAsStreamAsync().Result;

                var  responseObjet = serializer.ReadObject(streamTask) as T;
                return responseObjet;
            }
        }

        public static T GetObject<T>(string pPath, string pAPIKey, int pId) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pAPIKey);

                var response = client.GetAsync(pPath + "/" + pId).Result;
                var streamTask = response.Content.ReadAsStreamAsync().Result;

                var responseObjets = serializer.ReadObject(streamTask) as T;
                return responseObjets;
            }
        }

        public static T DeletObject<T>(string pPath, string pAPIKey, int pId) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pAPIKey);

                var response = client.DeleteAsync(pPath + "/" + pId).Result;
                var streamTask = response.Content.ReadAsStreamAsync().Result;

                var responseObjets = serializer.ReadObject(streamTask) as T;
                return responseObjets;
            }
        }

        public static T CancelObject<T>(string pPath, string pAPIKey, int pId) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pAPIKey);

                var response = client.PutAsync(pPath + "/" + pId + "/cancel", null).Result;
                var streamTask = response.Content.ReadAsStreamAsync().Result;

                var responseObjets = serializer.ReadObject(streamTask) as T;
                return responseObjets;
            }
        }
        
        public static string GetFirstDudeDate<T>(string pPath, string pAPIKey, string rootName, T pObject) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                MemoryStream streamObjet = new MemoryStream();
                var serializer = new DataContractJsonSerializer(typeof(T));

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pAPIKey);

                serializer.WriteObject(streamObjet, pObject);
                streamObjet.Position = 0;
                StreamReader sr = new StreamReader(streamObjet);
                string json = "{ \"" + rootName + "\": " + sr.ReadToEnd() + "}";
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync(pPath, httpContent).Result.Content.ReadAsStringAsync();
                return response.Result;
            }
        }
    }
}
