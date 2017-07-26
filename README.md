# foodwheels (C) Colin Melia 2017

This is a UWP sample application supporting the video course - **Building Universal Windows Platform Apps**

You can access the video course through a [Safari subscription](https://colinize.me/learnuwp) or to through [InformIT](http://colinize.me/learnuwpbuy).

Note that not all platform features from course lessons are demonstrated in the app - see the sub-lessons for those features

Running the app
- To get the app running:
- Download the code Open the solution file with VS 2015 Update 3
- Make sure Debug x86 is selected for the build
- Rebuild the Solution making sure nuget packages are restoring
- On project FWWebService goto right-click menu, select Debug, select Start New Instance; let it launch the default site web page in your default browser; stop debugging; set FWApp set start project and start debugging

Note that the truck data is simulated in FWWebService. It is held in memory and random truck openings are added when the webservice instance starts up.
Note, you will not see the graphical assets seen in the training video since this were specifically licensed for use in the video. You will see basic text images for a truck and dish. If you want that functionality, you will need to: 
- add those assets under FWWebService\Content\TruckImages
- update the fixed array of data in FWWebService\Models\TruckDataService.cs to point to the files you license of course, if you have a license to Fotolia you can get the assets referenced in TruckDataService.cs
- comment out the code indicated at the end of InitData() in FWWebService\Models\TruckDataService.cs

App publishing
- If you want to publish an app you will need to: 
- Start creating app in developer centre Associate app with store app
- Add graphical assets
- Test Package for the store 
- Upload to the developer portal and following the app submission and publication process
