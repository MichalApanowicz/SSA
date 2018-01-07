using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid
{
    public static class SampleData
    {

        static readonly MainRepository Repository = new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        public static List<Localization> Localizations = new List<Localization>()
        {
            new Localization()
            {
                Name = "Regał A, Półka nr 1",
                Code = "RA-P1",
                ItemModels = new List<ItemModel>(),
            },
            new Localization()
            {
                Name = "Regał A, Półka nr 2",
                Code = "RA-P2",
                ItemModels = new List<ItemModel>(),
            },
            new Localization()
            {
                Name = "Regał A, Półka nr 3",
                Code = "RA-P3",
                ItemModels = new List<ItemModel>(),
            },
            new Localization()
            {
                Name = "Regał A, Półka nr 4",
                Code = "RA-P4",
                ItemModels = new List<ItemModel>(),
            },
            new Localization()
            {
                Name = "Regał B, Półka nr 1",
                Code = "RB-P1",
                ItemModels = new List<ItemModel>(),
            },
            new Localization()
            {
                Name = "Regał B, Półka nr 2",
                Code = "RB-P2",
                ItemModels = new List<ItemModel>(),
            },
            new Localization()
            {
                Name = "Regał C, Półka nr 1",
                Code = "RC-P1",
                ItemModels = new List<ItemModel>(),
            },
            new Localization()
            {
                Name = "Regał C, Półka nr 2",
                Code = "RC-P2",
                ItemModels = new List<ItemModel>(),
            },
        };

        private static readonly List<Category> Categories = new List<Category>()
        {
            new Category()
            {
                Name = "D",
                ColorR = 214,
                ColorG = 214,
                ColorB = 214,
                ItemModels = new List<ItemModel>(),
            },
            new Category()
            {
                Name = "Z1",
                ColorR = 195,
                ColorG = 1,
                ColorB = 20,
                ItemModels = new List<ItemModel>(),
            },
            new Category()
            {
                Name = "Z2",
                ColorR = 20,
                ColorG = 1,
                ColorB = 195,
                ItemModels = new List<ItemModel>(),
            },
            new Category()
            {
                Name = "Z3",
                ColorR = 195,
                ColorG = 195,
                ColorB = 0,
                ItemModels = new List<ItemModel>(),
            },
            new Category()
            {
                Name = "Z4",
                ColorR = 20,
                ColorG = 195,
                ColorB = 0,
                ItemModels = new List<ItemModel>(),
            }
        };


        private static readonly List<PersonModel> Persons = new List<PersonModel>()
        {
            new PersonModel()
            {
                Name = "Michal Apanowicz",
                Description = "Administrator",
                Lists = new List<ListModel>()
            },
            new PersonModel()
            {
                Name = "GetUserName()",
                Description = "Pobrane dane",
                Lists = new List<ListModel>()
            }
        };

        private static readonly List<ListStatus> ListStatus = new List<ListStatus>()
        {
            new ListStatus
            {
                ListStatusId = (int)ListStatusEnum.Uncommitted,
                Name = "Uncommitted",
            },
            new ListStatus
            {
                ListStatusId = (int)ListStatusEnum.Committed,
                Name = "Committed",
            },
            new ListStatus
            {
                ListStatusId = (int)ListStatusEnum.Terminated,
                Name = "Terminated",
            }
        };

        private static readonly List<ListModel> Lists = new List<ListModel>
        {
            new ListModel()
            {
                Name = "Na biwak",
                Description = "Rzeczy do zabrania na biwak w Ostromecku",
                CreateDate = DateTime.Parse(DateTime.Parse("2017-12-14").ToLongDateString()).ToLongDateString(),
                Person = Persons[0],
                Status = ListStatus[0],
                Items = new List<ItemModel>
                {

                },
            },
            new ListModel()
            {
                Name = "Lista obozowa 1",
                Description = "Rzeczy zabrane na kwaterkę",
                CreateDate = DateTime.Parse("2017-12-14").ToLongDateString(),
                Person = Persons[0],
                Status = ListStatus[0],
                Items = new List<ItemModel>
                {

                },
            },
            new ListModel()
            {
                Name = "Lista Jastrzębi",
                Description = "Rzeczy zabrane na zbiórkę",
                CreateDate = DateTime.Parse("2017-12-14").ToLongDateString(),
                Person = Persons[0],
                Status = ListStatus[0],
                Items = new List<ItemModel>
                {

                },
            }
        };

        public static List<ItemStatus> ItemStatus = new List<ItemStatus>()
        {
            new ItemStatus
            {
                ItemStatusId = (int)ItemStatusEnum.Available,
                Name = "Available",
            },
            new ItemStatus
            {
                ItemStatusId = (int)ItemStatusEnum.Unavailable,
                Name = "Unavailable",
            },
            new ItemStatus
            {
                ItemStatusId = (int)ItemStatusEnum.Reserved,
                Name = "Reserved",
            }
        };

        private static readonly List<ItemModel> Items = new List<ItemModel>
        {
           new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000017",
                Name = "Ponton",
                Description = "4-osobowy, łatany",
                Category = Categories[0],
                LocalizationId = 0
           },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000024",
                Name = "Namiot armasport",
                Description = "niebiesko-czerwony, 3-osobowy",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000031",
                Name = "Namiot fjord nansen",
                Description = "oliwkowy, 3-osobowy",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000048",
                Name = "Namiot McKinley Stone 4",
                Description = "srebrny, 4-osobowy",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000055",
                Name = "Namiot Quechua",
                Description = "oliwkowy, 6-osobowy",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000062",
                Name = "Namiot Quechua",
                Description = "błękitny, 4-osobowy",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000079",
                Name = "Nikon 1 V1",
                Description = "w zestawie z kartą pamięci 16GB",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000086",
                Name = "Lampa błyskowa Nikon SB-N7",
                Description = "w zestawie z 2 bateriami AAA",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000093",
                Name = "Namiot harcerski SCOUT 1",
                Description = "oliwkowy, 10-osoby, w zestawie z masztami",
                Category = Categories[1],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000116",
                Name = "Namiot harcerski SCOUT 2",
                Description = "oliwkowy, 10-osoby, w zestawie z masztami",
                Category = Categories[2],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000123",
                Name = "Namiot harcerski SCOUT 3",
                Description = "oliwkowy, 10-osoby, w zestawie z masztami",
                Category = Categories[3],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000130",
                Name = "Namiot harcerski SCOUT 4",
                Description = "oliwkowy, 10-osoby, w zestawie z masztami",
                Category = Categories[4],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000147",
                Name = "Szpadel greenimil 1",
                Description = "z widocznymi śladami użytkowania",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000154",
                Name = "Szpadel greenimil 2",
                Description = "z widocznymi śladami użytkowania",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000161",
                Name = "Piła ramowa dwuręczna 1",
                Description = "w zestawie z brzeszczotem",
                Category = Categories[4],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000178",
                Name = "Piła ramowa dwuręczna 2",
                Description = "w zestawie z brzeszczotem",
                Category = Categories[1],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000185",
                Name = "Piła ramowa dwuręczna 3",
                Description = "w zestawie z brzeszczotem",
                Category = Categories[2],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000192",
                Name = "Piła ramowa dwuręczna 4",
                Description = "w zestawie z brzeszczotem",
                Category = Categories[3],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000208",
                Name = "Saperka fiskars 1",
                Description = "mała z drewnianym trzonkiem",
                Category = Categories[4],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000215",
                Name = "Saperka fiskars 2",
                Description = "mała z drewnianym trzonkiem",
                Category = Categories[1],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000222",
                Name = "Saperka fiskars 3",
                Description = "mała z drewnianym trzonkiem",
                Category = Categories[2],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000239",
                Name = "Saperka fiskars 4",
                Description = "mała z drewnianym trzonkiem",
                Category = Categories[3],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000246",
                Name = "Młot",
                Description = "10 kg, z drewnianym trzonkiem",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000253",
                Name = "Siekiera",
                Description = "1.5 kg, kuta ręcznie",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000260",
                Name = "Skrzynka do narzędzi 1",
                Description = "16-calowa, w zestawie: młotek ślusarski, siekiera, łom, miara zwijana",
                Category = Categories[4],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000277",
                Name = "Skrzynka do narzędzi 2",
                Description = "16-calowa, w zestawie: młotek ślusarski, siekiera, łom, miara zwijana",
                Category = Categories[1],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000284",
                Name = "Skrzynka do narzędzi 3",
                Description = "16-calowa, w zestawie: młotek ślusarski, siekiera, łom, miara zwijana",
                Category = Categories[2],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000291",
                Name = "Skrzynka do narzędzi 4",
                Description = "16-calowa, w zestawie: młotek ślusarski, siekiera, łom, miara zwijana",
                Category = Categories[3],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000307",
                Name = "Szpadel fiskars 1",
                Description = "czarny",
                Category = Categories[4],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000314",
                Name = "Świder fiskars 2",
                Description = "czarny",
                Category = Categories[3],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000321",
                Name = "Świder fiskars 3",
                Description = "czarny",
                Category = Categories[2],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000338",
                Name = "Świder fiskars 4",
                Description = "czarny",
                Category = Categories[3],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000345",
                Name = "Świder romanik 1",
                Description = "szary",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000352",
                Name = "Piła stanley jednoręczna",
                Description = "rękojeść żółta",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000369",
                Name = "Młotek ciesielski",
                Description = "250g żółty",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000376",
                Name = "Młotek stahlson",
                Description = "500g grafitowy",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000383",
                Name = "Poziomica",
                Description = "50cm, żółta",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000390",
                Name = "Zestaw dłut snycerskich",
                Description = "w zestawie 7 sztuk",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000406",
                Name = "Zszywacz tapicerski",
                Description = "żółty",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000413",
                Name = "Obcęgi 1",
                Description = "szare",
                Category = Categories[4],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000420",
                Name = "Obcęgi 2",
                Description = "szare",
                Category = Categories[1],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000437",
                Name = "Grabie",
                Description = "niebieskie, drewniany trzonek",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000444",
                Name = "Mała siekiera fiskars",
                Description = "czarna",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000451",
                Name = "Łuk",
                Description = "szary w zestawie z 4 strzałami",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000468",
                Name = "Zestaw do badmintona",
                Description = "dwie paletki i dwie lotki",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000475",
                Name = "Makaron pływacki 1",
                Description = "zielony",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000482",
                Name = "Makaron pływacki 2",
                Description = "niebieski",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000499",
                Name = "Piłka nożna kipsta",
                Description = "elementy biąłe i niebieskie",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000505",
                Name = "Zestaw do gry w boules",
                Description = "kompletny",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000512",
                Name = "Rękawice bokserskie para",
                Description = "czerwone",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000529",
                Name = "Rękawice bokserskie para",
                Description = "białe",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000536",
                Name = "Wiatrówka",
                Description = "w zestawie ze śrutem",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000543",
                Name = "Wykrywacz do metali",
                Description = "czarny, w zestawie bateria 9V",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000550",
                Name = "Patelnia",
                Description = "czarna, teflonowa",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000567",
                Name = "Garnek średniej wielkości",
                Description = "pojemność 40 l",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000574",
                Name = "Kociołek turystyczny 10l",
                Description = "pojemność 15 l",
                Category = Categories[0],
                LocalizationId = 0
            },
           new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000581",
                Name = "Pompka ręczna",
                Description = "uniwersalna",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000598",
                Name = "Siatka sportowa",
                Description = "szerokość 3m",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000604",
                Name = "Gaśnica p-poż",
                Description = "ABC, 3 kg",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000611",
                Name = "Bojka ratownicza",
                Description = "pomarańczowa z pasem i liną",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000628",
                Name = "Zestaw do nurkowania",
                Description = "niebieskie, płetwy i maska",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000635",
                Name = "Sygnałówka z ustnikiem",
                Description = "złota",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000642",
                Name = "Plandeka 4x8",
                Description = "zielona",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000659",
                Name = "Plandeka 4x5",
                Description = "zielona",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000666",
                Name = "Plandeka 5x8",
                Description = "zielona",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000673",
                Name = "Plandeka 2x1",
                Description = "niebieska",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000680",
                Name = "Koc gaśniczy",
                Description = "czerwony",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000697",
                Name = "Drabina linowa",
                Description = "długość 2m",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000703",
                Name = "Apteczka turystyczna 1",
                Description = "mała",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000710",
                Name = "Apteczka turystyczna 2",
                Description = "mała",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000727",
                Name = "Garnek dużej wielkości",
                Description = "pojemność: 50 l",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000734",
                Name = "Pojemniki plastikowe 31 l",
                Description = "białe z kranikiem",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000741",
                Name = "Piła kabłąkowa Fiskars",
                Description = "czarna",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000758",
                Name = "Piecyk żeliwny",
                Description = "czarny w zestawie z kominem i iskrownikiem",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000765",
                Name = "Kabel o długości 600m",
                Description = "czarny",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000772",
                Name = "Zestaw resuscytacyjno-tlenowy WOPR RT",
                Description = "czerwona torba",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000789",
                Name = "Laminator Fellowes Saturn 3i A4",
                Description = "srebrny",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000796",
                Name = "Stół Hendi",
                Description = "nierdzewny 1.8x0.6cm",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000802",
                Name = "Duża siekiera Fiskars X7",
                Description = "czarna",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000819",
                Name = "Apteczka plecakowa Janysport AP20 z zawartością",
                Description = "czerwona",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000826",
                Name = "Beczka plastikowa 250 l",
                Description = "czarna",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000833",
                Name = "AGREGAT HECHTGG2000i",
                Description = "czerwony",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000840",
                Name = "Pilarka spalinowa STIHL MS181",
                Description = "pomarańczowa",
                Category = Categories[0],
                LocalizationId = 0
            },
            //new ItemModel()
            //{
            //    Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
            //    KodEAN = "00000840",
            //    Name = "Spodnie ochronne Stihl",
            //    Description = "pomarańczowe",
            //    Category = Categories[0],
            //    LocalizationId = 0
            //},
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000857",
                Name = "Zestaw garnków",
                Description = "4 sztuki 5-20 l",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000864",
                Name = "Materac wojskowy podgumowany",
                Description = "szary",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000871",
                Name = "Rzutka ratownicza",
                Description = "czarna z odblaskiem",
                Category = Categories[0],
                LocalizationId = 0
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000888",
                Name = "Lina torowa",
                Description = "żółta 15 m",
                Category = Categories[0],
                LocalizationId = 0
            },
        };



        public static void AddData()
        {
            foreach (var category in Categories)
            {
                Repository.Save(category);
            }
            foreach (var localization in Localizations)
            {
                Repository.Save(localization);
            }
            foreach (var person in Persons)
            {
                Repository.Save<PersonModel>(person);
            }
            foreach (var status in ListStatus)
            {
                Repository.Save<ListStatus>(status);
            }
            foreach (var status in ItemStatus)
            {
                Repository.Save<ItemStatus>(status);
            }
            foreach (var list in Lists)
            {
                Repository.Save<ListModel>(list);
            }
            foreach (var item in Items.OrderBy(x => x.Name))
            {
                Repository.Save<ItemModel>(item);
            }
        }
        public static void DropData()
        {
            Repository.DeleteAll<Localization>();
            Repository.DeleteAll<Category>();
            Repository.DeleteAll<PersonModel>();
            Repository.DeleteAll<ItemStatus>();
            Repository.DeleteAll<ListStatus>();
            Repository.DeleteAll<ItemModel>();
            Repository.DeleteAll<ListModel>();
        }
    }
}