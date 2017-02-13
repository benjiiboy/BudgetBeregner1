using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BudgetBeregner1.Persistency
{
    public class PersistencyService
    {

        private readonly Task<StorageFile> localfolder;
        private object filnavn;

        public ObservableCollection<Model.Budget> BudgetListe { get; set; }


        const String fileName = "Budget1.json";

        public static async void SaveEventAsJsonAsync()
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            string JsonData = JsonConvert.SerializeObject(Model.BudgetCatalogSingleton.Instancebudget.BudgetListe);
            await FileIO.WriteTextAsync(localFile, JsonData);

        }


        public static async Task<ObservableCollection<Model.Budget>> LoadEventsFromJsonAsync()
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            String jsonData = await FileIO.ReadTextAsync(localFile);
            return JsonConvert.DeserializeObject<ObservableCollection<Model.Budget>>(jsonData);
        }

        public void IndsætJson(string filnavn)
        {
            List<Model.Budget> nyListe = JsonConvert.DeserializeObject<List<Model.Budget>>(filnavn);
            foreach (var i in nyListe)
            {
                Model.BudgetCatalogSingleton.Instancebudget.BudgetListe.Add(i);
            }
        }


    }
}

