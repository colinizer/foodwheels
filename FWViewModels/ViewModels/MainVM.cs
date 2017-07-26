using FWCore.Interfaces;
using FWCore.Models;
using FWCore.Services;
using FWCore.Utils;
using FWViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FWViewModels.ViewModels
{
    public class MainVM : NotificationBase
    {
        public MainVM()
        {
            Openings = new ObservableCollection<TruckOpeningVM>();
            Favorites = new ObservableCollection<FavoriteTruckVM>();

            HomeCommand = new Command(() =>
            {
                Repository.GetObject<INavigationService>().GotoPage("HomePage");
            });
            MapCommand = new Command(() =>
            {
                Repository.GetObject<INavigationService>().GotoPage("MapPage");
            });
            FavoritesCommand = new Command(() =>
            {
                Repository.GetObject<INavigationService>().GotoPage("FavoritesPage");
            });
            SettingsCommand = new Command(() =>
            {
                Repository.GetObject<INavigationService>().GotoPage("SettingsPage");
            });

            RefreshCommand = new Command(async () =>
            {
                await LoadTruckOpeningsAsync();
            });

            LoadTruckExecutor = new TaskExecutor<string>(async (TruckId) =>
            {
                var truck = await KnownException.Wrap<Truck>( () => TruckService.GetTruck(TruckId), "Cannot get truck information");

                LoadTruckVM(truck);
            });

            SelectTruckCommand = new Command(async (o) =>
            {
                var TruckId = o as string;
                SelectedTruck = null;
                Repository.GetObject<INavigationService>().GotoPage("TruckPage");
                await LoadTruckExecutor.ExecuteAsync(TruckId);
            });

            SendTruckRatingExecutor = new TaskExecutor<TruckVM>(async (o) =>
            {
                var VM = o as TruckVM;

                await KnownException.Wrap( () => TruckService.SendRating(VM.Id, (int)VM.Rating), "There was an issue sending the rating");
                var truck = await KnownException.Wrap( () => TruckService.GetTruck(VM.Id), "Cannot get the latest truck information" );
                VM.Rating = truck.Rating;
            });

            ToggleFavoriteCommand = new Command(async (o) =>
            {
                var VM = o as TruckVM;
                var Value = VM.IsFavorite;
                VM.IsFavorite = !VM.IsFavorite;
                var FoundFav = Favorites.FirstOrDefault(f => f.Id.Equals(VM.Id));
                if (Value == false)
                {
                    if (FoundFav == null)
                        Favorites.Add(new FavoriteTruckVM()
                        {
                            Id = VM.Id,
                            Title = VM.Title,
                            ImageUrl = VM.ImageUrl,
                        });

                    Repository.GetObject<IToastService>().DoBasicToast(VM.Id, "Added Favorite", $"{VM.Title} was added to favorites", VM.ImageUrl);
                }
                else
                {
                    if (FoundFav != null)
                        Favorites.Remove(FoundFav);

                    Repository.GetObject<IToastService>().DoBasicToast(VM.Id, "Removed Favorite", $"{VM.Title} was removed from favorites", VM.ImageUrl);
                }
                var Favs = Favorites.Select(f => new Favorite()
                {
                    Id = f.Id,
                    Title = f.Title,
                    ImageUrl = f.ImageUrl,
                });
                await FavoritesService.Save(Favs);
            });

            ToggleTilePinCommand = new Command(async (o) =>
            {
                var VM = o as TruckVM;
                var Value = VM.IsPinned;
                
                var tps = Repository.GetObject<ITilePinService>();
                var IsPinnedAlready = tps.IsPinned(VM.Id);
                if (Value == false)
                {
                    if (IsPinnedAlready == false)
                    {
                        VM.IsPinned = await tps.PinTile(VM.Id, VM.Title, VM.ImageUrl);
                    }
                    else
                        VM.IsPinned = true;
                }
                else
                {
                    if (IsPinnedAlready == true)
                    {
                        await tps.UnPinTile(VM.Id);
                    }
                    VM.IsPinned = false;
                }
            });

            ClearFavoritesCommand = new Command(async () =>
            {
                await FavoritesService.Clear();
                Favorites.Clear();
            });
        }

        public string AppVersion
        {
            get
            {
                return Repository.GetObject<IAppMetadata>()?.GetVersion() ?? "";
            }
        }
            
        public async void Launch(bool WasRunning, bool LoadData, string LaunchId)
        {
            var Args = (LaunchId ?? "").Split(new char[] { ':' });

            var IsLaunchedFromTile = Args.Length >= 2 && Args[0].Equals("Tile");
            var IsLaunchedFromToast = Args.Length >= 2 && Args[0].Equals("Toast");
            var IsLaunchedFromVoice = Args.Length >= 2 && Args[0].Equals("Voice");

            var NS = Repository.GetObject<INavigationService>();
            var DS = Repository.GetObject<IDialogService>();
            bool StillNeedToGetTrucks = true;
            if (IsLaunchedFromVoice)
            {
                string ResultMessage = null;
                string FoundId = null;
                if(Args.Length > 1 && Args[1].Equals("showTruckType"))
                {
                    var FoodType = Args[2];
                    if (LoadData == false)
                    {
                        await RefreshCommand.ExecuteAsync();
                        StillNeedToGetTrucks = false;
                    }
                    var FoundTruck = Openings.FirstOrDefault(o => o.FoodType.ToLowerInvariant().Contains(FoodType.ToLowerInvariant()));
                    if (FoundTruck != null)
                        FoundId = FoundTruck.TruckId;
                    else
                        ResultMessage = "Couldn't find a truck serving that food";
                }
                if (FoundId != null)
                {
                    SelectTruckCommand.Execute(FoundId);
                }
                else
                {
                    NS.GotoPage("HomePage");
                    if (ResultMessage != null)
                        await DS.ShowDialogAsync("Search for food type", ResultMessage, "OK", null);
                }
            }
            else if (IsLaunchedFromTile || IsLaunchedFromToast)
            {
                SelectTruckCommand.Execute(Args[1]);
            }
            else if (WasRunning == false)
            {
                NS.GotoPage("HomePage");
            }

            if (LoadData)
            {
                await LoadFavorites();

                if(WasRunning)
                    await RestoreState();

                if(StillNeedToGetTrucks)
                    RefreshCommand.Execute();

                if (NS.CurrentPageName.Equals("TruckPage"))
                    LoadTruckExecutor.Execute(SelectedTruck.Id);
                
            }
        }

        public Task EnteredBackground()
        {
            return PreserveState();
        }
        public void Resuming()
        {
            RefreshCommand.Execute();
        }

        private async Task PreserveState()
        {
            var Data = new SessionData()
            {
                TruckOpenings = _TruckOpenings,
                CurrentTruck = _SelectedTruck?.Truck,
            };
            await SessionDataService.Save(Data);
        }

        private async Task RestoreState()
        {
            var Data = await SessionDataService.Load();
            if(Data != null)
            {
                LoadTruckOpeningVMs(Data.TruckOpenings);
                LoadTruckVM(Data.CurrentTruck);
            }
        }


        public async Task LoadTruckOpeningsAsync()
        {
            var TruckOpenings = await KnownException.Wrap(() => TruckService.GetTruckOpenings(), "Cannot get the latest opening information.  Please refresh when online");

            LoadTruckOpeningVMs(TruckOpenings);
        }

        public Command ToggleTilePinCommand { get; set; }
        public Command ToggleFavoriteCommand { get; set; }
        public Command ClearFavoritesCommand { get; set; }
        public Command HomeCommand { get; private set; }
        public Command MapCommand { get; private set; }
        public Command FavoritesCommand { get; private set; }
        public Command SettingsCommand { get; private set; }
        public Command RefreshCommand { get; private set; }
        public Command SelectTruckCommand { get; private set; }
        public TaskExecutor<TruckVM> SendTruckRatingExecutor { get; private set; }
        public TaskExecutor<string> LoadTruckExecutor { get; private set; }

        private TruckVM _SelectedTruck;

        public TruckVM SelectedTruck
        {
            get { return _SelectedTruck; }
            set { NotifySet(ref _SelectedTruck, value); }
        }


        private ObservableCollection<TruckOpeningVM> _Openings;

        public ObservableCollection<TruckOpeningVM> Openings
        {
            get { return _Openings; }
            set { NotifySet(ref _Openings, value); }
        }

        private ObservableCollection<FavoriteTruckVM> _Favorites;

        public ObservableCollection<FavoriteTruckVM> Favorites
        {
            get { return _Favorites; }
            set { NotifySet(ref _Favorites, value); }
        }


        IEnumerable<TruckOpening> _TruckOpenings;
        private void LoadTruckOpeningVMs(IEnumerable<TruckOpening> TruckOpenings)
        {
            if (TruckOpenings == null)
                return;

            _TruckOpenings = TruckOpenings;

            Openings.Clear();
            foreach (var o in TruckOpenings)
                Openings.Add(new TruckOpeningVM()
                {
                    Id = o.Id,
                    ClosingTime = o.ClosingTime,
                    OpeningTime = o.OpeningTime,
                    Latitude = o.Latitude,
                    Longitude = o.Longitude,
                    Rating = o.Rating,
                    FoodType = o.FoodType,
                    ImageUrl = TruckService.UrlRoot + o.ImageUrl,
                    Title = o.Title,
                    TruckId = o.TruckId,
                });
        }

        private void LoadTruckVM(Truck truck)
        {
            if (truck == null)
                return;
            SelectedTruck = new TruckVM()
            {
                Id = truck.Id,
                Truck = truck,
                Title = truck.Title,
                ImageUrl = TruckService.UrlRoot + truck.ImageUrl,
                Rating = truck.Rating,
                FoodType = truck.FoodType,
                IsFavorite = Favorites.Any(f => f.Id.Equals(truck.Id)),
                IsPinned = Repository.GetObject<ITilePinService>().IsPinned(truck.Id),
                FoodItems = truck.FoodItems.Select(fi => new FoodItemVM()
                {
                    Id = fi.Id,
                    Title = fi.Title,
                    ImageUrl = TruckService.UrlRoot + fi.ImageUrl,
                }).ToList(),
                Openings = truck.Openings.Select(op => new OpeningVM()
                {
                    OpeningTime = op.OpeningTime,
                    ClosingTime = op.ClosingTime,
                }).ToList(),
            };
        }

        private async Task LoadFavorites()
        {
            var Favs = await FavoritesService.Load();
            if (Favs == null)
                return;
            foreach (var f in Favs)
                Favorites.Add(new FavoriteTruckVM()
                {
                    Id = f.Id,
                    Title = f.Title,
                    ImageUrl = f.ImageUrl,
                });
        }
    }
}
