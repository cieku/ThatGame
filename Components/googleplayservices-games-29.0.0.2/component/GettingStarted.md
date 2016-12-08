The Play Games SDK provides cross-platform Google Play games services that lets you easily integrate popular gaming features such as achievements, leaderboards, Saved Games, and real-time multiplayer in your tablet and mobile games.



Required Android API Levels
===========================

We recommend setting your app's *Target Framework* and *Target Android version* to **Android 5.0 (API Level 21)** or higher in your app project settings.

This Google Play Service SDK's requires a *Target Framework* of at least Android 4.1 (API Level 16) to compile.

You may still set a lower *Minimum Android version* (as low as Android 2.3 - API Level 9) so your app will run on older versions of Android, however you must ensure you do not use any API's at runtime that are not available on the version of Android your app is running on.




Google Play Developer Console Setup
===================================

Follow Google's instructions for [Setting Up Google Play Games Services](https://developers.google.com/games/services/console/enabling).

You will need to know your app's package name (from the *AndroidManifest.xml* file) as well as the SHA1 fingerprint for both your debug keystore and the keystore file you will use to sign your release builds.  If you are not sure how to find your SHA1 fingerprints, visit the documentation on [Finding your SHA-1 Fingerprints][2].




Android Manifest 
================

Some Google Play Services APIs require specific metadata, attributes, permissions or features to be declared in your *AndroidManifest.xml* file.

These can be added manually to the *AndroidManifest.xml* file, or merged in through the use of assembly level attributes.


For Play Games you will need to add the *App ID* you generated previously as metadata to your *AndroidManifest.xml* file.  You can add this by including the following assembly level attribute in your app:

```csharp
[assembly: MetaData ("com.google.android.gms.games.APP_ID", Value="YOUR-APP-ID")]
```



Samples
=======

You can find a Sample Application within each Google Play Services component.  The sample will demonstrate the necessary configuration and some basic API usages.






Learn More
==========

You can learn more about the various Google Play Services SDKs & APIs by visiting the official [Google APIs for Android][3] documentation


You can learn more about Google Play Services Games by visiting the official [Play Games Services SDK for Android](https://developers.google.com/games/services/) documentation.



[1]: https://console.developers.google.com/ "Google Developers Console"
[2]: https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/MD5_SHA1/ "Finding your SHA-1 Fingerprints"
[3]: https://developers.google.com/android/ "Google APIs for Android"

