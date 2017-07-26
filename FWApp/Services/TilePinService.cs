using FWCore.Services;
using FWViewModels.Interfaces;
using NotificationsExtensions;
using NotificationsExtensions.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace FWApp.Services
{
    public class TilePinService : ITilePinService
    {
        public bool IsPinned(string Id)
        {
            return SecondaryTile.Exists("Tile" + Id);
        }

        public async Task<bool> PinTile(string Id, string Title, string ImageUri)
        {
            var TileId = "Tile" + Id;
            var secondaryTile = new SecondaryTile(TileId, 
                Title, "Tile:" + Id,
                new Uri("ms-appx:///Assets/Square150x150Logo.png"), 
                TileSize.Square150x150);            
            secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;
            
            var result = await secondaryTile.RequestCreateAsync();            

            if(result)
            {
                var updater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(TileId);
                var Content = new TileContent()
                {                    
                    Visual = new TileVisual()
                    {                        
                        Arguments = "Tile:" + Id,
                        Branding = TileBranding.Name,                        
                        TileMedium = new TileBinding()
                        {
                            DisplayName = Title,                            
                            Content = new  TileBindingContentAdaptive()
                            {       
                                BackgroundImage = new TileBackgroundImage()
                                {
                                    Source = TruckService.UrlRoot + "imageresize/?Uri=" + ImageUri,
                                    HintOverlay = 60,                                    
                                },                                
                            },
                        },
                    },
                };
                var TileNotification = new TileNotification(Content.GetXml());
                updater.Update(TileNotification);
            }

            return result;
        }

        public async Task UnPinTile(string id)
        {
            var Id = "Tile" + id;
            var Tiles = await SecondaryTile.FindAllAsync();
            var Tile = Tiles.FirstOrDefault(t => t.TileId.Equals(Id));
            await Tile?.RequestDeleteAsync();
        }
    }
}
