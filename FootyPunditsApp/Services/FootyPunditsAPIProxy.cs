using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FootyPunditsApp.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using Newtonsoft.Json;

namespace FootyPunditsApp.Services
{
    class FootyPunditsAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:12833/FootyPunditsAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:12833/FootyPunditsAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:12833/FootyPunditsAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:12833/imgs"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://192.168.1.14:12833/imgs"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "http://localhost:12833/imgs"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        public string basePhotosUri;
        private static FootyPunditsAPIProxy proxy = null;

        public static FootyPunditsAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
            }

            if (proxy == null)
                proxy = new FootyPunditsAPIProxy(baseUri, basePhotosUri);
            return proxy;
        }


        private FootyPunditsAPIProxy(string baseUri, string basePhotosUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
        }


        //public async Task<UserAccount> TestAsync(int id)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Test?id={id}");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
        //                PropertyNameCaseInsensitive = true
        //            };
        //            string content = await response.Content.ReadAsStringAsync();
        //            UserAccount u = JsonConvert.DeserializeObject<UserAccount>(content, options);
        //            return u;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return null;
        //    }
        //}
        public async Task<UserAccount> SignUp(string email, string Upassword, string userName, int favoriteTeam)
        {
            try
            {
                UserAccount a = new UserAccount()
                {
                    Email = email,
                    Upass = Upassword,
                    AccName = userName,
                    Username = userName,
                    FavoriteTeam = favoriteTeam,
                    SignUpDate = DateTime.Now,
                    ProfilePicture = "default_pfp.jpg"
                };

                string json = JsonConvert.SerializeObject(a);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = $"{this.baseUri}/signUp";
                HttpResponseMessage response = await this.client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerSettings options = new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    };

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    UserAccount returnedAccount = JsonConvert.DeserializeObject<UserAccount>(jsonContent, options);
                    return returnedAccount;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool?> EmailExists(string email)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/api/email-exists?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    bool? b = JsonConvert.DeserializeObject<bool?>(content);
                    return b;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool?> UsernameExists(string username)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/api/username-exists?username={username}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    bool? b = JsonConvert.DeserializeObject<bool?>(content);
                    return b;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<UserAccount> LoginAsync(string email, string password)
        {
            try
            {
                UserAccount u = new UserAccount()
                {
                    Email = email,
                    Upass = password
                };

                string json = JsonConvert.SerializeObject(u);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    UserAccount returnedAccount = JsonConvert.DeserializeObject<UserAccount>(jsonContent);

                    return returnedAccount;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<string> GenerateToken()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/api/generate-token");
                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //public async Task<bool> UploadImage(FileInfo fileInfo)
        //{
        //    try
        //    {
        //        var multipartFormDataContent = new MultipartFormDataContent();
        //        var fileContent = new ByteArrayContent(File.ReadAllBytes(fileInfo.Name));
        //        multipartFormDataContent.Add(fileContent, "file", "kuku.jpg");
        //        HttpResponseMessage response = await client.PostAsync($"{this.baseUri}/FootyPunditsAPI/UploadImage", multipartFormDataContent);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return false;
        //    }
        //}

        public async Task<bool> UploadImage(string fullPath, string targetFileName)
        {
            try
            {
                var multipartFormDataContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(fullPath));
                multipartFormDataContent.Add(fileContent, "file", targetFileName);
                string url = $"{this.baseUri}/uploadimage";

                HttpResponseMessage response = await client.PostAsync(url, multipartFormDataContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateProfile(string username, string password)
        {
            try
            {
                string url = Uri.EscapeUriString($"{this.baseUri}/update-profile?password={password}&username={username}");
                HttpResponseMessage response = await this.client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public async Task<bool> Logout()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/logout");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    } 
}


